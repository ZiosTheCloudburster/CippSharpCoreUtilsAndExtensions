using System;
using System.Linq;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Utils
{
    /// <summary>
    /// Purpose: holds utils about interfaces
    /// </summary>
    public static class InterfacesUtils
    {
        /// <summary>
        /// Finds all interfaces of I among Objects
        /// Expensive but sure way to get 'em all
        /// </summary>
        /// <typeparam name="I">interface</typeparam>
        /// <returns></returns>
        public static I[] FindAllInterfaces<I>()
        {
            Object[] unityObjects = ObjectUtils.FindObjectsOfType<Object>();
            I[] interfaces = ArrayUtils.SelectIf(unityObjects, uo => uo is I, uo => (I) (object) uo).Distinct().ToArray();
            return interfaces;
        }
        
        /// <summary>
        /// Get all interfaces of Type I and calls invoke the action.
        /// </summary>
        /// <typeparam name="I">interface</typeparam>
        public static void CallOnAllInterfaces<I>(Action<I> action)
        {
            I[] allInterfaces = FindAllInterfaces<I>();
            if (ArrayUtils.IsNullOrEmpty(allInterfaces))
            {
                return;
            }

            foreach (var @interface in allInterfaces)
            {
                action?.Invoke(@interface);
            }
        }
    }
}
