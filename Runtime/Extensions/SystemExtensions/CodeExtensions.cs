using System;
using CippSharp.Core.Utils;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Extensions
{
    using CodeUtils = CippSharp.Core.Utils.CodeUtils;
    
    public static class CodeExtensions
    {
        #region Generic → Try
        
        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [Obsolete("Use Try with debug parameter.")]
        public static bool Try(this object element, Action action, out string error)
        {
            error = string.Empty;
            return CodeUtils.Try(element, action);
        }

        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static bool Try(this object element, Action action, Object debug)
        {
            return CodeUtils.Try(element, action, debug);
        }

        /// <summary>
        /// Tries to call an action from the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        public static bool Try(this object element, Action action)
        {
            return CodeUtils.Try(element, action);
        }
        
        #endregion
        
        #region Typed → Try On 'T' Reference
        
        /// <summary>
        /// Tries to call an action with the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <param name="error"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("Use Try with debug parameter instead.")]
        public static bool Try<T>(this T element, Action<T> action, out string error)
        {
            error = string.Empty;
            return CodeUtils.Try(element, action);
        }

        /// <summary>
        /// Tries to call an action with the given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <param name="debug"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Try<T>(this T element, Action<T> action, Object debug)
        {
            return CodeUtils.Try(element, action, debug);
        }
        
        #endregion

        #region Typed → Do
        
        /// <summary>
        /// Perform an action on element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Do<T>(this T element, Action<T> action)
        {
            return CodeUtils.Do(element, action);
        }

        /// <summary>
        /// Perform an action on an referred element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Do<T>(this T element, RefAction<T> action)
        {
            return CodeUtils.Do(element, action);
        }

        #endregion
    }
}
