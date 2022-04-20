using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Utils
{
    public static class CodeUtils
    {
        private static readonly string LogName = $"[{nameof(CodeUtils)}]: ";
        
        #region Generic → Try

        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static bool Try(object element, Action action, Object debug)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch (Exception e)
            {
                string logName = debug != null ? StringUtils.LogName(debug) : LogName;
                Debug.LogError(logName+$"{nameof(Try)} failed. Caught exception {e.Message}.", debug);
                return false;
            }
        }

        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool Try(object element, Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Typed → Try On 'T' Reference

        /// <summary>
        /// Tries to call an action with the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <param name="debug"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Try<T>(T element, Action<T> action, Object debug)
        {
            try
            {
                action.Invoke(element);
                return true;
            }
            catch (Exception e)
            {
                string logName = debug != null ? StringUtils.LogName(debug) : LogName;
                Debug.LogError(logName+$"{nameof(Try)} failed. Caught exception {e.Message}.", debug);
                return false;
            }
        }

        /// <summary>
        /// Tries to call an action with the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Try<T>(T element, Action<T> action)
        {
            try
            {
                action.Invoke(element);
                return true;
            }
            catch
            {
                return false;
            }
        }
       
        #endregion
        
        #region Typed → Do
        
        /// <summary>
        /// Perform an action on an element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Do<T>(T element, Action<T> action)
        {
            action.Invoke(element);
            return element;
        }
        
        /// <summary>
        /// Perform an action on an referred element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Do<T>(T element, RefAction<T> action)
        {
            action.Invoke(ref element);
            return element;
        }
        
        #endregion
    }
}
