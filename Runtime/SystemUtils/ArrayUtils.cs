using System;
using System.Collections.Generic;
using System.Linq;
using Debug = UnityEngine.Debug;

namespace CippSharp.Core.Utils
{
    /// <summary>
    /// Hold static helpful methods for arrays.
    /// </summary>
    public static class ArrayUtils
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
                Debug.LogError(LogName+ $"{nameof(TryToObjectArray)} failed to cast object to object[]. Caught exception: {e.Message}.");
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
        /// From list of Keys and Values to Dictionary
        /// 
        /// Warning:
        /// - keys and values MUST have the same length
        /// - keys MUST NOT have duplicates
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            Dictionary<TKey, TValue> newDictionary = new Dictionary<TKey, TValue>();
            
            for (int i = 0; i < keys.Count; i++)
            {
                newDictionary[keys[i]] = values[i];
            }

            return newDictionary;
        }

        /// <summary>
        /// To Dictionary from an IEnumerable of KeyValuePairs of same Types as Dictionary
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> array)
        {
            Dictionary<TKey, TValue> newDictionary = new Dictionary<TKey, TValue>();
            foreach (var keyValuePair in array)
            {
                newDictionary[keyValuePair.Key] = keyValuePair.Value;
            }
            return newDictionary;
        }
        
        
        /// <summary>
        /// From list of Keys and Values to Array of KeyValuePairs
        ///
        /// Warning: keys and values MUST have the same length
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static KeyValuePair<TKey, TValue>[] ToArray<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            int length = keys.Count;
            KeyValuePair<TKey, TValue>[] newArray = new KeyValuePair<TKey, TValue>[length];
            for (int i = 0; i < length; i++)
            {
                newArray[i] = new KeyValuePair<TKey, TValue>(keys[i], values[i]);
            }

            return newArray;
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
            ICollection<T> collection = enumerable is ICollection<T> c ? c : enumerable.ToArray();
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

        #region → Any, Contains or Find Element

        /// <summary>
        /// Similar to Any of <see> <cref>System.linq</cref> </see>
        /// it retrieve a valid index of the first element matching the predicate.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Any<T>(List<T> list, Predicate<T> predicate, out int index)
        {
            index = IndexOf(list, predicate);
            return index > -1;
        }
        
        /// <summary>
        /// Similar to Any of <see> <cref>System.linq</cref> </see>
        /// it retrieve a valid index of the first element matching the predicate.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="predicate"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Any<T>(T[] array, Predicate<T> predicate, out int index)
        {
            index = IndexOf(array, predicate);
            return index > -1;
        }

        /// <summary>
        /// The enumerable contains an element with predicate?
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Contains<T>(IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            return enumerable.Any(predicate.Invoke);
        }

        /// <summary>
        /// Find element in collection
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>success</returns>
        public static bool Find<T>(ICollection<T> collection, Predicate<T> predicate, out T result)
        {
            foreach (var element in collection)
            {
                if (!predicate.Invoke(element))
                {
                    continue;
                }
                
                result = element;
                return true;
            }
            
            result = default;
            return false;
        }
        
        #endregion
        
        #region → Element At

        /// <summary>
        /// Raw element at or default for arrays
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ElementAtOrDefault<T>(T[] array, int index)
        {
            try
            {
                return array[index];
            }
            catch
            {
                return default(T);
            }
        }

        #endregion

        #region → Has Duplicates
        
        /// <summary>
        /// Has Duplicates?
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasDuplicates<T>(IEnumerable<T> enumerable) 
        {
            HashSet<T> hs = new HashSet<T>();
            return enumerable.Any(t => !hs.Add(t));
        }

        #endregion
        
        #region → Index Of Element

        /// <summary>
        /// Retrieve index if array contains an element with given predicate.
        /// Otherwise -1
        /// </summary>
        /// <param name="array"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>-1 if it fails</returns>
        public static int IndexOf<T>(T[] array, Predicate<T> predicate)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate.Invoke(array[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// Retrieve index if list contains an element with given predicate.
        /// Otherwise -1
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>-1 if it fails</returns>
        public static int IndexOf<T>(List<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate.Invoke(list[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// Retrieve index if enumerable contains an element with given predicate.
        /// Otherwise -1
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int IndexOf<T>(IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            T[] array = enumerable.ToArray();
            return IndexOf(array, predicate);
        }

        #endregion
        
        #region → Is Null or Empty
        
        /// <summary>
        /// Returns true if the given array is null or empty
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(T[] array)
        {
            return array == null || array.Length < 1;
        }
        
        /// <summary>
        /// Returns true if the given list is null or empty
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(List<T> list)
        {
            return list == null || list.Count < 1;
        }

        /// <summary>
        /// Returns true if the given dictionary is null or empty
        /// </summary>
        /// <param name="dictionary"></param>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<K, V>(Dictionary<K, V> dictionary)
        {
            return dictionary == null || dictionary.Count < 1;
        }

        /// <summary>
        /// Returns true if the given collection is null or empty
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(ICollection<T> collection)
        {
            return collection == null || collection.Count < 1;
        }
        
        /// <summary>
        /// Returns true if the given enumerable is null or empty
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        #endregion
        
        #region → Is Valid Index

        /// <summary>
        /// Returns true if the given index is the array range.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsValidIndex<T>(int index, T[] array)
        {
            return index >= 0 && index < array.Length;
        }
        
        /// <summary>
        /// Returns true if the given index is in the list range.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsValidIndex<T>(int index, List<T> list)
        {
            return index >= 0 && index < list.Count;
        }

        /// <summary>
        /// Returns true if the given index is in the list range.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsValidIndex<T>(int index, IEnumerable<T> enumerable)
        {
            return index >= 0 && index < enumerable.Count();
        }

        /// <summary>
        /// Returns true if the given index is in the range 0-count
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool IsValidIndex(int index, int count)
        {
            return index >= 0 && index < count;
        }

        #endregion
        
        
        #region → Random Element

        /// <summary>
        /// Retrieve a random element in array.
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomElement<T>(T[] array)
        {
            int index = UnityEngine.Random.Range(0, array.Length);
            return array[index];
        }
        
        /// <summary>
        /// Retrieve a random element in list.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomElement<T>(List<T> list)
        {
            int index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }

        #endregion
        
        #region → Remove Element

        /// <summary>
        /// Remove an array element at the given index and retrieve the resulting array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RemoveAt<T>(ref T[] array, int index)
        {
            array = array.Where((e, i) => i != index).ToArray();
        }
        
        /// <summary>
        /// Remove an element from array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] Remove<T>(T[] array, T element)
        {
            return array.Where(e => (e.Equals(element) == false)).ToArray();
        }
        
        /// <summary>
        /// Remove from a list by predicate
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        public static void Remove<T>(List<T> list, Predicate<T> predicate)
        {
            int index = IndexOf(list, predicate);
            if (index > -1)
            {
                list.RemoveAt(index);
            }
        }

        #endregion
        
        
        #region → Select

        /// <summary>
        /// Select If util. Similar to System.linq Select but with a predicate to check.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <returns></returns>
        public static IEnumerable<F> SelectIf<T, F>(IEnumerable<T> enumerable, Predicate<T> predicate, Func<T, F> func)
        {
            return (from element in enumerable where predicate.Invoke(element) select func.Invoke(element));
        }
        
        /// <summary>
        /// Select Many If predicate. Similar to System.linq Select but with a predicate to check.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <returns></returns>
        public static IEnumerable<F> SelectManyIf<T, F>(IEnumerable<T> enumerable, Predicate<T> predicate, Func<T, IEnumerable<F>> func)
        {
            List<F> many = new List<F>();
            foreach (var element in enumerable)
            {
                if (!predicate.Invoke(element))
                {
                    continue;
                }
                
                many.AddRange(func.Invoke(element));
            }
            return many;
        }
        
        /// <summary>
        /// Select not null elements from an enumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> SelectNotNullElements<T>(IEnumerable<T> enumerable) where T : class
        {
            return enumerable.Where(e => e != null);
        }

        #endregion
        
        //strings are char[]... so arrays can have strings utils
        #region → Sub Array

        /// <summary>
        /// Same as substring, but for arrays.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SubArrayOrDefault<T>(T[] array, int index, int length)
        {
            return TrySubArray(array, index, length, out T[] subArray) ? subArray : default;
        }

        /// <summary>
        /// Same as substring, but for arrays.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SubArrayOrDefault<T>(ICollection<T> collection, int index, int length)
        {
            return TrySubArray(collection.ToArray(), index, length, out T[] subArray) ? subArray : default;
        }

        /// <summary>
        /// Try to get a subArray from an array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="subArray"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>success</returns>
        public static bool TrySubArray<T>(T[] array, int index, int length, out T[] subArray)
        {
            try
            {
                T[] result = new T[length];
                Array.Copy(array, index, result, 0, length);
                subArray = result;
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(LogName+$"{nameof(TrySubArray)} failed to get SubArray. Caught exception: {e.Message}.");
                subArray = null;
                return false;
            }
        }
        
        #endregion
        
        #region → Take

        /// <summary>
        /// Take until count!
        /// If there aren't enough elements only the few (less than count) are returned.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> TakeUntilOrLess<T>(IEnumerable<T> enumerable, int count)
        {
            ICollection<T> collection = enumerable is ICollection<T> c ? c : enumerable.ToArray();
            int collectionCount = collection.Count;
            return collectionCount <= count ? collection : collection.Take(count);
        }

        #endregion
        
        #endregion
    }
}
