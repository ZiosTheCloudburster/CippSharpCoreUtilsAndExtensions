//
// Author: Alessandro Salani (Cippo)
//

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Extensions
{
	public static class GameObjectExtensions
	{
		#region Create Child (from TransformUtils)
		
		/// <summary>
		/// Creates an empty child
		/// </summary>
		/// <param name="target"></param>
		/// <param name="childName"></param>
		/// <returns></returns>
		public static GameObject AddEmptyChild(this GameObject target, string childName)
		{
			return TransformUtils.AddEmptyChild(target.transform, childName).gameObject;
		}
		
		/// <summary>
		/// Creates a child with given components
		/// </summary>
		/// <param name="target"></param>
		/// <param name="childName"></param>
		/// <param name="components"></param>
		/// <returns></returns>
		public static GameObject AddChild(this GameObject target, string childName, Type[] components)
		{
			return TransformUtils.AddChild(target.transform, childName, components).gameObject;
		}
		
		/// <summary>
		/// Creates a child with a primitive
		/// </summary>
		/// <param name="target"></param>
		/// <param name="childName"></param>
		/// <param name="primitiveType"></param>
		/// <returns></returns>
		public static GameObject AddPrimitiveChild(this GameObject target, string childName, PrimitiveType primitiveType)
		{
			return TransformUtils.AddPrimitiveChild(target.transform, childName, primitiveType).gameObject;
		}
		
		#endregion

		/// <summary>
		/// It performs a GetComponent on the given GameObject.
		/// If it fails will add a new component of the given type.
		/// </summary>
		/// <param name="target"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetOrAddComponent<T>(this GameObject target) where T : Component
		{
			return GameObjectUtils.GetOrAddComponent<T>(target);
		}

		/// <summary>
		/// Remove target components from a gameobject
		/// </summary>
		/// <param name="target"></param>
		/// <param name="components"></param>
		/// <param name="hardcore"></param>
		public static bool TryRemoveComponents(this GameObject target, Type[] components, bool hardcore = false)
		{
			return GameObjectUtils.TryRemoveComponents(target, components, hardcore);
		}
		
		/// <summary>
		/// Retrieve the first gameObject component of type T.
		/// </summary>
		/// <param name="gameObject"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T As<T> (this GameObject gameObject)
		{
			return GameObjectUtils.As<T>(gameObject);
		}

		
		/// <summary>
		/// Retrieve true if target is children.
		/// </summary>
		/// <param name="root"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static bool Contains(this GameObject root, GameObject target)
		{
			return TransformUtils.Contains(root.transform, target);
		}
		
		/// <summary>
		/// Retrieve true if target is children.
		/// </summary>
		/// <param name="root"></param>
		/// <param name="target"></param>
		/// <param name="debugContext"></param>
		/// <returns></returns>
		public static bool Contains(this GameObject root, GameObject target, Object debugContext)
		{
			return TransformUtils.Contains(root.transform, target, debugContext);
		}
		
		/// <summary>
		/// Retrieve transform hierarchy path as string.
		/// </summary>
		/// <param name="gameObject"></param>
		/// <returns></returns>
		public static string GetGameObjectPath(this GameObject gameObject)
		{
			return TransformUtils.GetTransformPath(gameObject.transform);
		}
	}
}
