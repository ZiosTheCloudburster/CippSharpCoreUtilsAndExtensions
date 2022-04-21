//
// Author: Alessandro Salani (Cippo)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CippSharp.Core.Utils
{
	using Object = UnityEngine.Object;
	
	public static class ObjectUtils
	{
		
		#region Generic Object → Is 
		
		/// <summary>
		/// Is Object not null?
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool IsNotNull(Object o)
		{
			return (o != null);
		}
		
		/// <summary>
		/// Is Object null?
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool IsNull(Object o)
		{
			return (o == null);
		}

		/// <summary>
		/// Is Object T ?
		/// </summary>
		/// <param name="o"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static bool Is<T>(Object o)
		{
			return o is T;
		}
		
		/// <summary>
		/// Is Object T ?
		/// Plus retrieve the T result
		/// </summary>
		/// <param name="o"></param>
		/// <param name="result"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static bool Is<T>(Object o, out T result)
		{
			if (o is T t)
			{
				result = t;
				return true;
			}
			else
			{
				result = default(T);
				return false;
			}
		}

		#endregion

		#region Object → Find or Get of Type
		
		#region → Find Object(s) of Type
		
		/// <summary>
		/// A method to find a component (suggested to do during initialization) that is present in scene, even if inactive.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T FindObjectOfType<T>() where T : Object
		{
			//Unity retrieve all objects of that T even inactive objects and prefabs.
			//Then remove prefabs from retrieved array.
			return Resources.FindObjectsOfTypeAll<T>().FirstOrDefault(IsValidObjectOfType);
		}

		/// <summary>
		/// A method to find a component (suggested to do during initialization) that is present in scene, even if inactive.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T FindObjectOfType<T>(Predicate<T> predicate) where T : Object
		{
			//Unity retrieve all objects of that T even inactive objects and prefabs.
			//Then remove prefabs from retrieved array + predicate
			return Resources.FindObjectsOfTypeAll<T>().FirstOrDefault(t => IsValidObjectOfType(t, predicate));
		}
		

		/// <summary>
		/// A method to find components (suggested to do during initialization) that are present in scene even if inactive.
		///
		/// Warning: the result may be sorted differently at each play.
		/// Use <see cref="GetAllComponents{T}"/> for a sorted array.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] FindObjectsOfType<T>() where T : Object
		{
			//Retrieve all objects of that T even inactive objects and prefabs.
			//Then remove prefabs from retrieved array.
			return Resources.FindObjectsOfTypeAll<T>().Where(IsValidObjectOfType).ToArray();
		}

		/// <summary>
		/// A method to find components (suggested to do during initialization) that are present in scene even if inactive.
		///
		/// Warning: the result may be sorted differently at each play.
		/// Use <see cref="GetAllComponents{T}"/> for a sorted array.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] FindObjectsOfType<T>(Predicate<T> predicate) where T : Object
		{
			//Retrieve all objects of that T even inactive objects and prefabs.
			//Then remove prefabs from retrieved array + predicate
			return Resources.FindObjectsOfTypeAll<T>().Where(t => IsValidObjectOfType(t, predicate)).ToArray();
		}

		///  <summary>
		///  A method to select something from components (suggested to do during initialization)
		///  that are present in scene even if inactive.
		/// 
		///  Warning: the result may be sorted differently at each play.
		///  Use <see cref="GetAllComponents{T}"/> for a sorted array.
		///  </summary>
		///  <typeparam name="T"></typeparam>
		///  <typeparam name="K"></typeparam>
		///  <returns></returns>
		public static IEnumerable<K> SelectFromObjectsOfType<T, K>(Predicate<T> predicate, Func<T, K> func) where T : Object
		{
			//Retrieve all objects of that T even inactive objects and prefabs.
			//Then remove prefabs from retrieved array + predicate
			return Resources.FindObjectsOfTypeAll<T>().Where(t => IsValidObjectOfType(t, predicate)).Select(func);
		}

		#endregion
		
		#region → Is Valid Object of Type
		
		/// <summary>
		/// Is this a valid object of type?
		///
		/// To be valid it needs to have object path null or empty!
		/// </summary>
		/// <param name="target"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static bool IsValidObjectOfType<T>(T target) where T : Object
		{
			return
#if UNITY_EDITOR
				AssetDatabaseUtils.IsObjectPathNullOrEmpty(target);
#else
				true;
#endif
		}

		///  <summary>
		///  Is this a valid object of type?
		/// 
		///  To be valid it needs to have object path null or empty and that predicate is verified
		///  </summary>
		///  <param name="target"></param>
		/// <param name="predicate"></param>
		/// <typeparam name="T"></typeparam>
		///  <returns></returns>
		public static bool IsValidObjectOfType<T>(T target, Predicate<T> predicate) where T : Object
		{
			return 
#if UNITY_EDITOR
				AssetDatabaseUtils.IsObjectPathNullOrEmpty(target) && 
#endif
				predicate.Invoke(target);
		}

		#endregion

		#region → Get Object(s) of Type
		
		/// <summary>
		/// Retrieve a hierarchical sorted component lists.
		/// 
		/// WARNING: This doesn't retrieve objects on "DontDestroyOnLoad".
		/// </summary>
		/// <param name="affectInactive"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> GetAllComponents<T>(bool affectInactive = false)
		{
			List<T> sortedComponents = new List<T>();
			
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				Scene currentScene = SceneManager.GetSceneAt(i);
				GameObject[] roots = currentScene.GetRootGameObjects();
				foreach (GameObject gameObject in roots)
				{
					sortedComponents.AddRange(gameObject.GetComponentsInChildren<T>(affectInactive));
				}
			}
			
			return sortedComponents;
		}

		/// <summary>
		/// Retrieve a hierarchical sorted component lists.
		/// 
		/// WARNING: This doesn't retrieve objects on "DontDestroyOnLoad".
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="affectInactive"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> GetAllComponents<T>(Predicate<T> predicate, bool affectInactive = false)
		{
			List<T> sortedComponents = new List<T>();
			
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				Scene currentScene = SceneManager.GetSceneAt(i);
				GameObject[] roots = currentScene.GetRootGameObjects();
				foreach (GameObject gameObject in roots)
				{
					sortedComponents.AddRange(gameObject.GetComponentsInChildren<T>(affectInactive).Where(predicate.Invoke));
				}
			}
			
			return sortedComponents;
		}

		#endregion
		
		#endregion
		
		
		#region Object → Methods

		/// <summary>
		/// <see cref="GameObjectUtils.BroadcastAll"/>
		/// </summary>
		/// <param name="methodName"></param>
		/// <param name="parameter"></param>
		/// <param name="messageOptions"></param>
		[Obsolete("2021/08/14 → Moved to GameObjectUtils.BroadcastAll. This will be removed in future versions")]
		public static void BroadcastAll(string methodName, object parameter = null, SendMessageOptions messageOptions = SendMessageOptions.DontRequireReceiver)
		{
			//GameObjectUtils.BroadcastAll(methodName, parameter, messageOptions);
			return;
		}
		
		/// <summary>
		/// Retrieve object instance id, returns 0 if it fails.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public static int GetID (Object target)
		{
			return target != null ? target.GetInstanceID() : 0;
		}
		
		#region Object → Safe Destroy of UnityEngine.Object

		#region → Object
	
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
					Debug.LogError(logName+$"{nameof(SafeDestroyInternal)} I cannot destroy a target already null!", debugContext);
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
					Debug.LogError(logName + $"{nameof(SafeDestroyInternal)} failed to destroy {nameof(target)}. Caught exception: {e.Message}.", debugContext);
				}
				return false;
			}
		}
		
		#endregion

		#region → Object[] 
		
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

			bool allDestroyed = true;
			bool isPlaying = Application.isPlaying;
			foreach (Object o in targets)
			{
				if (o == null)
				{
					if (debug)
					{
						Debug.LogError(logName + $"{nameof(SafeDestroyInternal)} an object already null!", debugContext);
					}

					continue;
				}

				try
				{
					if (isPlaying)
					{
						Object.Destroy(o);
					}
					else
					{
						Object.DestroyImmediate(o);
					}

				}
				catch (Exception e)
				{
					if (debug)
					{
						Debug.LogError(logName +$"{nameof(SafeDestroyInternal)} failed to destroy a target in targets. Caught exception: {e.Message}.", debugContext);
					}

					allDestroyed = false;
					continue;
				}
			}

			return allDestroyed;
		}
		
		#endregion

		#region → Object and his Requiring Components
		
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
					Debug.LogError(logName + $"{nameof(SafeDestroyWithRequiringComponentsInternal)} I cannot destroy a target already null!", debugContext);
				}
				return;
			}
			
			Type componentType = component.GetType();
			Component[] otherComponents = component.gameObject.GetComponents<Component>();
			
			List<Component> componentsToDestroy = new List<Component>();
			foreach (Component otherComponent in otherComponents)
			{
				if (otherComponent == null)
				{
					if (debug)
					{
						Debug.LogWarning(logName+$"{nameof(SafeDestroyWithRequiringComponentsInternal)} A component on gameObject {component.gameObject.name} is null.", debugContext);
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
					componentsToDestroy.AddRange(from requiredType in requiredTypes where requiredType != null && requiredType.IsAssignableFrom(componentType) select otherComponent);
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
			
			try
			{
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
			catch (Exception e)
			{
				if (debug)
				{
					Debug.LogError(logName + $"{nameof(SafeDestroyWithRequiringComponentsInternal)} failed to destroy {nameof(component)}. Caught exception: {e.Message}.", debugContext);
				}
			}
		}

		#endregion
		
		#endregion
		
		#region Object → Set UnityEngine.Object enabled property
		
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
					Debug.LogWarning(logName + $"{nameof(SetEnabledInternal)} {nameof(targets)} are null or empty.", debugContext);
				}
				return;
			}
			
			foreach (var target in targets)
			{
				if (target == null)
				{
					if (debug)
					{
						Debug.LogWarning(logName + $"{nameof(SetEnabledInternal)} null target found. Continue.", debugContext);
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
						if (ReflectionUtils.HasProperty(target, UtilsConstants.EnabledPropertyName, out PropertyInfo property))
						{
							property.SetValue(target, value, null);
						}
					}
					catch (Exception e)
					{
						if (debug)
						{
							Debug.LogError(logName + $"{nameof(SetEnabledInternal)} failed. Caught exception {e.Message}.", debugContext);
						}
					}
				}
			}
		}

		#endregion
		
		#endregion
		
	}
}