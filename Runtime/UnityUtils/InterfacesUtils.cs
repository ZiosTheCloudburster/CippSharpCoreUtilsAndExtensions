using System;
using System.Collections.Generic;
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
        public static IEnumerable<I> FindAllInterfaces<I>()
        {
            return ObjectUtils.SelectFromObjectsOfType<Object, I>(o => o is I, o => (I) (object) o).Distinct();
        }
        
        /// <summary>
        /// Get all interfaces of Type I and calls invoke the action.
        /// </summary>
        /// <typeparam name="I">interface</typeparam>
        public static void CallOnAllInterfaces<I>(Action<I> action)
        {
            ArrayUtils.ForEach(FindAllInterfaces<I>(), action);
        }
    }
}
