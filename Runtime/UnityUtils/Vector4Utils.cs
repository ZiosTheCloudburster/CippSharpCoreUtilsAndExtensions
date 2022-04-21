using UnityEngine;

namespace CippSharp.Core.Utils
{
    public static class Vector4Utils
    {
        /// <summary>
        /// Converts Vector4 to Color
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Color ToColor(Vector4 vector)
        {
            return new Color(vector.x, vector.y, vector.z, vector.w);
        }
    }
}
