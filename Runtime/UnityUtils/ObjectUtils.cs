//
// Author: Alessandro Salani (Cippo)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CippSharp.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CippSharp.Core
{
	using Object = UnityEngine.Object;
	
	public static class ObjectUtils
	{
		#region Is Not Null / Is Null (UnityEngine.Object null check as utils for extensions)
		
		/// <summary>
		/// Retrieve if the give object is valid.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool IsNotNull(Object o)
		{
			return (o != null);
		}
		
		/// <summary>
		/// Retrieve if the given object is null.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool IsNull(Object o)
		{
			return (o == null);
		}

		#endregion
		
		#region Find or Get UnityEngine.Object of Type

		#region Find Object of Type
		
		/// <summary>
		/// A method to find a component (suggested to do during awake) that is present in scene, even if inactive.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T FindObjectOfType<T>() where T : Object
		{
			//Unity retrieve all objects of that T even inactive objects and prefabs.
			T[] array = Resources.FindObjectsOfTypeAll<T>();
			//If array is null or empty retrieve null.
			if (ArrayUtils.IsNullOrEmpty(array))
			{
				return null;
			}
#if UNITY_EDITOR
			//Remove prefabs from retrieved array.
			T[] sceneArray = array.Where(AssetDatabaseUtils.IsObjectPathNullOrEmpty).ToArray();
			return sceneArray.Length < 1 ? null : sceneArray[0];
#else
			//Retrieve the first element of array.
			return array[0];
#endif
		}
		
		/// <summary>
		///A method to find components (suggested to do during awake) that are present in scene even if inactive.
		/// Warning: the result may be sorted differently at each play.
		/// Use <see cref="GetAllComponents{T}"/> for a sorted array.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] FindObjectsOfType<T>() where T : Object
		{
			//Retrieve all objects of that T even inactive objects and prefabs.
			T[] array = Resources.FindObjectsOfTypeAll<T>();
#if UNITY_EDITOR
			//Remove prefabs from retrieved array.
			T[] sceneArray = array.Where(AssetDatabaseUtils.IsObjectPathNullOrEmpty).ToArray();
			return sceneArray;
#else
			return array;
#endif
		}

		
		#endregion

		#region Get
		
		/// <summary>
		/// Retrieve a hierarchical sorted component lists.
		/// 
		/// Note: it seems that it doesn't retrieve objects on "DontDestroyOnLoad".
		/// From now on if you used this to get and call methods on interfaces use <see cref="InterfacesUtils"/> instead.
		/// </summary>
		/// <param name="affectInactive"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] GetAllComponents<T>(bool affectInactive = false)
		{
			List<T> sortedComponents = new List<T>();
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				Scene currentScene = SceneManager.GetSceneAt(i);
				var roots = currentScene.GetRootGameObjects();
				foreach (var gameObject in roots)
				{
					sortedComponents.AddRange(gameObject.GetComponentsInChildren<T>(affectInactive));
				}
			}
			return sortedComponents.ToArray();
		}
		
		#endregion
	
		#endregion
		
		#region Get UnityEngine.Object Instance ID

		/// <summary>
		/// Retrieve object id, returns 0 if it fails.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public static int GetID (Object target)
		{
			return target != null ? target.GetInstanceID() : 0;
		}
		
		#endregion
		
		#region Safe Destroy of UnityEngine.Object

		#region Single Object
	
		/// <summary>
		/// Safely destroy a single Object.
		/// </summary>
		/// <param name="target"></param>
		public static bool SafeDestroy(Object target)
		{
			return SafeDestroyInternal(target, null, false);
		}
		
		/// <summary>
		/// Safely destroy a single Object.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="debugContext">from where I'm calling this static method</param>
		public static bool SafeDestroy(Object target, Object debugContext)
		{
			return SafeDestroyInternal(target, debugContext, true);
		}

		/// <summary>
		/// Safely destroy a single Object.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="debugContext"></param>
		/// <param name="debug"></param>
		private static bool SafeDestroyInternal(Object target, Object debugContext, bool debug)
		{
			string logName = (debug) ? StringUtils.LogName(debugContext) : string.Empty;
			
			if (target == null)
			{
				if (debug)
				{
					Debug.LogError(logName+"I cannot destroy a target already null!", debugContext);
				}
				return true;
			}

			try
			{
				bool isPlaying = Application.isPlaying;
				if (isPlaying)
				{
					Object.Destroy(target);
				}
				else
				{
					Object.DestroyImmediate(target);
				}

				return true;
			}
			catch (Exception e)
			{
				if (debug)
				{
					Debug.LogError(logName + e.Message, debugContext);
				}
				return false;
			}
		}
		
		#endregion

		#region Objects Array 
		
		/// <summary>
		/// Safely destroys Objects.
		/// </summary>
		/// <param name="targets"></param>
		public static bool SafeDestroy(Object[] targets)
		{
			return SafeDestroyInternal(targets, null, false);
		}
		
		/// <summary>
		/// Safely destroys Objects.
		/// </summary>
		/// <param name="targets"></param>
		/// <param name="debugContext">from where I'm calling this static method</param>
		public static bool SafeDestroy(Object[] targets, Object debugContext)
		{
			return SafeDestroyInternal(targets, debugContext, true);
		}

		/// <summary>
		/// Safely destroys Objects.
		/// </summary>
		/// <param name="targets"></param>
		/// <param name="debugContext"></param>
		/// <param name="debug"></param>
		/// <returns></returns>
		private static bool SafeDestroyInternal(Object[] targets, Object debugContext, bool debug)
		{
			string logName = (debug) ? StringUtils.LogName(debugContext) : string.Empty;

			try
			{
				bool isPlaying = Application.isPlaying;
				foreach (Object o in targets)
				{
					if (o == null)
					{
						if (debug)
						{
							Debug.LogError(logName+"I cannot destroy an object already null!", debugContext);
						}
						continue;
					}
				
					if (isPlaying)
					{
						Object.Destroy(o);
					}
					else
					{
						Object.DestroyImmediate(o);
					}
				}

				return true;
			}
			catch (Exception e)
			{
				if (debug)
				{
					Debug.LogError(logName+e.Message, debugContext);
				}
				return false;
			}
		}
		
		#endregion

		#region with Requiring Components
		
		/// <summary>
		///	Safe Destroys a component and all components that are requiring it.
		/// </summary>
		/// <param name="component"></param>
		public static void SafeDestroyWithRequiringComponents(Component component)
		{
			SafeDestroyWithRequiringComponentsInternal(component, null, false);
		}
		
		/// <summary>
		///	Safe Destroys a component and all components that are requiring it.
		/// </summary>
		/// <param name="component"></param>
		/// <param name="debugContext">from where I'm calling this static method</param>
		public static void SafeDestroyWithRequiringComponents(Component component, Object debugContext)
		{
			SafeDestroyWithRequiringComponentsInternal(component, debugContext, true);
		}

		/// <summary>
		/// Recursive insane method to destroy a component and all other components that requires it.
		/// </summary>
		/// <param name="component"></param>
		/// <param name="debugContext"></param>
		/// <param name="debug"></param>
		private static void SafeDestroyWithRequiringComponentsInternal(Component component, Object debugContext, bool debug)
		{
			string logName = (debug) ? StringUtils.LogName(debugContext) : string.Empty;
			if (component == null)
			{
				if (debug)
				{
					Debug.LogError(logName + "I cannot destroy a target already null!", debugContext);
				}
				return;
			}
			
			Type componentType = component.GetType();
			Component[] otherComponents = component.gameObject.GetComponents<Component>();
			List<Component> componentsToDestroy = new List<Component>();
			foreach (var otherComponent in otherComponents)
			{
				if (otherComponent == null)
				{
					if (debug)
					{
						Debug.LogWarning(logName+"A component on gameObject "+component.gameObject.name+".", debugContext);
					}
					continue;
				}

				Type otherComponentType = otherComponent.GetType();
				MemberInfo otherComponentMemberInfo = otherComponentType;
				RequireComponent[] requiredComponentAttributes = Attribute.GetCustomAttributes(otherComponentMemberInfo, typeof(RequireComponent), true).Cast<RequireComponent>().ToArray();
				if (ArrayUtils.IsNullOrEmpty(requiredComponentAttributes))
				{
					continue;
				}
            
				foreach (RequireComponent requireComponent in requiredComponentAttributes)
				{
					if (requireComponent == null)
					{
						continue;
					}

					Type[] requiredTypes = new[] {requireComponent.m_Type0, requireComponent.m_Type1, requireComponent.m_Type2};
					for (int i = 0; i < requiredTypes.Length; i++)
					{
						Type requiredType = requiredTypes[i];
						if (requiredType == null)
						{
							continue;
						}

						if (requiredType.IsAssignableFrom(componentType))
						{
							componentsToDestroy.Add(otherComponent);
						}
					}
				} 
			}

			componentsToDestroy.Remove(component);

			if (!ArrayUtils.IsNullOrEmpty(componentsToDestroy))
			{
				//Recurse
				foreach (var componentToDestroy in componentsToDestroy)
				{
					if (componentToDestroy == null)
					{
						continue;
					}

					SafeDestroyWithRequiringComponentsInternal(componentToDestroy, debugContext, debug);
				}
			}
			
			bool isPlaying = Application.isPlaying;
			if (isPlaying)
			{
				Object.Destroy(component);
			}
			else
			{
				Object.DestroyImmediate(component);
			}
		}

		#endregion
		
		#endregion
		
		#region Set UnityEngine.Object enabled property
		
		/// <summary>
		/// Set enabled a bunch of targets.
		/// </summary>
		/// <param name="targets"></param>
		/// <param name="value"></param>
		public static void SetEnabled(Object[] targets, bool value)
		{
			SetEnabledInternal(targets, value, null, false);
		}

		/// <summary>
		/// Set enabled a bunch of targets.
		/// </summary>
		/// <param name="targets"></param>
		/// <param name="value"></param>
		/// <param name="debugContext"></param>
		public static void SetEnabled(Object[] targets, bool value, Object debugContext)
		{
			SetEnabledInternal(targets, value, debugContext, true);
		}

		/// <summary>
		/// Set enabled a bunch of targets.
		/// </summary>
		/// <param name="targets"></param>
		/// <param name="value"></param>
		/// <param name="debugContext"></param>
		/// <param name="debug"></param>
		private static void SetEnabledInternal(Object[] targets, bool value, Object debugContext, bool debug)
		{
			string logName = debug ? StringUtils.LogName(debugContext) : string.Empty;
			if (ArrayUtils.IsNullOrEmpty(targets))
			{
				if (debug)
				{
					Debug.LogWarning(logName +nameof(targets)+" are null or empty.", debugContext);
				}
				return;
			}
			
			foreach (var target in targets)
			{
				if (target == null)
				{
					if (debug)
					{
						string error = "Null component found on components list.";
						Debug.LogError(logName + error, debugContext);
					}
					
					continue;
				}

				if (target is Behaviour b)
				{
					b.enabled = value;
				}
				else
				{
					try
					{
						PropertyInfo property;
						if (ReflectionUtils.HasProperty(target, UtilsConstants.EnabledPropertyName, out property))
						{
							property.SetValue(target, value, null);
						}
					}
					catch (Exception e)
					{
						if (debug)
						{
							Debug.LogError(logName + e.Message, debugContext);
						}
					}
				}
			}
		}

		#endregion

		[Obsolete("2021/08/14 → Moved to GameObjectUtils.BroadcastAll")]
		public static void BroadcastAll(string methodName, object parameter = null, SendMessageOptions messageOptions = SendMessageOptions.DontRequireReceiver)
		{
			return;
		}
	}
}