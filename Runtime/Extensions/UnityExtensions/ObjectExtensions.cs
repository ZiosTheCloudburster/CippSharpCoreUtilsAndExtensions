//
// Author: Alessandro Salani (Cippo)
//

using UnityEngine;

namespace CippSharp.Core.Extensions
{
    using ObjectUtils = CippSharp.Core.Utils.ObjectUtils;
    
    public static class ObjectExtensions 
    {
        #region Generic Object → Is 
        
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
            return ObjectUtils.IsNull(o);
        }

        /// <summary>
        /// Is Object T ?
        /// </summary>
        /// <param name="o"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Is<T>(this Object o)
        {
            return ObjectUtils.Is<T>(o);
        }
        		
        /// <summary>
        /// Is Object T ?
        /// Plus retrieve the T result
        /// </summary>
        /// <param name="o"></param>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Is<T>(this Object o, out T result)
        {
            return ObjectUtils.Is(o, out result);
        }

        #endregion
    }
}
