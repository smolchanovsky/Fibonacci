using System;

namespace Infrastructure.Helpers
{
	public interface IDateTimeManager
	{
		DateTime GetNow();
	}

	public class DateTimeManager : IDateTimeManager
	{
		public DateTime GetNow()
		{
			return DateTime.Now;
		}
	}
}