//
// Author: Alessandro Salani (Cippo)
//

using UnityEngine;

namespace CippSharp.Core.Extensions
{
	public static class Vector2Extensions 
	{
		/// <summary>
		/// Retrieve an axis angle from vector2.
		/// </summary>
		/// <param name="axis"></param>
		/// <returns></returns>
		public static float AxisAngle(this Vector2 axis)
		{
			return Vector2Utils.AxisAngle(axis);
		}

		/// <summary>
		/// Converts a vector2 to array
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static float[] ToArray(this Vector2 vector)
		{
			return Vector2Utils.ToArray(vector);
		}
	}
}
