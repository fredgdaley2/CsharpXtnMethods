using System;

namespace CsharpXtnMethods.DateTimeXtns
{

    public static class DateExtensions
    {
        /// <summary>
        /// Get the last date of the month.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>Last date of the month as a DateTime</returns>
        public static DateTime EndOfTheMonth(this DateTime source)
        {
            return new DateTime(source.Year, source.Month, DateTime.DaysInMonth(source.Year, source.Month));
        }

        /// <summary>
        /// Get the month name
        /// </summary>
        /// <param name="source"></param>
        /// <returns>The month name as string</returns>
        public static String ToMonthName(this DateTime source)
        {
            return source.ToString("MMMM");
        }

        /// <summary>
        /// Check if a date is between a range of two dates
        /// </summary>
        /// <param name="inBetweenDate"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>true if the inBetweenDate is between the range of startDate and endDate</returns>
        public static bool IsDateBetween(this DateTime inBetweenDate, DateTime startDate, DateTime endDate)
        {
            return inBetweenDate >= startDate && inBetweenDate <= endDate;
        }


        /// <summary>
        /// Get the next occurrence of a given day. i.e.  Get the next Monday's date.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns>next occurrence of a given day as DateTime</returns>
        public static DateTime GetNextOccDayOfWeek(this DateTime source, DayOfWeek dayOfWeek)
        {
            int daysToAdd = 0;
            if (source.DayOfWeek < dayOfWeek)
            {
                daysToAdd = dayOfWeek - source.DayOfWeek;
            }
            else
            {
                int currDayOfWeek = (int)source.DayOfWeek;
                daysToAdd = (7 - currDayOfWeek) + (int)dayOfWeek;
            }
            return source.AddDays(daysToAdd);
        }

        /// <summary>
        /// Get the next business day
        /// </summary>
        /// <param name="source"></param>
        /// <returns>next business day as DateTime</returns>
        public static DateTime GetNextBussinessDay(this DateTime source)
        {
            int daysToAdd = 0;
            if (source.DayOfWeek == DayOfWeek.Saturday) daysToAdd = 2;
            if (source.DayOfWeek == DayOfWeek.Sunday) daysToAdd = 1;
            return source.AddDays(daysToAdd);
        }


        /// <summary>
        /// Check if date is an weekend day
        /// </summary>
        /// <param name="source"></param>
        /// <returns>true if date is a weekend day</returns>
        public static bool IsWeekEnd(this DateTime source)
        {
            return (source.DayOfWeek == DayOfWeek.Sunday || source.DayOfWeek == DayOfWeek.Saturday);
        }

        public static int YearsOld(this DateTime source)
        {
            if (DateTime.Today.Month < source.Month || DateTime.Today.Month == source.Month && DateTime.Today.Day < source.Day)
            {
                return DateTime.Today.Year - source.Year - 1;
            }
            return DateTime.Today.Year - source.Year;
        }

        /// <summary>
        /// Formats DateTime as yyyy-MM-dd HH:mm:ss
        /// Removes the time if time is 00:00:00
        /// </summary>
        /// <param name="source"></param>
        /// <returns>DateTime as string "yyyy-MM-dd HH:mm:ss" or "yyyy-MM-dd" if time is zero</returns>
        public static String ToMySqlDateFormat(this DateTime source)
        {
            if (source.Hour == 0 && source.Minute == 0 && source.Second == 0)
            {
                return source.ToString("yyyy-MM-dd");
            }
            return source.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
