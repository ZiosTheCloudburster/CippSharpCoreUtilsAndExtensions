
using UnityEngine;

namespace CippSharp.Core.Utils
{
    public static class Vector2Utils
    {
        /// <summary>
        /// Retrieve an axis angle from vector2.
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static float AxisAngle(Vector2 axis)
        {
            float angle = Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg;
            angle = 90.0f - angle;
            if (angle < 0)
            {
                angle += 360.0f;
            }
            return angle;
        }

        /// <summary>
        /// Converts a vector2 to float array 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static float[] ToArray(Vector2 vector)
        {
            return new[] {vector.x, vector.y};
        }
    }
}
