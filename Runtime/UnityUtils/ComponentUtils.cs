//
// Author: Alessandro Salani (Cippo)
//

using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Utils
{
	public static class ComponentUtils
	{
		/// <summary>
		/// A better name for logs
		/// </summary>
		private static readonly string LogName = $"[{nameof(ComponentUtils)}]: ";

		#region → GetComponentInParent

		/// <summary>
		/// Hardcore GetComponentInParent that works even in special cases like
		/// UNET networking
		/// </summary>
		/// <param name="target"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetComponentInParent<T>(GameObject target)
		{
			if (target == null)
			{
				Debug.LogError(LogName+$"{nameof(GetComponentInParent)} {nameof(target)} is null.");
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
		/// Hardcore GetComponentInParent that works even in special cases like
		/// UNET networking
		/// </summary>
		/// <param name="target"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetComponentInParent<T>(Component target)
		{
			return GetComponentInParent<T>(target.gameObject);
		}

		#endregion

		

		#region Add Component to Targets

		/// <summary>
		/// Add specific components to generic bunch of targets
		/// </summary>
		/// <param name="targets"></param>
		/// <param name="dontAddComponentIfItAlreadyExists"></param>
		public static List<T> AddComponentToTargets<T>(Object[] targets,  bool dontAddComponentIfItAlreadyExists = true) where T : Component
		{
			List<T> addedComponents = new List<T>();
			var filteredTargets = ArrayUtils.SelectIf(targets, o => GameObjectUtils.From(o) != null, GameObjectUtils.From);
			for (int i = 0; i < targets.Length; i++)
			{
				Object target = targets[i];
				if (target == null)
				{
					Debug.LogError(LogName+$"{nameof(AddComponentToTargets)} {nameof(target)} is null at {i.ToString()}.");
					continue;
				}
				
				GameObject add
				
				Component inspectedComponent = target as Component;
				if (inspectedComponent == null)
				{
					
					Debug.LogWarning(LogName+$"{nameof(AddComponentToTargets)} {nameof(target)} is null at {i.ToString()}.");
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
				UnityEditor.EditorUtility.SetDirty(inspectedComponent.gameObject);
#endif
			}

			return addedComponents;
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
				UnityEditor.EditorUtility.SetDirty(gameObject);
#endif
			}
		}
		

		#endregion
	}
}
