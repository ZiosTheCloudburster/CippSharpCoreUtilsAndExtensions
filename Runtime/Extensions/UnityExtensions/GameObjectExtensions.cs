//
// Author: Alessandro Salani (Cippo)
//

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Extensions
{
	using TransformUtils = CippSharp.Core.Utils.TransformUtils;
	using GameObjectUtils = CippSharp.Core.Utils.GameObjectUtils;
	
	public static class GameObjectExtensions
	{
		#region GameObject Generic
		
		#region → Add Children (from TransformUtils)

		/// <summary>
		/// Allow to create a child if it isn't found!
		/// </summary>
		/// <param name="target"></param>
		/// <param name="childName"></param>
		/// <returns></returns>
		public static GameObject FindOrCreateChild(this GameObject target, string childName)
		{
			return TransformUtils.FindOrCreateChild(target.transform, childName).gameObject;
		}

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
		
		#region → Cast
		
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
		/// Object to GameObject
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static GameObject To(this Object o)
		{
			return GameObjectUtils.From(o);
		}
		
		#endregion
		
		#endregion

		#region GameObject → TransformUtils

		#region → Contains 
		
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
		
		#endregion
		
		/// <summary>
		/// Retrieve transform hierarchy path as string.
		/// </summary>
		/// <param name="gameObject"></param>
		/// <returns></returns>
		public static string GetGameObjectPath(this GameObject gameObject)
		{
			return TransformUtils.GetTransformPath(gameObject.transform);
		}
		
		#endregion
		
		#region → GetComponent
		
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

		#endregion
		
		#region → RemoveComponent
		
		/// <summary>
		/// Remove target components from a GameObject
		/// </summary>
		/// <param name="target"></param>
		/// <param name="components"></param>
		/// <param name="hardcore"></param>
		public static bool TryRemoveComponents(this GameObject target, Type[] components, bool hardcore = false)
		{
			return GameObjectUtils.TryRemoveComponents(target, components, hardcore);
		}

		#endregion
		
	}
}
