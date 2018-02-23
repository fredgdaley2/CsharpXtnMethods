using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CsharpXtnMethods.ValueTypeExtns;

namespace CsharpXtnMethods.StringExtns
{
    public static class StringExtensions
    {
        private const string Comma = ",";

        public static string ToDelimitedValue(this object source, string delimiter = Comma, bool addDelimiter = true)
        {
            string delValue = null;
            if (source == null)
            {
                delValue = string.Empty;
            }
            else
            {
                delValue = Convert.ToString(source).Trim();
            }

            if (addDelimiter)
            {
                return delValue.EncloseInQuotes(delimiter) + delimiter;
            }
            else
            {
                return delValue.EncloseInQuotes(delimiter);
            }
        }


        public static string ToDelimitedValueForceQuotes(this object source, string delimiter = Comma, bool addDelimiter = true)
        {
            string delValue = null;
            if (source == null)
            {
                delValue = string.Empty;
            }
            else
            {
                delValue = Convert.ToString(source).Trim();
            }

            if (addDelimiter)
            {
                return delValue.ToQuoted() + delimiter;
            }
            else
            {
                return delValue.ToQuoted();
            }
        }

        public static string AddBlankLines(this string source, int numberOfBlankLines)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(source);
            for (int i = 0; i <= numberOfBlankLines - 1; i++)
            {
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Return Left specified number of characters
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string Left(this string source, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (length == 0 || source.Length == 0)
            {
                return string.Empty;
            }
            else if (source.Length <= length)
            {
                return source;
            }
            else
            {
                return source.Substring(0, length);
            }
        }

        /// <summary>
        /// Return Right specified number of characters
        /// </summary>
        public static string Right(this string source, int length)
        {
            if (source.IsEmpty()) return string.Empty;

            return source.Length <= length ? source : source.Substring(source.Length - length);
        }

        public static bool IsValidEmailAddress(this string source)
        {
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return regex.IsMatch(source);
        }

        public static string RemoveCharZero(this string source)
        {
            return source.Replace("\0", string.Empty);
        }

        public static double ToDouble(this string source)
        {
            return double.Parse(source.DefaultIfNullOrEmpty("0"));
        }

        public static decimal ToDecimal(this string source)
        {
            return decimal.Parse(source.DefaultIfNullOrEmpty("0"));
        }

        public static float ToFloat(this string source)
        {
            return float.Parse(source.DefaultIfNullOrEmpty("0"));
        }

        public static int ToInt(this string source)
        {
            return int.Parse(source.DefaultIfNullOrEmpty("0"));
        }


        public static long ToLong(this string source)
        {
            return long.Parse(source.DefaultIfNullOrEmpty("0"));
        }


        /// <summary>
        /// Return a string from a fixed field string at a given position, startIndex, and given length
        /// </summary>
        public static string ParseFixedFld(this string source, int startIndex, int length)
        {
            return source.Substring(startIndex, length).Trim();
        }

        public static String DefaultIfEmpty(this string source, string defValue)
        {
            return source.Trim().IsEmpty() ? defValue : source.Trim();
        }

        public static string DefaultIfNullOrEmpty(this string source, string defaultString)
        {
            return string.IsNullOrEmpty(source) ? defaultString : source;
        }

        public static string DefaultIfNullOrWhiteSpace(this string source, string defaultString)
        {
            return string.IsNullOrWhiteSpace(source) ? defaultString : source;
        }

        /// <summary>
        /// Converts a string to DateTime and if string is empty then returns 01/01/1900 12:00:00 AM.
        /// </summary>
        public static DateTime DefaultDateIfEmpty(this string source)
        {
            return source.Trim().IsEmpty() ? DefaultDate() : DateTime.Parse(source.Trim());
        }

        /// <summary>
        /// Returns 01/01/1900 12:00:00 AM.
        /// </summary>
        public static DateTime DefaultDate()
        {
            return DateTime.Parse("January 01, 1900 12:00:00 AM");
        }

        public static string RemoveNonNumericCharacters(this string source)
        {
            return Regex.Replace(source, "[^0-9]", string.Empty);
        }

        public static string RemoveNonNumericCharactersExcludingPeriods(this string source)
        {
            return Regex.Replace(source, "[^0-9|^.]", string.Empty);
        }

        public static string RemoveNonNumericCharactersExcludingCommas(this string source)
        {
            return Regex.Replace(source, "[^0-9|^,]", string.Empty);
        }


        public static string RemoveNonNumericCharactersExcludingCustomChar(this string value, string excludedChar)
        {
            return Regex.Replace(value, "[^0-9|^" + excludedChar + "]", string.Empty);
        }

        public static string RemoveNumericCharacters(this string value)
        {
            return Regex.Replace(value, "[0-9]", "");
        }

        /// <summary>
        ///  Removes the extended ascii double quotes and replaces them
        ///  with the non-extended ascii double quotes
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <remarks></remarks>

        public static string ReplaceExtAsciiDblQuotes(this string source)
        {
            if (source.IsEmpty())
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            List<char> chars = source.ToList();
            for (int index = 0; index <= chars.Count - 1; index++)
            {
                if ((int)chars[index] == 147 || (int)chars[index] == 148)
                {
                    chars[index] = Convert.ToChar("\"");
                }
                sb.Append(chars[index]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Check if string is a valid date.
        /// </summary>
        public static bool IsDate(this string source)
        {
            if (!source.IsEmpty())
            {
                DateTime dt;
                return (DateTime.TryParse(source, out dt));
            }
            return false;
        }

        /// <summary>
        /// Removes all control line feed and tab characters: \n\r\t
        /// </summary>
        public static string RemoveCrLfTab(this string source)
        {
            return Regex.Replace(source, "^[+\n\r\t]", string.Empty);
        }

        /// <summary>
        /// Removes all control line feed characters: \n\r
        /// </summary>
        public static string RemoveCrLf(this string source)
        {
            return Regex.Replace(source, "^[+\n\r]", string.Empty);
        }

        /// <summary>
        /// Removes all tab characters
        /// </summary>
        public static string RemoveTab(this string source)
        {
            return Regex.Replace(source, @"^[+\t]", string.Empty);
        }

        /// <summary>
        /// Appends the given value separated by the given separator
        /// </summary>
        public static string AddSeparator(this string source, string valueToAppend, char separator)
        {
            if (valueToAppend.IsEmpty()) return source;
            if (!source.IsEmpty())
            {
                return source + separator + valueToAppend;
            }
            return valueToAppend;
        }

        /// <summary>
        /// Short for IsNullOrEmpty
        /// </summary>
        public static bool IsEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static string ToMySqlWindowsPath(this string source)
        {
            return source.Replace(@"\", @"/");
        }

        /// <summary>
        /// Removes the last occurrence of a given value.
        /// </summary>
        public static string RemoveLastOccurrenceOf(this string source, string valueToRemove)
        {
            int idxOfOccurrence = source.LastIndexOf(valueToRemove, StringComparison.Ordinal);
            if (idxOfOccurrence < 0)
            {
                idxOfOccurrence = source.Length - 1;
            }
            return source.Substring(0, idxOfOccurrence);
        }

        /// <summary>
        /// Return the string quoted if contains given delimiter
        /// </summary>
        public static string EncloseInQuotes(this string source, string delimiter)
        {
            if (source.Contains(delimiter))
            {
                return source.ToQuoted();
            }
            return source;
        }

        /// <summary>
        /// Return the string quoted.
        /// </summary>
        public static string ToQuoted(this string source)
        {
            return string.Format(@"""{0}""", source);
        }

        /// <summary>
        /// Converts a string to byte[].
        /// </summary>
        public static byte[] ToByteArray(this string source)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetBytes(source);
        }

        /// <summary>
        /// Return the abbreviation of a given string by removing all vowels.
        /// </summary>
        public static String Abbrev(this String source)
        {
            if (source.IsEmpty())
            {
                return source;
            }
            string firstLetter = source.Left(1);
            List<char> noVowels = new List<char>(source.Right(source.Length - 1));
            noVowels.RemoveAll(c => "aeiou".Contains(c.ToString().ToLower()));
            return firstLetter + string.Join("", noVowels);
        }

        //Credit goes to ScubaSteve's answer: https://stackoverflow.com/questions/206717/how-do-i-replace-multiple-spaces-with-a-single-space-in-c
        //Performs better than Regex and more effecient than using this -> while(str.Contains("  ")) str = str.Replace("  ", " ");
        /// <summary>
        /// Reduces multiple occurrences of whitespace down to one.
        ///Performs better than Regex and more effecient than using this -> while(str.Contains("  ")) str = str.Replace("  ", " ");
        /// </summary>
        public static String ReduceWhitespace(this String source)
        {
            var result = new StringBuilder();
            bool previousCharWasWhitespace = false;
            for (int i = 0; i < source.Length; i++)
            {
                if (Char.IsWhiteSpace(source[i]))
                {
                    if (previousCharWasWhitespace)
                    {
                        continue;
                    }

                    previousCharWasWhitespace = true;
                }
                else
                {
                    previousCharWasWhitespace = false;
                }

                result.Append(source[i]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Implement's VB's Like operator logic.
        /// </summary>
        public static bool IsLike(this string s, string pattern)
        {
            // Characters matched so far
            int matched = 0;

            // Loop through pattern string
            for (int i = 0; i < pattern.Length; )
            {
                // Check for end of string
                if (matched > s.Length)
                    return false;

                // Get next pattern character
                char c = pattern[i++];
                if (c == '[') // Character list
                {
                    // Test for exclude character
                    bool exclude = (i < pattern.Length && pattern[i] == '!');
                    if (exclude)
                        i++;
                    // Build character list
                    int j = pattern.IndexOf(']', i);
                    if (j < 0)
                        j = s.Length;
                    HashSet<char> charList = CharListToSet(pattern.Substring(i, j - i));
                    i = j + 1;

                    if (charList.Contains(s[matched]) == exclude)
                        return false;
                    matched++;
                }
                else if (c == '?') // Any single character
                {
                    matched++;
                }
                else if (c == '#') // Any single digit
                {
                    if (!Char.IsDigit(s[matched]))
                        return false;
                    matched++;
                }
                else if (c == '*') // Zero or more characters
                {
                    if (i < pattern.Length)
                    {
                        // Matches all characters until
                        // next character in pattern
                        char next = pattern[i];
                        int j = s.IndexOf(next, matched);
                        if (j < 0)
                            return false;
                        matched = j;
                    }
                    else
                    {
                        // Matches all remaining characters
                        matched = s.Length;
                        break;
                    }
                }
                else // Exact character
                {
                    if (matched >= s.Length || c != s[matched])
                        return false;
                    matched++;
                }
            }
            // Return true if all characters matched
            return (matched == s.Length);
        }

        /// <summary>
        /// Converts a string of characters to a HashSet of characters. If the string
        /// contains character ranges, such as A-Z, all characters in the range are
        /// also added to the returned set of characters.
        /// </summary>
        /// <param name="charList">Character list string</param>
        private static HashSet<char> CharListToSet(string charList)
        {
            HashSet<char> set = new HashSet<char>();

            for (int i = 0; i < charList.Length; i++)
            {
                if ((i + 1) < charList.Length && charList[i + 1] == '-')
                {
                    // Character range
                    char startChar = charList[i++];
                    i++; // Hyphen
                    char endChar = (char)0;
                    if (i < charList.Length)
                        endChar = charList[i++];
                    for (int j = startChar; j <= endChar; j++)
                        set.Add((char)j);
                }
                else set.Add(charList[i]);
            }
            return set;
        }

        public static string FormatEmailForHref(this string source, string emailSubject)
        {
            string emailAddr = source;
            string emailAddrHref = string.Format(@"mailto:{0}?Subject={1}:", emailAddr, emailSubject);
            return emailAddrHref;
        }


        public static string FormatPhoneForHref(this string source)
        {
            string phoneNum = source.RemoveNonNumericCharacters();
            if (phoneNum.IsPhoneTollFree())
            {
                phoneNum = phoneNum.StartsWith("1", StringComparison.Ordinal) ? string.Empty : "1" + phoneNum;
            }
            phoneNum = "tel:+" + phoneNum;
            return phoneNum;
        }


        /// <summary>
        /// Returns phone number string formatted as "(###) ###-####"
        /// </summary>
        public static string FormatPhoneForDisplay(this string source)
        {
            string phoneFormat = "(###) ###-####";
            bool isTollFree = false;
            string phoneNum = source.Trim().RemoveNonNumericCharacters();
            int numberLength = phoneNum.Length;
            isTollFree = phoneNum.IsPhoneTollFree();
            if (numberLength > 0)
            {
                if (numberLength > 10)
                {
                    phoneNum = phoneNum.Substring(1, numberLength - 1);
                }
                phoneNum = Convert.ToInt64(phoneNum).ToString(phoneFormat);
            }
            if (isTollFree)
            {
                phoneNum = "1 " + phoneNum;
            }
            return phoneNum;
        }


        /// <summary>
        /// Checks if a phone number is toll free.
        /// </summary>
        public static bool IsPhoneTollFree(this string source)
        {
            bool result = false;
            string phoneNum = source.RemoveNonNumericCharacters();
            string tollFreeExchanges = "800 844 855 866 877 888";
            if ((!phoneNum.StartsWith("1", StringComparison.Ordinal) && tollFreeExchanges.Contains(phoneNum.Left(3))) || (phoneNum.StartsWith("1", StringComparison.Ordinal) && tollFreeExchanges.Contains(phoneNum.Substring(1, 3))))
            {
                result = true;
            }
            return result;
        }


        public static string SqlInjectionFilter(this string source)
        {
            return source.Replace(",", string.Empty).Replace(";", string.Empty);
        }

        public static string FixSql(this string source)
        {
            return source.Replace("'", "''");
        }

        /// <summary>
        /// Returns a string padded left with zero(s) given the desired length.
        /// </summary>
        public static string PadWithZero(this string source, int length)
        {
            return source.DefaultIfNullOrWhiteSpace(string.Empty).Trim().PadLeft(length, '0');
        }

        /// <summary>
        /// Returns a string padded left with letter A(s) given the desired length.
        /// </summary>
        public static string PadWithA(this string source, int length)
        {
            return source.DefaultIfNullOrWhiteSpace(string.Empty).Trim().PadLeft(length, 'A');
        }


        /// <summary>
        /// Returns a title case string
        /// </summary>
        public static string ToTitleCase(this string source, string cultureName = "en-US")
        {
            System.Globalization.TextInfo txtInfo = new System.Globalization.CultureInfo(cultureName, false).TextInfo;
            return txtInfo.ToTitleCase(source.ToLower());
        }

        /// <summary>
        /// Returns a string formatted as money {0:C?} given the number of decimals.
        /// </summary>
        public static string FormatToMoney(this string source)
        {
            return source.ToDecimal().FormatToMoney();
        }

        public static string FormatToInt(this string source)
        {
            return source.ToInt().Formatted();
        }

        /// <summary>
        /// Returns the input string with the first character converted to uppercase
        /// </summary>
        public static string FirstLetterToUpperCase(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            char[] a = source.ToCharArray();
            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }

        /// <summary>
        /// Returns a FileInfo object from a filePath if exists.
        /// </summary>
        public static FileInfo ToFileInfo(this string filePath)
        {
            if (filePath.IsEmpty()) {
                return null;
            }

            return new FileInfo(filePath);
        }

        /// <summary>
        /// Converts the pascal-cased string to a displayable string
        /// </summary>
        public static string ToDisplayable(this string value)
        {
            for (int i = value.Length - 1; i >= 0; i--)
            {
                if (value[i] == char.ToUpper(value[i]) && i > 0)
                {
                   value = value.Insert(i, " ");
                }
            }

            return value;
        }

        /// <summary>
        /// Compare string equality.
        /// </summary>
        /// <param name="comparee">string to compare.</param>
        /// <param name="ignoreCase">default is true. Set to false for case sensitive compare.</param>
        /// <returns></returns>
        public static bool IsEqualTo(this string value, string comparee, bool ignoreCase = true)
        {
            return string.Compare(value, comparee, ignoreCase) == 0;
        }

        public static bool IsNumber(this string value)
        {
            int parsedInt = 0;
            return int.TryParse(value, out parsedInt);
        }

        public static string AlignLeft(this string value, int totWidth)
        {
            return value.PadRight(totWidth);
        }

        public static string AlignRight(this string value, int totWidth)
        {
            return value.PadLeft(totWidth);
        }



    }
}
