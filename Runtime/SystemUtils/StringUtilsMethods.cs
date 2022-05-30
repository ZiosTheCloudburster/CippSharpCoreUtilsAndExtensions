using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CippSharp.Core.Utils
{
    using Color = UnityEngine.Color;
    using ColorUtility = UnityEngine.ColorUtility;
    
    public static class StringUtils
    {
        #region → Log Name

        /// <summary>
        /// Retrieve a more contextual name for logs, based on typeName.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static string LogName(string typeName)
        {
            return string.Format("[{0}]: ", typeName);
        }
        
        /// <summary>
        /// Retrieve a more contextual name for logs, based on type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string LogName(Type type)
        {
            return string.Format("[{0}]: ", type.Name);
        }

        /// <summary>
        /// Retrieve a more contextual name for logs, based on type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string LogName(Type type, Color color)
        {
            return string.Format("[{0}]: ", OfColor(type.Name, color));
        }

        /// <summary>
        /// Retrieve a more contextual name for logs, based on object.
        /// If object is null an empty string is returned.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string LogName(object context)
        {
            return ((object)context == null) ? string.Empty : LogName(context.GetType());
        }
        
        #endregion

        //Chars Utils
        #region Generic String → Chars Utils

        /// <summary>
        /// Add spaces before capital letters
        /// 
        /// https://stackoverflow.com/questions/272633/add-spaces-before-capital-letters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AddSpacesBeforeCapitalLetters(string value)
        {
            return Regex.Replace(value, "([a-z])([A-Z])", "$1 $2");
        }
        
        /// <summary>
        /// Put the first valid Char of a string to UpperCase after specific chars.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string CapitalizeAfter(string s, IEnumerable<char> chars)
        {
            var charsHash = new HashSet<char>(chars);
            StringBuilder sb = new StringBuilder(s);
            for (int i = 0; i < sb.Length - 2; i++)
            {
                if (charsHash.Contains(sb[i]) && sb[i + 1] == ' ')
                    sb[i + 2] = char.ToUpper(sb[i + 2]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Ensure that a string ends with a certain char.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string EnsureEndingChar(string input, char c)
        {
            if (input.EndsWith(c.ToString()))
            {
                return input;
            }
            else
            {
                return (input + c);
            }
        }
        
        #region → Firt Char to
        
        /// <summary>
        /// Put the first Char of a string to UpperCase
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return string.Format("{0}{1}", value.First().ToString().ToUpper(), value.Substring(1));
        }
        
        /// <summary>
        /// Put the first Char of a string to LowerCase
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToLower(string input)
        {
            string newString = input;
            if (!string.IsNullOrEmpty(newString) && char.IsUpper(newString[0]))
            {
                newString = char.ToLower(newString[0]) + newString.Substring(1);
            }
            return newString;
        }
        
        #endregion
        
        #endregion
        
        #region String → Methods
        
        /// <summary>
        /// Add a string to another
        /// </summary>
        /// <returns></returns>
        public static string Add(string value, string other)
        {
            return string.Concat(value, other);
        }

        #region → Contains and Equal
        
        /// <summary>
        /// The value contains Any of the strings?
        /// </summary>
        /// <param name="value"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static bool ContainsAnyString(string value, ICollection<string> strings)
        {
            return strings.Any(value.Contains);
        }

        /// <summary>
        /// Is any string == the other?
        /// </summary>
        /// <param name="value"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static bool EqualAnyString(string value, ICollection<string> strings)
        {
            return strings.Any(s => s == value);
        }

        #endregion
        
        /// <summary>
        /// Encode bytes in UTF8 string
        /// </summary>
        /// <param name="bytes">must be not null</param>
        /// <returns></returns>
        public static string EncodeBytes(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);;
        }
        
        /// <summary>
        /// If a string ends with number this code increments that
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IncrementEndingNumber(string value)
        {
            var r = value;
            var name = value;
            string num = "";
            for (int k = 1; k < name.Length; k++)
            {
                if(int.TryParse(name.Substring(name.Length - k), out int n))
                {
                    num = name.Substring(name.Length - k);
                }
            }

            if (num != "")
            {
                r = r.Substring(0, name.Length - num.Length) + (int.Parse(num) + 1);
            }
            else
            {
                r += "1";
            }
            
            return r;
        }
        
        /// <summary>
        /// Retrieve a line as multiline with tabs
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tabLevel"></param>
        /// <param name="maxCharactersPerLine"></param>
        /// <returns></returns>
        public static string MultiLineTab(string value, int tabLevel, int maxCharactersPerLine = 50)
        {
            string tabs = string.Empty;
            for (int i = 0; i < tabLevel; i++)
            {
                tabs += "\t";
            }

            string finalValue = "";
            
            if (value.Length > maxCharactersPerLine)
            {
                string[] valueSplitResult = Split(value, maxCharactersPerLine).ToArray();
                for (int i = 0; i < valueSplitResult.Length; i++)
                {
                    string valueSplit = valueSplitResult[i];
                    finalValue += $"{tabs}{valueSplit}{Environment.NewLine}";
                }
            }
            else
            {
                finalValue = $"{tabs}{value}{Environment.NewLine}";
            }

            return finalValue;
        }
        
        /// <summary>
        /// Zero padding string.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string PaddingFormat(int amount)
        {
            string s = "";
            for (int i = 0; i < amount; i++)
            {
                s += "0";
            }

            return s;
        }

        #region → Split
          
        /// <summary>
        /// Perform the split only if it is possible, otherwise retrieve
        /// an array of length 1 with value as element.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="strings"></param>
        /// <param name="splitOptions"></param>
        /// <returns></returns>
        public static string[] CheckedSplit(string value, string[] strings, StringSplitOptions splitOptions)
        {
            if (string.IsNullOrEmpty(value))
            {
                return splitOptions.HasFlag(StringSplitOptions.RemoveEmptyEntries) ? new string[0] : new[] {value};
            }

            if (ArrayUtils.IsNullOrEmpty(strings))
            {
                return new[] {value};
            }

            if (!ContainsAnyString(value, strings))
            {
                return new[] {value};
            }

            return value.Split(strings, splitOptions);
        }
        
        /// <summary>
        /// Split string into chunks of specified length.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<string> Split(string str, int chunkSize = 50)
        { 
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
        
        
        #endregion
        
        /// <summary>
        /// Write a string array as flat string
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToFlatArray(IEnumerable<string> enumerable, string separator = "")
        {
            ICollection<string> collection = enumerable is ICollection<string> c ? c : enumerable.ToArray();
            string last = collection.Last();
            return collection.Aggregate(string.Empty, (current, s) => current + $"{s}{(s != last ? separator : string.Empty)}");
        }
        
        #region → Predicates
       
        /// <summary>
        /// Return true if a string is equal to, or contains value
        /// </summary>
        /// <returns></returns>
        public static bool EqualOrContains(string input, string value)
        {
            return (input == value) || (input.Contains(value));
        }
        
        /// <summary>
        /// Is this value string contained in any of string array?
        /// </summary>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsContained(string value, string[] values)
        {
            return values.Any(s => s == value);
        }
        
        #endregion
        
        #region → Remove and Replace

        /// <summary>
        /// Remove special characters from a string 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(string input, string replace = "")
        {
            Regex reg = new Regex("[*'\",_&#^@]");
            input = reg.Replace(input, replace);

            Regex reg1 = new Regex("[ ]");
            input = reg1.Replace(input, replace);
            return input;
        }

        /// <summary>
        /// Replace last occurrence of match.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="match"></param>
        /// <param name="replace"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ReplaceLastOccurrence(string source, string match, string replace, StringComparison culture = StringComparison.InvariantCulture)
        {
            int place = source.LastIndexOf(match, culture);

            if (place == -1)
            {
                return source;
            }

            string result = source.Remove(place, match.Length).Insert(place, replace);
            return result;
        }
        
        /// <summary>
        /// Replace empty lines
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceEmptyLine(string lines, string replace = "")
        {
            return Regex.Replace(lines, @"^\s*$\n|\r", replace, RegexOptions.Multiline).TrimEnd();
        }
        
        /// <summary>
        /// Replace new lines
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceNewLine(string lines, string replace = "")
        {
            return Regex.Replace(lines, @"\r\n?|\n", replace, RegexOptions.Multiline).TrimEnd();
        }
        
        
        /// <summary>
        /// Replace any of the values with the replace value.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="values"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceAny(string input, string[] values, string replace = "")
        {
            return values.Aggregate(input, (current, t) => current.Replace(t, replace));
        }

        #endregion
        
        #endregion

        
        #region Unity → Rich Text

         /// <summary>
        /// Add italic.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Italic(string value)
        {
            return string.Format("<i>{0}</i>", value);
        }

        /// <summary>
        /// Remove italic.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnItalic(string value)
        {
            return value.Replace("<i>", string.Empty).Replace("</i>", string.Empty);
        }

        /// <summary>
        /// Add bold
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Bold(string value)
        {
            return string.Format("<b>{0}</b>", value);
        }

        /// <summary>
        /// Remove bold
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnBold(string value)
        {
            return value.Replace("<b>", string.Empty).Replace("</b>", string.Empty);
        }

        /// <summary>
        /// Set the string to that color.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string OfColor(string value, Color color)
        {
            return string.Format("<color=#{1}>{0}</color>", value, ColorUtility.ToHtmlStringRGBA(color));
        }

        /// <summary>
        /// Remove the specific color from a string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string UnColor(string value, Color color)
        {
            return value.Replace(string.Format("<color=#{0}>", ColorUtility.ToHtmlStringRGBA(color)), string.Empty).Replace("</color>", string.Empty);
        }

        /// <summary>
        /// Remove any color from a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnColor(string value)
        {
            return Regex.Replace(value.Replace("</color>", string.Empty), "<color=#.*?>", string.Empty);
        }

        /// <summary>
        /// Set the char size to a specific value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string OfSize(string value, int size)
        {
            return string.Format("<size={1}>{0}</size>", value, size.ToString());
        }

        /// <summary>
        /// Remove the size from the string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnSize(string value)
        {
            return Regex.Replace(value.Replace("</size>", string.Empty), "<size=.*?>", string.Empty);
        }

        /// <summary>
        /// Remove any rich text utility from string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnRichTextString(string value)
        {
            return Regex.Replace(value, "<.*?>", string.Empty);
        }

        #endregion
        
    }
}
