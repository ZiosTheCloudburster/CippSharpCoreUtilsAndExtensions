using UnityEngine;

namespace CippSharp.Core.Extensions
{
    public static class LayerMaskExtensions
    {
        /// <summary>
        /// LayerMask contains layer?
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return LayerMaskUtils.Contains(mask, layer);
        }
    }
}
