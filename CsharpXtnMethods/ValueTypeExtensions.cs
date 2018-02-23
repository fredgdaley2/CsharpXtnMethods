using System;

namespace CsharpXtnMethods.ValueTypeExtns
{
    public static class ValueTypeExtensions
    {

        public static int ToInt(this bool value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Returns a string formatted as money {0:C?} given the number of decimals.
        /// </summary>
        public static string FormatToMoney(this decimal source, int numOfDecimals = 0)
        {
            string moneyFormat = "{0:C" + numOfDecimals + "}";
            return String.Format(moneyFormat, source, numOfDecimals);
        }


        /// <summary>
        /// Returns a string formatted as money {0:C?} given the number of decimals.
        /// </summary>
        public static string FormatToMoney(this double source, int numOfDecimals = 0)
        {
            string moneyFormat = "{0:C" + numOfDecimals + "}";
            return String.Format(moneyFormat, source, numOfDecimals);
        }


        /// <summary>
        /// Returns a string formatted like #,###;(0);zero
        /// </summary>
        public static string Formatted(this int source)
        {
            return source.ToString("#,###;(0);0");
        }


    }
}
