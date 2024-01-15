using System;
using System.IO;

namespace WeatherParser
{
	internal class Uploader
	{
		public static void Upload()
		{
			var paths = new[] { 
				@"\\insql1\d$\inetpub\wwwroot\pages\weather\",
				@"\\insql2\d$\inetpub\wwwroot\pages\weather\",
				@"\\web\c$\inetpub\wwwroot\pages\weather\",
				@"\\web\c$\inetpub\wwwroot\Content\html\weather\",
			};

			foreach (var path in paths)
			{
				CopyFilesRecursively(AppContext.BaseDirectory + "\\Parsed\\", path);
			}
		}

		private static void CopyFilesRecursively(string sourcePath, string targetPath)
		{
			//Now Create all of the directories
			foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
			{
				Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
			}

			//Copy all the files & Replaces any files with the same name
			foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
			{
				File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
			}
		}
	}
}
