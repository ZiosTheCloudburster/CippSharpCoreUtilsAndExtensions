using UnityEngine;

namespace CippSharp.Core
{
    public static class Vector4Extensions
    {
        /// <summary>
        /// Converts Vector4 to Color
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Color ToColor(this Vector4 vector)
        {
            return Vector4Utils.ToColor(vector);
        }
    }
}
