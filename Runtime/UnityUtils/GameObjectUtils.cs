using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Utils
{
    public static class GameObjectUtils
    {
        /// <summary>
        /// A better name for logs
        /// </summary>
        private static readonly string LogName = $"[{nameof(GameObjectUtils)}]: ";

        #region GameObject Generic
        
        #region → Brodcast All

        /// <summary>
        /// Calls the method named "methodName" on every MonoBehaviour that can be reached this way.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameter"></param>
        /// <param name="messageOptions"></param>
        public static void BroadcastAll(string methodName, object parameter = null, SendMessageOptions messageOptions = SendMessageOptions.DontRequireReceiver)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene currentScene = SceneManager.GetSceneAt(i);
                GameObject[] roots = currentScene.GetRootGameObjects();
                foreach (var gameObject in roots)
                {
                    gameObject.BroadcastMessage(methodName, parameter, messageOptions);
                }
            }
        }

        #endregion

        #region → Cast

        /// <summary>
        /// Retrieve the first gameObject component of type T.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T As<T> (GameObject gameObject)
        {
            return gameObject.GetComponent<T>();
        }
        
         
        /// <summary>
        /// Generic Object as GameObject
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static GameObject From(Object target)
        {
            switch (target)
            {
                case GameObject g:
                    return g;
                case Component c:
                    return c.gameObject;
                default:
                    return null;
            }
        }

        /// <summary>
        /// From a list of Objects this filters all possible GameObjects in targets.
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        public static List<GameObject> FilterAll(Object[] targets)
        {
            List<GameObject> filtered = new List<GameObject>();
            foreach (var target in targets)
            {
                if (target == null)
                {
                    continue;
                }

                GameObject result = From(target);
                if (result != null)
                {
                    ArrayUtils.AddIfNew(filtered, result);
                }
            }
            return filtered;
        }

        /// <summary>
        /// From a list of Objects this filters only GameObjects in targets.
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        public static List<GameObject> FilterGameObjectsOnly(Object[] targets)
        {
            List<GameObject> filtered = new List<GameObject>();
            foreach (var target in targets)
            {
                if (target == null)
                {
                    continue;
                }

                if (target is GameObject g)
                {
                    ArrayUtils.AddIfNew(filtered, g);
                }
            }
            return filtered;
        }


        #endregion
        
        #region → Contains
        
        /// <summary>
        /// Contains another GameObject?
        /// </summary>
        /// <param name="root"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool Contains(GameObject root, GameObject target)
        {
            return TransformUtils.Contains(root != null ? root.transform : null, target);
        }

        /// <summary>
        /// Contains any component of type T?
        /// </summary>
        /// <param name="root"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Contains<T>(GameObject root)
        {
            return TransformUtils.Contains<T>(root != null ? root.transform : null);
        }

        /// <summary>
        /// Contains specific component of type T?
        /// </summary>
        /// <param name="root"></param>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Contains<T>(GameObject root, T component)
        {
            return TransformUtils.Contains<T>(root != null ? root.transform : null, component);
        }

        #endregion

        #endregion
        
        
        #region → AddComponent

        /// <summary>
        /// Add specific components to generic bunch of targets
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="dontAddComponentIfItAlreadyExists"></param>
        public static List<T> AddComponentToTargets<T>(Object[] targets, bool dontAddComponentIfItAlreadyExists = true) where T : Component
        {
            List<T> addedComponents = new List<T>();
            List<GameObject> filteredGameObjects = FilterAll(targets);
            if (ArrayUtils.IsNullOrEmpty(filteredGameObjects))
            {
                return addedComponents;
            }
			
            foreach (var filteredGameObject in filteredGameObjects)
            {
                T componentToAdd = filteredGameObject.GetComponent<T>();
                if (dontAddComponentIfItAlreadyExists)
                {
                    if (componentToAdd == null)
                    {
                        componentToAdd = filteredGameObject.AddComponent<T>();
                    }
                }
                else
                {
                    componentToAdd = filteredGameObject.AddComponent<T>();
                }
				
                addedComponents.Add(componentToAdd);

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(filteredGameObject);
#endif
            }

            return addedComponents;
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
        public static T GetOrAddComponent<T>(GameObject target) where T : Component
        {
            T component = target.GetComponent<T>();
            if (component == null)
            {
                component = target.AddComponent<T>();
            }
            return component;
        }
        
        #endregion
        
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


        #endregion

        #region → RemoveComponent
        
        /// <summary>
        /// Remove last component of type T from a bunch of targets
        /// </summary>
        /// <param name="targets"></param>
        /// <typeparam name="T"></typeparam>
        public static void RemoveLastComponentFromTargets<T>(Object[] targets) where T : Component
        {
            List<GameObject> filteredGameObjects = FilterAll(targets);
            if (ArrayUtils.IsNullOrEmpty(filteredGameObjects))
            {
                return;
            }

            foreach (var filteredGameObject in filteredGameObjects)
            {
                T[] components = filteredGameObject.GetComponents<T>();
                if (ArrayUtils.IsNullOrEmpty(components))
                {
                    continue;
                }

                int length = components.Length;
                ObjectUtils.SafeDestroy(components[length - 1]);
                
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(filteredGameObject);
#endif
            }
        }
        
        /// <summary>
        /// Remove target components from a gameObject
        /// </summary>
        /// <param name="target"></param>
        /// <param name="types"></param>
        /// <param name="hardcore">if true it performs a 'SafeDestroyWithRequiringComponents'</param>
        public static bool TryRemoveComponents(GameObject target, Type[] types, bool hardcore = false)
        {
            try
            {
                foreach (Type type in types)
                {
                    if (type == null) 
                    {
                        continue;
                    }
                    
                    Component[] components = target.GetComponents(type);
                    if (ArrayUtils.IsNullOrEmpty(components))
                    {
                        continue;
                    }

                    for (int i = components.Length - 1; i >= 0; i--)
                    {
                        Component component = components[i];
                        try
                        {
                            if (hardcore)
                            {
                                ObjectUtils.SafeDestroyWithRequiringComponents(component);
                            }
                            else
                            {
                                ObjectUtils.SafeDestroy(component);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(LogName +$"{nameof(TryRemoveComponents)} components remove of type {type.Name} loop at {i.ToString()}. Caught exception: {e.Message}.", target);
                            continue;
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(LogName+$"{nameof(TryRemoveComponents)} failed. Caught exception: {e.Message}.", target);
            }

            return false;
        }
        
        #endregion

      
    }
}
