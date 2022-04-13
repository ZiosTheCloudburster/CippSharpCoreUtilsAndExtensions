//
// Author: Alessandro Salani (Cippo)
//

using UnityEngine;

namespace CippSharp.Core.Extensions
{
    public static class ObjectExtensions 
    {
        /// <summary>
        /// Retrieve if the give object is valid.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNotNull(this Object o)
        {
            return ObjectUtils.IsNotNull(o);
        }

        /// <summary>
        /// Retrieve if the given object is null.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNull(this Object o)
        {
            return ObjectUtils.IsNotNull(o);
        }
    }
}
