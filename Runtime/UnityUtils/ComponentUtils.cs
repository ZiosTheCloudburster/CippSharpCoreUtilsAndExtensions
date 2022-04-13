//
// Author: Alessandro Salani (Cippo)
//

using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core
{
	public static class ComponentUtils
	{
		/// <summary>
		/// Get component in parent that works even in special cases, like unet.
		/// </summary>
		/// <param name="target"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetComponentInParent<T>(GameObject target)
		{
			if (target == null)
			{
				Debug.LogError(nameof(target) + " is null.");
				return default(T);
			}

			T component = default(T);
			Transform transform = target.transform;
			while (component == null)
			{
				if (transform == null)
				{
					break;
				}

				component = transform.GetComponent<T>();
				transform = transform.parent;
			}

			return component;
		}

		/// <summary>
		/// Get component in parent that works even in special cases, like unet.
		/// </summary>
		/// <param name="target"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetComponentInParent<T>(Component target)
		{
			if (target == null)
			{
				Debug.LogError(nameof(target) + " is null.");
				return default(T);
			}

			return GetComponentInParent<T>(target.gameObject);
		}

		#region Add Component to Target Objects

		/// <summary>
		/// Add a component to serialized object's targets. Retrieve array of components as Object[]
		/// </summary>
		/// <param name="targets"></param>
		/// <param name="dontAddComponentIfItAlreadyExists"></param>
		public static Object[] AddComponentToTargets<T>(Object[] targets,  bool dontAddComponentIfItAlreadyExists = true) where T : Component
		{
			List<Object> addedComponents = new List<Object>();
			for (int i = 0; i < targets.Length; i++)
			{
				Object target = targets[i];
				if (target == null)
				{
					Debug.LogError("An inspected target is null.");
					continue;
				}
				
				Component inspectedComponent = target as Component;
				if (inspectedComponent == null)
				{
					Debug.LogWarning("Inspected component " + target.name + " is not a "+ typeof(Component).Name+"." , target);
					continue;
				}
				
				T componentToAdd = inspectedComponent.gameObject.GetComponent<T>();
				if (dontAddComponentIfItAlreadyExists)
				{
					if (componentToAdd == null)
					{
						componentToAdd = inspectedComponent.gameObject.AddComponent<T>();
					}
				}
				else
				{
					componentToAdd = inspectedComponent.gameObject.AddComponent<T>();
				}
				
				addedComponents.Add(componentToAdd);

#if UNITY_EDITOR
				EditorObjectUtils.SetDirty(inspectedComponent.gameObject);
#endif
			}

			return addedComponents.ToArray();
		}

		#endregion

		#region Remove Component from Target Objects

		/// <summary>
		/// Remove a component from serialized object's targets.
		/// This method targets the first component of T found.
		/// </summary>
		/// <param name="targets"></param>
		/// <typeparam name="T"></typeparam>
		public static void RemoveComponentFromTargets<T>(Object[] targets) where T : Component
		{
			//Editor.targets aren't serializedObject.targetObjects
			for (int i = 0; i < targets.Length; i++)
			{
				Object target = targets[i];
				if (target == null)
				{
					Debug.LogError("An inspected target is null.");
					continue;
				}
					
				Component inspectedComponent = target as Component;
				if (inspectedComponent == null)
				{
					Debug.LogWarning("Inspected component " + target.name + " is not a "+ typeof(Component).Name+"." , target);
					continue;
				}

				GameObject gameObject = inspectedComponent.gameObject;
				T componentToRemove = inspectedComponent.GetComponent<T>();
				if (componentToRemove != null)
				{
					ObjectUtils.SafeDestroy(componentToRemove);
				}

#if UNITY_EDITOR
				EditorObjectUtils.SetDirty(gameObject);
#endif
			}
		}
		

		#endregion
	}
}
