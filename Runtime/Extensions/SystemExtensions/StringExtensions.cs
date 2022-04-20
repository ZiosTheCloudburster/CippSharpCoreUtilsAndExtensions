﻿//
// Author: Alessandro Salani (Cippo)
//

using System;
using System.Collections.Generic;

namespace CippSharp.Core.Extensions
{
    using Color = UnityEngine.Color;
    using StringUtils = CippSharp.Core.Utils.StringUtils;
    
    public static class StringExtensions
    {
        //Chars Utils
        #region Generic String → Chars Utils
        
        /// <summary>
        /// Add spaces before capital letters
        /// 
        /// https://stackoverflow.com/questions/272633/add-spaces-before-capital-letters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AddSpacesBeforeCapitalLetters(this string value)
        {
            return StringUtils.AddSpacesBeforeCapitalLetters(value);
        }

        /// <summary>
        /// Put the first valid Char of a string to UpperCase after specific chars.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string CapitalizeAfter(this string s, IEnumerable<char> chars)
        {
            return StringUtils.CapitalizeAfter(s, chars);
        }
        
        /// <summary>
        /// Ensure that a string ends with a certain char.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string EnsureEndingChar(this string input, char c)
        {
            return StringUtils.EnsureEndingChar(input, c);
        }

        #region → Firt Char to
        
        /// <summary>
        /// Put the first Char of a string to UpperCase
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string input)
        {
            return StringUtils.FirstCharToUpper(input);
        }
        
        /// <summary>
        /// Put the first Char of a string to LowerCase
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToLower(this string input)
        {
            return StringUtils.FirstCharToLower(input);
        }
        
        #endregion
        
        #endregion
        
        #region String → Methods
        
        /// <summary>
        /// Add a string to another
        /// </summary>
        /// <returns></returns>
        public static string Add(this string value, string other)
        {
            return StringUtils.Add(value, other);
        }
        
        /// <summary>
        /// If a string ends with number this code increments that
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IncrementEndingNumber(this string value)
        {
            return StringUtils.IncrementEndingNumber(value);
        }
        
        /// <summary>
        /// Retrieve a line as multiline with tabs
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tabLevel"></param>
        /// <param name="maxCharactersPerLine"></param>
        /// <returns></returns>
        public static string MultiLineTab(this string value, int tabLevel, int maxCharactersPerLine = 50)
        {
            return StringUtils.MultiLineTab(value, tabLevel, maxCharactersPerLine);
        }

            
        /// <summary>
        /// Split string into chunks of specified length.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<string> Split(string str, int chunkSize = 50)
        {
            return StringUtils.Split(str, chunkSize);
        }
        
        /// <summary>
        /// Write a string enumerable as flat string
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToFlatArray(this IEnumerable<string> enumerable, string separator = "")
        {
            return StringUtils.ToFlatArray(enumerable, separator);
        }

        #region → Predicates
        
        /// <summary>
        /// Return true if a string is equal to, or contains value
        /// </summary>
        /// <returns></returns>
        public static bool EqualOrContains(this string input, string value)
        {
            return StringUtils.EqualOrContains(input, value);
        }

        /// <summary>
        /// Is this value string contained in any of string array?
        /// </summary>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsContained(string value, string[] values)
        {
            return StringUtils.IsContained(value, values);
        }

        #endregion
        
        #region → Remove and Replace
        
        /// <summary>
        /// Remove special characters from a string 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string input, string replace = "")
        {
            return StringUtils.RemoveSpecialCharacters(input, replace);
        }

        /// <summary>
        /// Replace last occurrence of match.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="match"></param>
        /// <param name="replace"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ReplaceLastOccurrence(this string source, string match, string replace, StringComparison culture = StringComparison.InvariantCulture)
        {
            return StringUtils.ReplaceLastOccurrence(source, match, replace, culture);
        }
        
        /// <summary>
        /// Replace empty lines.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="replace">string.Empty</param>
        /// <returns></returns>
        public static string ReplaceEmptyLine(this string lines, string replace = "")
        {
            return StringUtils.ReplaceEmptyLine(lines, replace);
        }
        
        /// <summary>
        /// Replace new lines 
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceNewLine(this string lines, string replace = "")
        {
            return StringUtils.ReplaceNewLine(lines, replace);
        }
        
        /// <summary>
        /// Replace any of the values with the replace value.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="values"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceAny(this string input, string[] values, string replace = "")
        {
            return StringUtils.ReplaceAny(input, values, replace);
        }

        #endregion
        
        #endregion
        
        
        #region Unity → Rich Text

        /// <summary>
        /// Add italic.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Italic(this string value)
        {
            return StringUtils.Italic(value);
        }

        /// <summary>
        /// Remove italic.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnItalic(this string value)
        {
            return StringUtils.UnItalic(value);
        }

        /// <summary>
        /// Add bold
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Bold(this string value)
        {
            return StringUtils.Bold(value);
        }

        /// <summary>
        /// Remove bold
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnBold(this string value)
        {
            return StringUtils.UnBold(value);
        }

        /// <summary>
        /// Set the string to that color.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string OfColor(this string value, Color color)
        {
            return StringUtils.OfColor(value, color);
        }

        /// <summary>
        /// Remove the specific color from a string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string UnColor(this string value, Color color)
        {
            return StringUtils.UnColor(value, color);
        }

        /// <summary>
        /// Remove any color from a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnColor(this string value)
        {
            return StringUtils.UnColor(value);
        }

        /// <summary>
        /// Set the char size to a specific value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string OfSize(this string value, int size)
        {
            return StringUtils.OfSize(value, size);
        }

        /// <summary>
        /// Remove the size from the string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnSize(this string value)
        {
            return StringUtils.UnSize(value);
        }

        /// <summary>
        /// Remove any rich text utility from string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnRichTextString(this string value)
        {
            return StringUtils.UnRichTextString(value);
        }
       
        #endregion

       

       
    }
}
