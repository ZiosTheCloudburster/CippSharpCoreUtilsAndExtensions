using System;
using System.Collections.Generic;
using System.Linq;
using Debug = UnityEngine.Debug;

namespace CippSharp.Core
{
    /// <summary>
    /// Hold static helpful methods for arrays.
    /// </summary>
    public static partial class ArrayUtils
    {
        private static readonly string LogName = $"[{nameof(ArrayUtils)}]: ";
        
        
        //This part is dedicated to topmost generic arrays methods
        #region Array Generic → To Conversions and IsArray

        /// <summary>
        /// Retrieve if context type is an array.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsArray(Type type)
        {
            return type.IsArray;
        }

        /// <summary>
        /// Retrieve if context object is an array.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsArray(object context)
        {
            return IsArray(context.GetType());
        }
        
        
        /// <summary>
        /// Try to get element at index of object[]
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="element"></param>
        /// <returns>success</returns>
        public static bool TryGetValue(object[] array, int index, out object element)
        {
            try
            {
                element = array[index];
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(LogName+ $"{nameof(TryGetValue)} failed to get value. Caught exception: {e.Message}.");
                element = null;
                return false;
            }
        }

        /// <summary>
        /// Try to set value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="element"></param>
        /// <returns>success</returns>
        public static bool TrySetValue(object[] array, int index, object element)
        {
            try
            {
                array[index] = element;
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(LogName+ $"{nameof(TrySetValue)} failed to set value. Caught exception: {e.Message}.");
                return false;
            }
        }

        /// <summary>
        /// Try to cast an object to object[]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <returns>success</returns>
        public static bool TryToObjectArray(object value, out object[] array)
        {
            try
            {
                array = ((Array)value).Cast<object>().ToArray();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(LogName+ $"{nameof(TryToObjectArray)} failed. Caught exception: {e.Message}.");
                array = null;
                return false;
            }
        }
        
        /// <summary>
        /// Try to cast a generic Array to object[]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <returns>success</returns>
        public static bool TryToObjectArray(Array value, out object[] array)
        {
            try
            {
                array = (value).Cast<object>().ToArray();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(LogName+ $"{nameof(TryToObjectArray)} failed. Caught exception: {e.Message}.");
                array = null;
                return false;
            }
        }

        #endregion
        
        #region Array Typed → To Conversions

        /// <summary>
        /// To Dictionary from an IEnumerable of KeyValuePairs of same Types as Dictionary
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <returns></returns>
        public static Dictionary<T, F> ToDictionary<T, F>(IEnumerable<KeyValuePair<T, F>> array)
        {
            Dictionary<T, F> newDictionary = new Dictionary<T, F>();
            foreach (var keyValuePair in array)
            {
                newDictionary[keyValuePair.Key] = keyValuePair.Value;
            }
            return newDictionary;
        }
        
        #endregion

        #region Array → Iterators
        
        #region For
          
        /// <summary>
        /// Perform a referred for iteration on an array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void For<T>(T[] array, ForRefAction<T> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                T element = array[i];
                action.Invoke(ref element, i);
                array[i] = element;
            }
        }

        /// <summary>
        /// Perform a referred for iteration on a list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void For<T>(List<T> list, ForRefAction<T> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T element = list[i];
                action.Invoke(ref element, i);
                list[i] = element;
            }
        }

        /// <summary>
        /// Perform a referred for iteration on enumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> For<T>(IEnumerable<T> enumerable, ForRefAction<T> action)
        {
            T[] array = enumerable.ToArray();
            For(array, action);
            return array;
        }

        #endregion
        
        #region For Each
        
        /// <summary>
        /// Perform a foreach on an array, using System.Array.Foreach method
        /// </summary>
        /// <param name="array"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void ForEach<T>(T[] array, Action<T> action)
        {
            Array.ForEach(array, action);
        }

        /// <summary>
        /// Perform a foreach on a list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void ForEach<T>(List<T> list, Action<T> action)
        {
            foreach (var element in list)
            {
                action.Invoke(element);
            }
        }

        /// <summary>
        /// Perform a foreach on a collection
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ICollection<T> ForEach<T>(ICollection<T> collection, Action<T> action)
        {
            foreach (var element in collection)
            {
                action.Invoke(element);
            }
            
            return collection;
        }
        
        /// <summary>
        /// Perform a foreach on an enumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> ForEach<T>(IEnumerable<T> enumerable, Action<T> action)
        {
            ICollection<T> collection = (enumerable is ICollection<T> c) ? c : enumerable.ToArray();
            return ForEach(collection, action);
        }
        
        #endregion

        #endregion

        #region Array → Methods

        #region → Add

        /// <summary>
        /// Add an element to a list only if it is new
        /// </summary>
        /// <param name="list"></param>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddIfNew<T>(List<T> list, T element)
        {
            if (!list.Contains(element))
            {
                list.Add(element);
            }
        }

        #endregion
        
        #region → Clear

        /// <summary>
        /// Clear not null elements from an enumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("2021/08/14 → Use SelectNotNullElements instead. This will be removed in future versions.")]
        public static IEnumerable<T> ClearNullEntries<T>(IEnumerable<T> enumerable) where T : class
        {
            return SelectNotNullElements(enumerable);
        }

        #endregion

        #endregion
    }
}
