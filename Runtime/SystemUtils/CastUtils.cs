//
// Author: Alessandro Salani (Cippo)
//

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Utils
{
    public static class CastUtils
    {
        private static readonly string LogName = $"[{nameof(CastUtils)}]: ";
        
        /// <summary>
        /// Tries to cast an object to another.
        /// If it fails return false and logs an error. 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryCast<T>(object o, out T value)
        {
            try
            {
                value = (T) o;
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }
        
        /// <summary>
        /// Tries to cast an object to another.
        /// If it fails return false and logs an error. 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="value"></param>
        /// <param name="debug"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryCast<T>(object o, out T value, Object debug)
        {
            try
            {
                value = (T) o;
                return true;
            }
            catch (Exception e)
            {
                string logName = debug != null ? StringUtils.LogName(debug) : LogName;
                Debug.LogError(logName+$"{nameof(TryCast)} failed. Caught exception {e.Message}.", debug);
                value = default(T);
                return false;
            }
        }

        /// <summary>
        /// Casts an object to type T. In case of failure returns T default value. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToOrDefault<T>(object target)
        {
            try
            {
                return (T) target;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Returns true if successfully casted to T.
        /// Casts an object to type T.
        /// In case of failure returns false and T default value.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>success</returns>
        public static bool To<T>(object target, out T result)
        {
            try
            {
                result = (T) target;
                return true;
            }
            catch
            {
                result = default(T);
                return false;
            }
        }
    }
}
