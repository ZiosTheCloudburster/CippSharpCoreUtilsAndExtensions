using UnityEngine;

namespace CippSharp.Core.Extensions
{
	using RectTransformUtils = CippSharp.Core.Utils.RectTransformUtils;
	using CornerPoint = CippSharp.Core.Utils.CornerPoint;
	
	
	public static class RectTransformExtensions
	{
		/// <summary>
		/// Converts RectTransform.rect's local coordinates to world space
		/// Usage example RectTransformExtensions.GetWorldRect(myRect, Vector2.one);
		/// </summary>
		/// <returns>The world rect.</returns>
		/// <param name="rectTransform">RectangleTransform we want to convert to world coordinates.</param>
		public static Rect GetWorldRect(this RectTransform rectTransform)
		{
			return RectTransformUtils.GetWorldRect(rectTransform);
		}

		/// <summary>
		/// Retrieve rect transform world bounds
		/// </summary>
		/// <param name="rectTransform"></param>
		/// <param name="z"></param>
		/// <returns></returns>
		public static Bounds GetWorldBounds(this RectTransform rectTransform, float z = 1)
		{
			return RectTransformUtils.GetWorldBounds(rectTransform, z);
		}

		/// <summary>
		/// Retrieve rect transform bounds bounds
		/// </summary>
		/// <param name="rectTransform"></param>
		/// <param name="z"></param>
		/// <returns></returns>
		public static Bounds GetLocalBounds(this RectTransform rectTransform, float z = 1)
		{
			return RectTransformUtils.GetLocalBounds(rectTransform, z);
		}

		/// <summary>
		/// Returns true if all world corners of a rect transform are inside another.
		/// </summary>
		/// <param name="rectTransform"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool Contains(this RectTransform rectTransform, RectTransform other)
		{
			return RectTransformUtils.Contains(rectTransform, other);
		}

		/// <summary>
		/// Returns true if all local corners of a rect transform are inside another.
		/// </summary>
		/// <param name="rectTransform"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool ContainsLocal(this RectTransform rectTransform, RectTransform other)
		{
			return RectTransformUtils.ContainsLocal(rectTransform, other);
		}
		

		/// <summary>
		/// Retrieve the closest corner point to the target.
		/// </summary>
		/// <param name="rectTransform"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static CornerPoint ToCornerPoint(this RectTransform rectTransform, Vector3 target)
		{
			return RectTransformUtils.ToCornerPoint(rectTransform, target);
		}
	}
}
