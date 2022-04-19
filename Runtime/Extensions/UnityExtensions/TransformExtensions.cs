//
// Author: Alessandro Salani (Cippo)
//

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Extensions
{
	using TransformUtils = CippSharp.Core.Utils.TransformUtils;
	
	public static class TransformExtensions
	{
		#region Create Child

		/// <summary>
		/// Allow to create a child if it isn't found!
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="childName"></param>
		/// <returns></returns>
		public static Transform FindOrCreateChild(this Transform parent, string childName)
		{
			return TransformUtils.FindOrCreateChild(parent, childName);
		}
		
		/// <summary>
		/// Creates an empty child
		/// </summary>
		/// <param name="target"></param>
		/// <param name="childName"></param>
		/// <returns></returns>
		public static Transform AddEmptyChild(this Transform target, string childName)
		{
			return TransformUtils.AddEmptyChild(target, childName);
		}
		
		/// <summary>
		/// Creates a child with given components
		/// </summary>
		/// <param name="target"></param>
		/// <param name="childName"></param>
		/// <param name="components"></param>
		/// <returns></returns>
		public static Transform AddChild(this Transform target, string childName, Type[] components)
		{
			return TransformUtils.AddChild(target, childName, components);
		}
		
		
		/// <summary>
		/// Creates a child with a primitive
		/// </summary>
		/// <param name="target"></param>
		/// <param name="childName"></param>
		/// <param name="primitiveType"></param>
		/// <returns></returns>
		public static Transform AddPrimitiveChild(this Transform target, string childName, PrimitiveType primitiveType)
		{
			return TransformUtils.AddPrimitiveChild(target, childName, primitiveType);
		}
		
		#endregion
		
		#region Position
		
		/// <summary>
		/// Set local position X
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetLocalPositionX(this Transform target, float value)
		{
			TransformUtils.SetLocalPositionX(target, value); 
		}
		
		/// <summary>
		/// Set local position Y
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetLocalPositionY(this Transform target, float value)
		{
			TransformUtils.SetLocalPositionY(target, value);
		}
		
		/// <summary>
		/// Set local position Z
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetLocalPositionZ(this Transform target, float value)
		{
			TransformUtils.SetLocalPositionZ(target, value);
		}
		
		/// <summary>
		/// Set position X
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetPositionX(this Transform target, float value)
		{
			TransformUtils.SetPositionX(target, value);
		}
      
		/// <summary>
		/// Set position Y
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetPositionY(this Transform target, float value)
		{
			TransformUtils.SetPositionY(target, value);
		}
        
		/// <summary>
		/// Set position Z
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetPositionZ(this Transform target, float value)
		{
			TransformUtils.SetPositionZ(target, value);
		}
		
		#endregion

		#region Rotation

		/// <summary>
		/// Set local euler angles X value
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetLocalEulerAngleX(this Transform target, float value)
		{
			TransformUtils.SetLocalEulerAngleX(target, value);
		}

		/// <summary>
		/// Set local euler angles Y value
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetLocalEulerAngleY(Transform target, float value)
		{
			TransformUtils.SetLocalEulerAngleY(target, value);
		}
        
		/// <summary>
		/// Set local euler angles Z value
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetLocalEulerAngleZ(Transform target, float value)
		{
			TransformUtils.SetLocalEulerAngleZ(target, value);
		}
		
		/// <summary>
		/// Set local euler angles X value
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetEulerAngleX(this Transform target, float value)
		{
			TransformUtils.SetEulerAngleX(target, value);
		}

		/// <summary>
		/// Set local euler angles Y value
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetEulerAngleY(this Transform target, float value)
		{
			TransformUtils.SetEulerAngleY(target, value);
		}
        
		/// <summary>
		/// Set local euler angles Z value
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetEulerAngleZ(this Transform target, float value)
		{
			TransformUtils.SetEulerAngleZ(target, value);
		}

		#endregion
		
		#region Scale

		/// <summary>
		/// Set Local Scale X
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetLocalScaleX(this Transform target, float value)
		{
			TransformUtils.SetLocalScaleX(target, value);
		}
        
		/// <summary>
		/// Set Local Scale Y
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetLocalScaleY(this Transform target, float value)
		{
			TransformUtils.SetLocalScaleY(target, value);
		}
        
		/// <summary>
		/// Set Local Scale Z
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		public static void SetLocalScaleZ(this Transform target, float value)
		{
			TransformUtils.SetLocalScaleZ(target, value);
		}

		/// <summary>
		/// Set Transform Lossy Scale 
		/// </summary>
		public static void SetLossyScale(this Transform target, Vector3 lossyScale)
		{
			TransformUtils.SetLossyScale(target, lossyScale);
		}
        
		#endregion

		#region Resets
		
		/// <summary>
		/// Reset local position of a transform
		/// </summary>
		/// <param name="target"></param>
		public static void ResetLocalPosition(this Transform target)
		{
			TransformUtils.ResetLocalPosition(target);
		}

		/// <summary>
		/// Reset local rotation of a transform
		/// </summary>
		/// <param name="target"></param>
		public static void ResetLocalRotation(this Transform target)
		{
			TransformUtils.ResetLocalRotation(target);
		}

		/// <summary>
		/// Reset local scale of a transform
		/// </summary>
		/// <param name="target"></param>
		public static void ResetLocalScale(this Transform target)
		{
			TransformUtils.ResetLocalScale(target);
		}

		/// <summary>
		/// Reset locals of a transform
		/// </summary>
		/// <param name="target"></param>
		public static void ResetLocals(this Transform target)
		{
			TransformUtils.ResetLocals(target);
		}
		
		/// <summary>
		/// Copy local properties of value transform into target.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value">must be different from null</param>
		/// <exception cref="ArgumentNullException"></exception>
		public static void CopyLocalsFrom(this Transform target, Transform value)
		{
			TransformUtils.CopyLocalsFrom(target, value);
		}
		
		#endregion
		
		/// <summary>
		/// Returns true if target transform is in camera view.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="camera"></param>
		/// <returns></returns>
		public static bool IsInViewOf(this Transform target, Camera camera)
		{
			return TransformUtils.IsInViewOf(target, camera);
		}

		/// <summary>
		/// Returns true if the target is facing the other with an exclusive angle tolerance.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="other"></param>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static bool IsFacing(this Transform target, Transform other, float angle = 5.0f)
		{
			return TransformUtils.IsFacing(target, other, angle);
		}

		/// <summary>
		/// It finds a transform that is brother of the target.
		/// </summary>
		/// <returns></returns>
		public static Transform FindBrother(this Transform target, string brotherName)
		{
			return TransformUtils.FindBrother(target, brotherName);
		}
		
			
		/// <summary>
		/// Retrieve true if target is children.
		/// </summary>
		/// <param name="root"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static bool Contains(this Transform root, GameObject target)
		{
			return TransformUtils.Contains(root, target);
		}
		
		/// <summary>
		/// Retrieve true if target is children.
		/// </summary>
		/// <param name="root"></param>
		/// <param name="target"></param>
		/// <param name="debugContext"></param>
		/// <returns></returns>
		public static bool Contains(this Transform root, GameObject target, Object debugContext)
		{
			return TransformUtils.Contains(root, target, debugContext);
		}
		
		/// <summary>
		/// Retrieve transform hierarchy path as string.
		/// </summary>
		/// <param name="transform"></param>
		/// <returns></returns>
		public static string GetTransformPath(this Transform transform)
		{
			return TransformUtils.GetTransformPath(transform);
		}

		/// <summary>
		/// Retrieve transform nest level.
		/// </summary>
		/// <param name="transform"></param>
		/// <returns></returns>
		public static int GetTransformNestLevel(this Transform transform)
		{
			return TransformUtils.GetTransformNestLevel(transform);
		}
		
		#region Children Find
		
		/// <summary>
		/// Find a direct children even if he is inactive!
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="childName">direct child</param>
		/// <returns></returns>
		public static Transform FindInactive(this Transform transform, string childName)
		{
			return TransformUtils.FindInactive(transform, childName);
		}
		
		#endregion

		#region Matrix

		/// <summary>
		/// Matrix TRS from Transform. Default is World Space.
		/// </summary>
		/// <param name="transform"></param>
		/// <returns></returns>
		public static Matrix4x4 TRS(this Transform transform)
		{
			return TransformUtils.TRS(transform);
		} 

		#endregion
	}
}
