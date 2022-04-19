using System.Collections.Generic;
using System.Linq;

namespace CippSharp.Core
{
    using Array = System.Array;
    using Type = System.Type;
    using Exception = System.Exception;
    using Debug = UnityEngine.Debug;
    
    /// <summary>
    /// Hold static helpful methods for arrays.
    /// </summary>
    public static partial class ArrayUtils
    {
        private static readonly string LogName = $"[{nameof(ArrayUtils)}]: ";
//        private const string ArrayGenericError = "Failed for ";
        
        
        //This part is dedicated to topmost generic arrays methods
        #region Array Generic → Conversions and IsArray

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
        
        #region Array To → Conversions

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
    }
}
