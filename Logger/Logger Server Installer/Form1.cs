using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger_Installer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			folderBrowserDialog1.SelectedPath = textBox1.Text;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DialogResult result = folderBrowserDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				var folderName = folderBrowserDialog1.SelectedPath;

				textBox1.Text = folderName;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (CheckDatabase())
			{
				label3.Text = "Да";
			}
			else
			{
				label3.Text = "Нет";
			}
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			label3.Text = "Нет";
		}

		private void button3_Click(object sender, EventArgs e)
		{
			richTextBox1.Text = "";

			// Проверить, что папка есть
			string place = textBox1.Text;
			if (!Directory.Exists(place))
			{
				MessageBox.Show("Указанной папки не существует");
				return;
			}
			Log("Папка установки доступна");

			// Проверить, что база доступна
			string connString = textBox2.Text;
			if (!CheckDatabase())
			{
				//MessageBox.Show("Нет подключения к базе данных");
				return;
			}
			Log("Подключение к базе данных доступно");

			button3.Enabled = false;

			try
			{
				// Остановка и удаление службы
				RemoveService();

				// Копирование файлов
				MoveBinaries(place);

				// Пересоздание базы данных
				RebuildDatabase(connString);

				// Создание службы
				CreateService(place);

				// Запуск службы
				RunService();

				if (MessageBox.Show("Установка завершена! Выйти из инстраллятора?", "Успех!", MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					Application.Exit();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка! " + ex.Message);
			}
			finally
			{
				button3.Enabled = true;
			}
		}

		// методы

		bool CheckDatabase()
		{
			using (var conn = new SqlConnection())
			{
				try
				{
					conn.ConnectionString = textBox2.Text;
					conn.Open();

					using (var command = new SqlCommand())
					{
						command.Connection = conn;
						command.CommandText = "SELECT 1;";
						var result = command.ExecuteScalar();

						if (result.ToString() == "1")
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ошибка! " + ex.Message);
					return false;
				}
			}
		}

		void RemoveService()
		{
			Log("Удаление существующей службы...");

			// Удаление записи в реестре
			// Нужно, потому что при удалении из оснастки служба всего лишь помечается на удаление после перезагрузки
			try
			{
				using (var view = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
				{
					using (var service = view.OpenSubKey(@"SYSTEM\CurrentControlSet\Services", true))
					{
						service.DeleteSubKeyTree("Logger Server");
					}
				}
			}
			catch { }

			// Удаление записи в оснастке
			using (var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "cmd.exe",
					CreateNoWindow = true,
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					UseShellExecute = false,
				},
			})
			{
				process.Start();
				process.StandardInput.WriteLine("sc stop \"Logger Server\" && exit");
				if (!process.WaitForExit(5000)) process.Close();
			}
			using (var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "cmd.exe",
					CreateNoWindow = true,
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					UseShellExecute = false,
				},
			})
			{
				process.Start();
				process.StandardInput.WriteLine("sc delete \"Logger Server\" && exit");
				if (!process.WaitForExit(5000)) process.Close();
			}
			Log("Удаление существующей службы завершено");
		}

		void MoveBinaries(string dest)
		{
			Log("Копирование файлов...");

			string source = AppDomain.CurrentDomain.BaseDirectory + "\\Binaries\\";

			CopyDirectory(source, dest, true);

			Log("Копирование файлов завершено");

			void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
			{
				var dir = new DirectoryInfo(sourceDir);
				if (!dir.Exists)
					throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

				DirectoryInfo[] dirs = dir.GetDirectories();
				Directory.CreateDirectory(destinationDir);

				foreach (FileInfo file in dir.GetFiles())
				{
					string targetFilePath = Path.Combine(destinationDir, file.Name);
					file.CopyTo(targetFilePath, true);

					Log("Копирование файла: " + targetFilePath);
				}

				if (recursive)
				{
					foreach (DirectoryInfo subDir in dirs)
					{
						string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
						CopyDirectory(subDir.FullName, newDestinationDir, true);
					}
				}
			}
		}

		void RebuildDatabase(string connString)
		{
			Log("Создание структуры базы...");

			using (var conn = new SqlConnection())
			{
				conn.ConnectionString = connString;
				conn.Open();

				using (var command = new SqlCommand())
				{
					command.Connection = conn;
					command.CommandText = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Scripts\\Structure.sql");
					command.CommandTimeout = 60;

					int r = command.ExecuteNonQuery();

					Log("ВЫполнено операций: " + r);
				}
			}

			Log("Создание структуры базы завершено");
		}

		void CreateService(string dest)
		{
			Log("Создание службы...");

			using (var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "cmd.exe",
					CreateNoWindow = true,
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					UseShellExecute = false,
				},
			})
			{
				process.Start();
				process.StandardInput.WriteLine("sc create \"Logger Server\" binPath=\"" + dest + "\\Logger Server.exe\" DisplayName=\"Logger Server\" start=auto && exit");
				if (!process.WaitForExit(5000)) process.Close();
			}

			Log("Создание службы завершено");
		}

		void RunService()
		{
			if (MessageBox.Show("Запустить службу?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				Log("Запуск службы...");

				using (var process = new Process
				{
					StartInfo = new ProcessStartInfo
					{
						FileName = "cmd.exe",
						CreateNoWindow = true,
						RedirectStandardInput = true,
						RedirectStandardOutput = true,
						UseShellExecute = false,
					},
				})
				{
					process.Start();
					process.StandardInput.WriteLine("sc start \"Logger Server\" && exit");
					if (!process.WaitForExit(5000)) process.Close();
				}

				Log("Служба запущена. Веб-консоль доступна по адресу: http://localhost:4330/");
			}
		}

		void Log(string output)
		{
			richTextBox1.AppendText(output + "\r\n");
			richTextBox1.ScrollToCaret();
		}
	}
}
