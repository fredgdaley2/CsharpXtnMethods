using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using CsharpXtnMethods.StringExtns;
namespace CsharpXtnMethods.GenericXtns
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Check if a value exists in a List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns>true if value exists in the List</returns>
        public static bool Exists<T>(this List<T> source, T value)
        {
            return source.IndexOf(value) > -1;
        }

        /// <summary>
        /// Reads the entire file of the given fileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <returns>The contents of the file as byte[]</returns>
        public static byte[] GetFileData(this string fileName, string filePath)
        {
            var fullFilePath = string.Format("{0}/{1}", filePath, fileName);
            if (!File.Exists(fullFilePath))
            {
                throw new FileNotFoundException("The file does not exist.", fullFilePath);
            }
            return File.ReadAllBytes(fullFilePath);
        }

        /// <summary>
        /// Converts a string to a stream
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Stream</returns>
        public static Stream ToStream(this string str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Get the string value of a property name of an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="o"></param>
        /// <param name="propertySelector"></param>
        /// <returns>name of property as string</returns>
        public static string Name<T, TProp>(this T o, Expression<Func<T, TProp>> propertySelector)
        {
            MemberExpression body = (MemberExpression)propertySelector.Body;
            return body.Member.Name;
        }

        /// <summary>
        /// Converts the pascal-cased property name to a displayable name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="o"></param>
        /// <param name="propertySelector"></param>
        /// <returns></returns>

        public static string GetDisplayName<T, TProp>(this T o, Expression<Func<T, TProp>> propertySelector)
        {
            MemberExpression body = (MemberExpression)propertySelector.Body;
            string nameToDisplay = body.Member.Name;
            return nameToDisplay.ToDisplayable();
        }


        /// <summary>
        /// Check if object is in a given list of values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        /// <param name="values"></param>
        /// <returns>true if object is in the given list of values</returns>
        public static bool In<T>(this T @object, params T[] values)
        {
            //if (myVal.In("what","this","cool"))
            return values.Contains(@object);
        }


        public static IEnumerable<T> RemoveDuplicates<T>(this ICollection<T> list, Func<T, int> predicate)
        {
            var dict = new Dictionary<int, T>();

            foreach (var item in list)
            {
                if (!dict.ContainsKey(predicate(item)))
                {
                    dict.Add(predicate(item), item);
                }
            }

            return dict.Values.AsEnumerable();
        }


        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var knownKeys = new HashSet<TKey>();
            return source.Where(element => knownKeys.Add(keySelector(element)));
        }



    }
}
