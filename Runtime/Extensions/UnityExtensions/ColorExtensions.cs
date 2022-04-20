using UnityEngine;

namespace CippSharp.Core
{
    using ColorUtils = CippSharp.Core.Utils.ColorUtils;
    
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts a Color to Vector4
        /// </summary>
        /// <returns></returns>
        public static Vector4 ToVector4(this Color color)
        {
            return ColorUtils.ToVector4(color);
        }
    }
}
