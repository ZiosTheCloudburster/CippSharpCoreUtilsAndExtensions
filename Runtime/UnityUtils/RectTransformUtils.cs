using System.Linq;
using UnityEngine;

namespace CippSharp.Core.Utils
{
    public static class RectTransformUtils
    {
        /// <summary>
        /// Corners Array
        /// </summary>
        private static readonly Vector3[] corners = new Vector3[4];

        #region Rect Transform Bounds
        
        /// <summary>
        /// Converts RectTransform.rect's local coordinates to world space
        /// Usage example RectTransformExtensions.GetWorldRect(myRect, Vector2.one);
        /// </summary>
        /// <returns>The world rect.</returns>
        /// <param name="rectTransform">RectangleTransform we want to convert to world coordinates.</param>
        public static Rect GetWorldRect(RectTransform rectTransform)
        {
            Vector2 scale = rectTransform.lossyScale;
            // Convert the rectangle to world corners and grab the top left
            rectTransform.GetWorldCorners(corners);
            Vector3 topLeft = corners[(int)CornerPoint.TopLeft];
            // Rescale the size appropriately based on the current Canvas scale
            Rect rect = rectTransform.rect;
            Vector2 scaledSize = new Vector2(scale.x * rect.size.x, scale.y * rect.size.y);

            return new Rect(topLeft, scaledSize);
        }
        
        /// <summary>
        /// Retrieve rect transform world bounds
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Bounds GetWorldBounds(RectTransform rectTransform, float z = 1)
        {
            rectTransform.GetWorldCorners(corners);
            Vector3 bottomLeft = corners[(int) CornerPoint.BottomLeft];
            float width = Vector3.Distance(bottomLeft, corners[(int) CornerPoint.BottomRight]);
            float height = Vector3.Distance(bottomLeft, corners[(int) CornerPoint.TopLeft]);
            Vector3 center = (bottomLeft + corners[(int)CornerPoint.TopRight])* 0.5f;
            Bounds bounds = new Bounds(center, new Vector3(width, height, z));
            return bounds;
        }
        
        /// <summary>
        /// Retrieve rect transform bounds bounds
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Bounds GetLocalBounds(RectTransform rectTransform, float z = 1) 
        {
            rectTransform.GetLocalCorners(corners);
            Vector3 bottomLeft = corners[(int) CornerPoint.BottomLeft];
            float width = Vector3.Distance(bottomLeft, corners[(int) CornerPoint.BottomRight]);
            float height = Vector3.Distance(bottomLeft, corners[(int) CornerPoint.TopLeft]);
            Vector3 center = (bottomLeft + corners[(int)CornerPoint.TopRight])* 0.5f;
            Bounds bounds = new Bounds(center, new Vector3(width, height, z));
            return bounds;
        }
        
        #endregion

        #region Contains

        /// <summary>
        /// Returns true if all world corners of a rect transform are inside another.
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool Contains(RectTransform rectTransform, RectTransform other)
        {
            Bounds bounds = GetWorldBounds(rectTransform);
            other.GetWorldCorners(corners);
            var otherCorners = corners;
            return otherCorners.All(o => bounds.Contains(o));
        }

        /// <summary>
        /// Returns true if all local corners of a rect transform are inside another.
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool ContainsLocal(RectTransform rectTransform, RectTransform other)
        {
            Bounds bounds = GetLocalBounds(rectTransform);
            other.GetLocalCorners(corners);
            var otherCorners = corners;
            return otherCorners.All(o => bounds.Contains(o));
        }

        #endregion
        
        
        /// <summary>
        /// Retrieve the closest corner point to the target.
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static CornerPoint ToCornerPoint(this RectTransform rectTransform, Vector3 target)
        {
            rectTransform.GetWorldCorners(corners);

            var closest = Vector3Utils.Closest(target, corners);
            for (var i = 0; i < corners.Length; i++)
            {
                var corner = corners[i];
                if (corner == closest)
                {
                    return (CornerPoint) i;
                }
            }
			
            return (CornerPoint)(int)-1;
        }
    }
}
