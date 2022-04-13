using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CippSharp.Core
{
    public static class GameObjectUtils
    {
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
                
        #region Brodcast All

        /// <summary>
        /// Calls the method named method Name on every MonoBehaviour in the scene.
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

        /// <summary>
        /// Remove target components from a gameobject
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
                    Component component = target.GetComponent(type);

                    if (component == null)
                    {
                        continue;
                    }

                    if (hardcore)
                    {
                        ObjectUtils.SafeDestroyWithRequiringComponents(component);
                    }
                    else
                    {
                        ObjectUtils.SafeDestroy(component);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
               Debug.LogError(e.Message, target);
            }

            return false;
        }

      
    }
}
