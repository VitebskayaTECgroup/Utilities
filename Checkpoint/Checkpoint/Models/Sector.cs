using System;

namespace Checkpoint.Models
{
	public class Sector
	{
		public TimeSpan Enter { get; set; }

		public TimeSpan Leave { get; set; }

		public TimeSpan WorkEnter { get; set; }

		public TimeSpan WorkLeave { get; set; }

		public TimeSpan WorkTime(WorkBoundaries work)
		{
			// пришел до конца дня
			if (Enter < work.WorkEnd) 
			{
				// пришел до начала дня
				if (Enter < work.WorkStart) 
				{
					WorkEnter = work.WorkStart;

					// пришел до начала дня, ушел до начала дня
					if (Leave < work.WorkStart)
					{
						WorkLeave = work.WorkStart;

						return TimeSpan.Zero;
					}

					// пришел до начала дня, ушел до начала обеда
					else if (Leave < work.DinnerStart)
					{
						WorkLeave = Leave;

						return Leave - work.WorkStart;
					}

					// пришел до начала дня, ушел до конца обеда
					else if (Leave < work.DinnerEnd)
					{
						WorkLeave = work.DinnerStart;

						return work.DinnerStart - work.WorkStart;
					}

					// пришел до начала дня, ушел до конца дня
					else if (Leave < work.WorkEnd)
					{
						WorkLeave = Leave;

						return Leave - (work.DinnerEnd - work.DinnerStart) - work.WorkStart;
					}

					// пришел до начала дня, ушел после конца дня
					else
					{
						WorkLeave = work.WorkEnd;

						return work.WorkEnd - (work.DinnerEnd - work.DinnerStart) - work.WorkStart;
					}
				}

				// пришел после начала дня до обеда
				else if (Enter < work.DinnerStart) 
				{
					WorkEnter = Enter;

					// пришел после начала дня до обеда, ушел до начала обеда
					if (Leave < work.DinnerStart)
					{
						WorkLeave = Leave;

						return Leave - Enter;
					}

					// пришел после начала дня до обеда, ушел до конца обеда
					else if (Leave < work.DinnerEnd)
					{
						WorkLeave = work.DinnerStart;

						return work.DinnerStart - Enter;
					}

					// пришел после начала дня до обеда, ушел до конца дня
					else if (Leave < work.WorkEnd)
					{
						WorkLeave = Leave;

						return Leave - (work.DinnerEnd - work.DinnerStart) - Enter;
					}

					// пришел после начала дня до обеда, ушел после конца дня
					else
					{
						WorkLeave = work.WorkEnd;

						return work.WorkEnd - (work.DinnerEnd - work.DinnerStart) - Enter;
					}
				}

				// пришел во время обеда
				else if (Enter < work.DinnerEnd) 
				{
					WorkEnter = work.DinnerEnd;

					// пришел во время обеда, ушел до конца обеда
					if (Leave < work.DinnerEnd)
					{
						WorkLeave = work.DinnerEnd;

						return TimeSpan.Zero;
					}

					// пришел во время обеда, ушел до конца дня
					else if (Leave < work.WorkEnd)
					{
						WorkLeave = Leave;

						return Leave - work.DinnerEnd;
					}

					// пришел во время обеда, ушел после конца дня
					else
					{
						WorkLeave = work.WorkEnd;

						return work.WorkEnd - work.DinnerEnd;
					}
				}

				// пришел после обеда
				else
				{
					WorkEnter = Enter;

					// пришел после обеда, ушел до конца дня
					if (Leave < work.WorkEnd)
					{
						WorkLeave = Leave;

						return Leave - Enter;
					}
					
					// пришел после обеда, ушел после конца дня
					else
					{
						WorkLeave = work.WorkEnd;

						return work.WorkEnd - Enter;
					}
				}
			}

			// пришел после конца дня
			else
			{
				WorkEnter = work.WorkEnd;
				WorkLeave = work.WorkEnd;

				return TimeSpan.Zero;
			}
		}
	}
}