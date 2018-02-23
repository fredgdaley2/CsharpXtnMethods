using System;
using System.Linq;
using CsharpXtnMethods.StringExtns;
namespace CsharpXtnMethods.EnumExtns
{
    public class EnumExtensions
    {
        /// <summary>
        /// Convert to Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public System.Nullable<T> ToEnum<T>(string value) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            string enumName = null;
            enumName = Enum.GetNames(typeof(T)).FirstOrDefault(e => e.IsEqualTo(value));
            if (string.IsNullOrEmpty(enumName))
            {
                return null;
            }
            T result = default(T);
            if (Enum.TryParse<T>(value, true, out result))
            {
                return result;
            }
            return null;
        }

    }
}
