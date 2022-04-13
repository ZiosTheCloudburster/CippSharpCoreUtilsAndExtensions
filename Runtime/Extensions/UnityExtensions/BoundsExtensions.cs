using UnityEngine;

namespace CippSharp.Core.Extensions
{
    public static class BoundsExtensions
    {
        /// <summary>
        /// Converts bounds int to bounds.
        /// </summary>
        /// <param name="boundsInt"></param>
        /// <returns></returns>
        public static Bounds ToBounds(this BoundsInt boundsInt)
        {
            return BoundsUtils.ToBounds(boundsInt);
        }

        /// <summary>
        /// Retrieve Bounds Corners
        /// </summary>
        /// <returns></returns>
        public static Vector3[] GetBoundsCorners(this Bounds bounds)
        {
            return BoundsUtils.GetBoundsCorners(bounds);
        }
    }
}
