using UnityEngine;

namespace CippSharp.Core.Utils
{
    public static class LayerMaskUtils
    {
        /// <summary>
        /// Is layer in LayerMask?
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static bool IsLayerInMask(int layer, LayerMask mask)
        {
            return Contains(mask, layer);
        }

        /// <summary>
        /// LayerMask contains layer?
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static bool Contains(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}
