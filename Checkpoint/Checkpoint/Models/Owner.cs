using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Checkpoint.Models
{
	public class Owner
	{
		public int Id { get; set; } = -1;

		public string Official { get; set; }

		public string TableId { get; set; }

		public string Group { get; set; }

		public string Department { get; set; }

		public DateTime ViewDate { get; set; }

		public int AlcoholPass { get; set; }


		public List<Event> Events { get; set; } = new List<Event>();

		public List<Sector> Sectors { get; set; } = new List<Sector>();

		public TimeSpan WorkEnter { get; set; } = TimeSpan.Zero;

		public TimeSpan DinnerLeave { get; set; } = TimeSpan.Zero;

		public TimeSpan DinnerReturn { get; set; } = TimeSpan.Zero;

		public TimeSpan WorkLeave { get; set; } = TimeSpan.Zero;

		public TimeSpan WorkTime { get; set; } = TimeSpan.Zero;

		public TimeSpan FactTime { get; set; } = TimeSpan.Zero;


		// Проверка на аномалии в данных и нормализация их порядка

		public void Process(WorkBoundaries work)
		{
			var passEvents = Events.Where(x => x.Code < 340).ToList();

			// алкотестирование
			if (Events.Count(x => x.Code == 342) > 0)
			{
				AlcoholPass = -1;
			}
			else if (Events.Count(x => x.Code == 343) > 0)
			{
				AlcoholPass = 1;
			}
			else
			{
				AlcoholPass = 0;
			}

			// Диапазоны времени на работе
			Sectors = new List<Sector>();
			TimeSpan? enter = null;

			// Разнесение событий по времени
			foreach (var ev in passEvents.OrderBy(x => x.Date))
			{
				if (Event.FromType(ev.Type).Contains("Вход"))
				{
					// Вход
					// Если сектор начат, игнорируем
					// Если сектора нет, начинаем новый
					if (!enter.HasValue)
					{
						enter = ev.Date.TimeOfDay;
					}

					if (WorkEnter.Empty())
					{
						WorkEnter = ev.Date.TimeOfDay;
					}
					else
					{
						DinnerReturn = ev.Date.TimeOfDay;
					}
				}
				else
				{
					// Выход
					// Если сектор уже начат, завершаем его, добавляем в список и обнуляем переменную
					// Если сектора ещё нет, игнорируем
					if (enter.HasValue)
					{
						Sectors.Add(new Sector
						{
							Enter = enter.Value,
							Leave = ev.Date.TimeOfDay
						});

						enter = null;
					}

					if (DinnerLeave.Empty())
					{
						DinnerLeave = ev.Date.TimeOfDay;
					}
					else
					{
						WorkLeave = ev.Date.TimeOfDay;
					}
				}
			}
			if (enter.HasValue)
			{
				Sectors.Add(new Sector
				{
					Enter = enter.Value,
					Leave = TimeSpan.FromDays(1)
				});
			}

			// Если нет проходов на обеде, дата переставляется на крайнюю позицию
			if (WorkLeave.Empty() && !DinnerLeave.Empty())
			{
				if (DinnerLeave > work.DinnerEnd)
				{
					Debug.WriteLine(Official + " [1]");
					WorkLeave = DinnerLeave;
					DinnerLeave = TimeSpan.Zero;
				}
			}

			// Проверка на исключение: если дата прохода на обед записалось как время прохода на станцию, а того не было
			//if (!WorkEnter.Empty() && !DinnerLeave.Empty() && DinnerReturn.Empty())
			//{
			//    if (WorkEnter > DinnerLeave)
			//    {
			//        Debug.WriteLine(Official + " [2]");
			//        DinnerReturn = WorkEnter;
			//        WorkEnter = TimeSpan.Zero;
			//    }
			//}

			// Проверка - несколько выходов после работы, если выхода на обед нет (лишняя запись как на обед)
			if (!DinnerLeave.Empty() && !WorkLeave.Empty() && DinnerReturn.Empty())
			{
				if (DinnerLeave > work.DinnerEnd)
				{
					Debug.WriteLine(Official + " [3]");
					WorkLeave = DinnerLeave;
					DinnerLeave = TimeSpan.Zero;
				}
			}

			// Проверка - несколько выходов после работы, если выхода на обед нет (лишняя запись как на обед)
			if (!WorkEnter.Empty() && !DinnerReturn.Empty() && DinnerLeave.Empty())
			{
				if (DinnerReturn < work.DinnerStart)
				{
					Debug.WriteLine(Official + " [4]");
					WorkEnter = DinnerReturn;
					DinnerReturn = TimeSpan.Zero;
				}
			}
			if (!DinnerLeave.Empty())
			{
				if (DinnerLeave > work.WorkEnd)
				{
					Debug.WriteLine(Official + " [5]");
					WorkLeave = DinnerLeave;
					DinnerLeave = TimeSpan.Zero;
				}
			}
			if (!DinnerReturn.Empty())
			{
				if (DinnerReturn > work.WorkEnd)
				{
					Debug.WriteLine(Official + " [6]");
					DinnerReturn = TimeSpan.Zero;
				}
			}

			// Проверка - несколько выходов после работы, если есть выход на обед (лишняя запись как с обед)
			if (!WorkLeave.Empty() && !DinnerReturn.Empty() && !DinnerLeave.Empty())
			{
				if (WorkLeave < DinnerReturn)
				{
					Debug.WriteLine(Official + " [7]");
					if (WorkLeave < DinnerLeave)
					{
						DinnerLeave = WorkLeave;
					}
					WorkLeave = TimeSpan.Zero;
				}
			}

			// Нормализация порядка следования событий (Голомуздов)
			if (!WorkEnter.Empty() && !DinnerLeave.Empty() && DinnerReturn.Empty() && WorkLeave.Empty())
			{
				if (WorkEnter > DinnerLeave)
				{
					Debug.WriteLine(Official + " [8]");
					DinnerReturn = WorkEnter;
					WorkEnter = TimeSpan.Zero;
				}
			}


			// Определение проведенного времени на работе за выбранный период
			if (!WorkEnter.Empty() && !DinnerLeave.Empty() && !DinnerReturn.Empty() && !WorkLeave.Empty())
			{
				FactTime = WorkLeave - DinnerReturn + DinnerLeave - WorkEnter;
			}
			else if (!WorkEnter.Empty() && !WorkLeave.Empty())
			{
				FactTime = WorkLeave - WorkEnter - TimeSpan.Parse("00:45:00");
			}
			else if (!DinnerReturn.Empty() && !WorkLeave.Empty())
			{
				FactTime = WorkLeave - DinnerReturn;
			}
			else if (!WorkEnter.Empty() && !DinnerLeave.Empty())
			{
				FactTime = DinnerLeave - WorkEnter;
			}
			else
			{
				FactTime = TimeSpan.Zero;
			}

			// Определение рабочего времени за выбранный период
			//if (!WorkEnter.Empty() && !WorkLeave.Empty())
			//{
			//	var enter1 = WorkEnter.Empty()
			//		? WorkBoundaries.DayDinnerStart()
			//		: TimeSpan.FromMinutes(Math.Max(WorkEnter.TotalMinutes, WorkBoundaries.DayWorkStart().TotalMinutes));

			//	var leave1 = WorkLeave.Empty()
			//		? WorkBoundaries.DayWorkEnd(ViewDate)
			//		: TimeSpan.FromMinutes(Math.Min(WorkLeave.TotalMinutes, WorkBoundaries.DayWorkEnd(ViewDate).TotalMinutes));

			//	if (!DinnerLeave.Empty() && !DinnerReturn.Empty())
			//	{
			//		var leave2 = DinnerLeave.Empty()
			//			? WorkBoundaries.DayDinnerStart()
			//			: TimeSpan.FromMinutes(Math.Min(DinnerLeave.TotalMinutes, WorkBoundaries.DayDinnerStart().TotalMinutes));

			//		var enter2 = DinnerReturn.Empty()
			//			? WorkBoundaries.DayDinnerEnd()
			//			: TimeSpan.FromMinutes(Math.Max(DinnerReturn.TotalMinutes, WorkBoundaries.DayDinnerEnd().TotalMinutes));

			//		WorkTime = leave1 - enter1 - (enter2 - leave2);
			//	}
			//	else
			//	{
			//		WorkTime = leave1 - enter1 - (WorkBoundaries.DayDinnerEnd() - WorkBoundaries.DayDinnerStart());
			//	}
			//}
			//else
			//{
			//	WorkLeave = TimeSpan.Zero;
			//}

			WorkTime = TimeSpan.FromMinutes(Sectors.Select(x => x.WorkTime(work).TotalMinutes).Sum());

			// Определение наличия нарушений
			if (Group == "Дневной персонал")
			{
				IsWorkEnterFault = !WorkEnter.Empty() && WorkEnter > work.WorkStart;
				IsDinnerLeaveFault = !DinnerLeave.Empty() && DinnerLeave < work.DinnerStart;
				IsDinnerReturnFault = !DinnerReturn.Empty() && DinnerReturn > work.DinnerEnd;
				IsWorkLeaveFault = !WorkLeave.Empty() && WorkLeave < work.WorkEnd;

				if (passEvents.Count > 0)
				{
					var date = passEvents[0].Date.Date;
					if (date < DateTime.Today)
					{
						// Отслеживание отсутствующих данных за прошедший день
						if (!WorkEnter.Empty() && WorkLeave.Empty()) IsFault = true;
						if (WorkEnter.Empty() && !WorkLeave.Empty()) IsFault = true;
						if (!DinnerLeave.Empty() && DinnerReturn.Empty()) IsFault = true;
						if (DinnerLeave.Empty() && !DinnerReturn.Empty()) IsFault = true;

						// Проверка времени на работе
						if (WorkLeave < TimeSpan.Parse(ViewDate.DayOfWeek == DayOfWeek.Friday ? "07:00:00" : "08:15:00"))
						{
							IsWorkTimeFault = true;
						}
					}
				}

				IsFault = IsWorkEnterFault || IsDinnerLeaveFault || IsDinnerReturnFault || IsWorkLeaveFault || IsWorkTimeFault;
			}
		}


		// Проверка на нарушения для дневного персонала

		public bool IsFault { get; set; } = false;

		public bool IsWorkEnterFault { get; set; } = false;

		public bool IsDinnerLeaveFault { get; set; } = false;

		public bool IsDinnerReturnFault { get; set; } = false;

		public bool IsWorkLeaveFault { get; set; } = false;

		public bool IsWorkTimeFault { get; set; } = false;


		// Получение сводки по данных

		public string Summary => !WorkTime.Empty() && ViewDate.Date != DateTime.Today
			? WorkTime.ToString()
			: Events.Count > 0 
				? "" 
				: "Нет данных";

		public string SummaryFact => !FactTime.Empty()
			? FactTime.ToString()
			: Events.Count > 0
				? ""
				: "Нет данных";

		public string SummaryText
		{
			get
			{
				if (!WorkTime.Empty())
				{
					string temp = "";

					if (WorkTime.Hours > 0)
					{
						string s = WorkTime.Hours.ToString();
						int x = int.Parse(s[s.Length - 1].ToString());
						if (x > 4 || x == 0)
						{
							temp += s + " часов ";
						}
						else if (x > 1)
						{
							temp += s + " часа ";
						}
						else
						{
							temp += s + " час ";
						}
					}

					if (WorkTime.Minutes > 0)
					{
						string s = WorkTime.Minutes.ToString();
						int x = int.Parse(s[s.Length - 1].ToString());
						if (x > 4 || x == 0)
						{
							temp += s + " минут ";
						}
						else if (x > 1)
						{
							temp += s + " минуты ";
						}
						else
						{
							temp += s + " минута ";
						}
					}

					if (WorkTime.Seconds > 0)
					{
						string s = WorkTime.Seconds.ToString();
						int x = int.Parse(s[s.Length - 1].ToString());
						if (x > 4 || x == 0)
						{
							temp += s + " секунд";
						}
						else if (x > 1)
						{
							temp += s + " секунды";
						}
						else
						{
							temp += s + " секунда";
						}
					}

					return temp;
				}
				else
				{
					return Events.Count > 0
						? "Недостаточно данных для расчета"
						: "Сотрудник не отмечен на проходной в этот день";
				}
			}
		}

		public string SummaryTextFact
		{
			get
			{
				if (!FactTime.Empty())
				{
					string temp = "";

					if (FactTime.Hours > 0)
					{
						string s = FactTime.Hours.ToString();
						int x = int.Parse(s[s.Length - 1].ToString());
						if (x > 4 || x == 0)
						{
							temp += s + " часов ";
						}
						else if (x > 1)
						{
							temp += s + " часа ";
						}
						else
						{
							temp += s + " час ";
						}
					}

					if (FactTime.Minutes > 0)
					{
						string s = FactTime.Minutes.ToString();
						int x = int.Parse(s[s.Length - 1].ToString());
						if (x > 4 || x == 0)
						{
							temp += s + " минут ";
						}
						else if (x > 1)
						{
							temp += s + " минуты ";
						}
						else
						{
							temp += s + " минута ";
						}
					}

					if (FactTime.Seconds > 0)
					{
						string s = FactTime.Seconds.ToString();
						int x = int.Parse(s[s.Length - 1].ToString());
						if (x > 4 || x == 0)
						{
							temp += s + " секунд";
						}
						else if (x > 1)
						{
							temp += s + " секунды";
						}
						else
						{
							temp += s + " секунда";
						}
					}

					return temp;
				}
				else
				{
					return Events.Count > 0
						? "Недостаточно данных для расчета"
						: "Сотрудник не отмечен на проходной в этот день";
				}
			}
		}

		public bool IsFactWorkTimeFault { get; set; }
	}
}