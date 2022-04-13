using UnityEngine;

namespace CippSharp.Core
{
    public static class BoundsUtils 
    {
        /// <summary>
        /// Converts bounds int to bounds.
        /// </summary>
        /// <param name="boundsInt"></param>
        /// <returns></returns>
        public static Bounds ToBounds(BoundsInt boundsInt)
        {
            return new Bounds(boundsInt.center, boundsInt.size);
        }
        
        /// <summary>
        /// Get point by extents and normalized position
        /// (consider an origin to Vector3.zero and extents as 'area' around that point)
        /// </summary>
        /// <param name="extents"></param>
        /// <param name="normalizedPosition"></param>
        /// <returns></returns>
        public static Vector3 GetPoint(Vector3 extents, Vector3 normalizedPosition)
        {
            Vector3 extentsCopy = extents;
            extentsCopy.x *= normalizedPosition.x;
            extentsCopy.y *= normalizedPosition.y;
            extentsCopy.z *= normalizedPosition.z;
            return (extentsCopy);
        }

        /// <summary>
        /// Retrieve Bounds Corners
        /// </summary>
        /// <returns></returns>
        public static Vector3[] GetBoundsCorners(Bounds bounds)
        {
            Vector3[] newArray = new Vector3[8];
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;
            newArray[0] = min;
            newArray[1] = max;
            newArray[2] = new Vector3(min.x, min.y, max.z);
            newArray[3] = new Vector3(min.x, max.y, min.z);
            newArray[4] = new Vector3(max.x, min.y, min.z);
            newArray[5] = new Vector3(min.x, max.y, max.z);
            newArray[6] = new Vector3(max.x, min.y, max.z);
            newArray[7] = new Vector3(max.x, max.y, min.z);
            return newArray;
        }
    }
}
