//
// Author: Alessandro Salani (Cippo)
//

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Utils
{
	[Obsolete("Use GameObjectUtils instead. This will be removed in future versions.")]
	public static class ComponentUtils
	{
		#region → GetComponentInParent

		/// <summary>
		/// Hardcore GetComponentInParent that works even in special cases like
		/// UNET networking
		/// </summary>
		/// <param name="target"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		[Obsolete("Use GameObjectUtils.GetComponentInParent instead. This will be removed in future versions.")]
		public static T GetComponentInParent<T>(GameObject target)
		{
			return GameObjectUtils.GetComponentInParent<T>(target);
		}

		/// <summary>
		/// Hardcore GetComponentInParent that works even in special cases like
		/// UNET networking
		/// </summary>
		/// <param name="target"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		[Obsolete("Use GameObjectUtils.GetComponentInParent instead. This will be removed in future versions.")]
		public static T GetComponentInParent<T>(Component target)
		{
			return GameObjectUtils.GetComponentInParent<T>(target.gameObject);
		}

		#endregion
		

		#region → Add Component to Targets Objects

		/// <summary>
		/// Add specific components to generic bunch of targets
		/// </summary>
		/// <param name="targets"></param>
		/// <param name="dontAddComponentIfItAlreadyExists"></param>
		[Obsolete("Use GameObjectUtils.AddComponentToTargets instead. This will be removed in future versions.")]
		public static List<T> AddComponentToTargets<T>(Object[] targets,  bool dontAddComponentIfItAlreadyExists = true) where T : Component
		{
			return GameObjectUtils.AddComponentToTargets<T>(targets, dontAddComponentIfItAlreadyExists);
		}

		#endregion

		#region → Remove Component from Target Objects

		/// <summary>
		/// Remove a component from serialized object's targets.
		/// This method targets the first component of T found.
		/// </summary>
		/// <param name="targets"></param>
		/// <typeparam name="T"></typeparam>
		[Obsolete("Use GameObjectUtils.RemoveLastComponentFromTargets instead. This will be removed in future versions.")]
		public static void RemoveComponentFromTargets<T>(Object[] targets) where T : Component
		{
			GameObjectUtils.RemoveLastComponentFromTargets<T>(targets);
		}
		
		#endregion
	}
}
