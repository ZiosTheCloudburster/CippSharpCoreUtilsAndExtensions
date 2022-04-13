//
// Author: Alessandro Salani (Cippo)
//

using UnityEngine;

namespace CippSharp.Core.Extensions
{
	public static class Vector3Extensions 
	{
		/// <summary>
		/// Mathf Abs on each float of Vector3
		/// </summary>
		/// <param name="input"></param>
		public static Vector3 Abs(this Vector3 input)
		{
			Vector3Utils.Abs(ref input);
			return input;
		}
		
		/// <summary>
		/// Retrieve if current vector c is in line with a and b.
		/// </summary>
		/// <param name="c"></param>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool IsBetweenAB(this Vector3 c, Vector3 a, Vector3 b)
		{
			return Vector3Utils.IsBetweenAB(c, a, b);
		}
		
		/// <summary>
		/// Check if a vector is equal to another with a tolerance
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="tolerance"></param>
		/// <returns></returns>
		public static bool IsSimilar (this Vector3 a, Vector3 b, float tolerance = 0.0001f)
		{
			return Vector3Utils.IsSimilar(a, b, tolerance);
		}
		
		/// <summary>
		/// Retrieve if current vector is inside an area.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="areaMinValues"></param>
		/// <param name="areaMaxValues"></param>
		/// <returns></returns>
		public static bool IsInArea(this Vector3 position, Vector3 areaMinValues, Vector3 areaMaxValues)
		{
			return Vector3Utils.IsInArea(position, areaMinValues, areaMaxValues);

		}
		
		/// <summary>
		/// Retrieve the closest point from a vector 3 array.
		/// </summary>
		/// <param name="point"></param>
		/// <param name="array"></param>
		/// <returns></returns>
		public static Vector3 Closest(this Vector3 point, Vector3[] array)
		{
			return Vector3Utils.Closest(point, array);
		}

		/// <summary>
		/// UnScale vector
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static void UnScale(this ref Vector3 a, Vector3 b)
		{
			Vector3Utils.UnScale(ref a, b);
		}

		/// <summary>
		/// This vector contains any value with 0?
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static bool ContainsZero(this Vector3 v)
		{
			return Vector3Utils.Contains(v, 0);
		}
		
		#region Set
		
		/// <summary>
		/// Set Vector3 X
		/// </summary>
		/// <param name="input"></param>
		/// <param name="value"></param>
		public static void SetX(this ref Vector3 input, float value)
		{
			Vector3Utils.SetX(ref input, value);
		}
		
		/// <summary>
		/// Set Vector3 Y
		/// </summary>
		/// <param name="input"></param>
		/// <param name="value"></param>
		public static void SetY(this ref Vector3 input, float value)
		{
			Vector3Utils.SetY(ref input, value);
		}
		
		/// <summary>
		/// Set Vector3 Z
		/// </summary>
		/// <param name="input"></param>
		/// <param name="value"></param>
		public static void SetZ(this ref Vector3 input, float value)
		{
			Vector3Utils.SetZ(ref input, value);
		}
		
		#endregion
	}
}
