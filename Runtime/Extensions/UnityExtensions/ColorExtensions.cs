using UnityEngine;

namespace CippSharp.Core
{
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
