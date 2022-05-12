using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.Core.Utils
{
    public static class TransformUtils
    {
        /// <summary>
        /// A better name for logs
        /// </summary>
        private static readonly string LogName = $"[{nameof(TransformUtils)}]: ";
                
        #region Transform → Brothers

        /// <summary>
        /// It finds a transform that is brother of the target.
        /// </summary>
        /// <returns></returns>
        public static Transform FindBrother(Transform target, string brotherName)
        {
            Transform parent = target.parent;
            if (parent == null)
            {
                var brothers = target.gameObject.scene.GetRootGameObjects();
                foreach (var brother in brothers)
                {
                    if (brother.name != brotherName)
                    {
                        continue;
                    }
					
                    return brother.transform;
                }
            }
            else
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    var child = parent.GetChild(i);
                    if (child.gameObject.name != brotherName)
                    {
                        continue;
                    }

                    return child;
                }
            }
			
            return null;
        }

        #endregion
        
        #region Transform → Children

        #region → Create Child 
        
        /// <summary>
        /// Allow to create a child if it isn't found!
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static Transform FindOrCreateChild(Transform parent, string childName)
        {
            Transform child = parent.Find(childName);
            if (child == null)
            {
                child = AddEmptyChild(parent, childName).transform;
            }
            return child;
        }
        
        /// <summary>
        /// Creates an empty child
        /// </summary>
        /// <param name="target"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static Transform AddEmptyChild(Transform target, string childName)
        {
            Transform child = new GameObject(childName).transform;
            child.SetParent(target, false);
            ResetLocals(child);
            return child.transform;
        }
        
        /// <summary>
        /// Creates a child with given components
        /// </summary>
        /// <param name="target"></param>
        /// <param name="childName"></param>
        /// <param name="components"></param>
        /// <returns></returns>
        public static Transform AddChild(Transform target, string childName, Type[] components)
        {
            Transform child = new GameObject(childName, components).transform;
            child.SetParent(target, false);
            ResetLocals(child);
            return child;
        }
        
        /// <summary>
        /// Creates a child with a primitive
        /// </summary>
        /// <param name="target"></param>
        /// <param name="childName"></param>
        /// <param name="primitiveType"></param>
        /// <returns></returns>
        public static Transform AddPrimitiveChild(Transform target, string childName, PrimitiveType primitiveType)
        {
            Transform child = GameObject.CreatePrimitive(primitiveType).transform;
            child.gameObject.name = childName;
            child.SetParent(target, false);
            ResetLocals(child);
            return child;
        }
        
        #endregion

        #region → Contains

        #region → GameObject
        
        /// <summary>
        /// Retrieve true if target is children, at any level
        /// </summary>
        /// <param name="root"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool Contains(Transform root, GameObject target)
        {
            return ContainsInternal(root, target, null, false);
        }
        
        /// <summary>
        /// Retrieve true if target is children, at any level
        /// </summary>
        /// <param name="root"></param>
        /// <param name="target"></param>
        /// <param name="debugContext"></param>
        /// <returns></returns>
        public static bool Contains(Transform root, GameObject target, Object debugContext)
        {
            return ContainsInternal(root, target, debugContext, true);
        }

        /// <summary>
        /// Retrieve true if target is children, at any level
        /// </summary>
        /// <param name="root"></param>
        /// <param name="target"></param>
        /// <param name="debugContext"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        private static bool ContainsInternal(Transform root, GameObject target, Object debugContext, bool debug)
        {
            string logName = debug ? StringUtils.LogName(debugContext) : string.Empty;
			
            if (root == null)
            {
                if (debug)
                {
                    Debug.LogError(logName+$"{nameof(ContainsInternal)} {nameof(root)} is null.", debugContext);
                }
                return false;
            }

            if (target == null)
            {
                if (debug)
                {
                    Debug.LogError(logName+$"{nameof(ContainsInternal)} {nameof(target)} is null.", debugContext);
                }
                return false;
            }

            return ContainsInternalRecursive(root, ref target);
        }

        /// <summary>
        /// Recursive check to find if any child is 'target'
        /// </summary>
        /// <param name="root">must not be null</param>
        /// <param name="target">must not be null</param>
        /// <returns></returns>
        private static bool ContainsInternalRecursive(Transform root, ref GameObject target)
        {
            if (root.gameObject == target)
            {
                return true;
            }

            int length = root.childCount;
            if (length <= 0)
            {
                return false;
            }
            
            for (int i = 0; i < length; i++)
            {
                var child = root.GetChild(i);
                if (ContainsInternalRecursive(child, ref target))
                {
                    return true;
                }
            }

            return false;
        }
        
        #endregion

        #endregion

        #region → Component 

        /// <summary>
        /// Contains any component of type T?
        /// </summary>
        /// <param name="root"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Contains<T>(Transform root)
        {
            T[] components = root != null ? root.GetComponentsInChildren<T>(true) : null;
            return !ArrayUtils.IsNullOrEmpty(components);
        }

        /// <summary>
        /// Contains specific component of type T?
        /// </summary>
        /// <param name="root"></param>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Contains<T>(Transform root, T component)
        {
            T[] components = root != null ? root.GetComponentsInChildren<T>(true) : null;
            return !ArrayUtils.IsNullOrEmpty(components) && (components.FirstOrDefault(c => c.Equals(component)) != null);
        }

        #endregion
        
        
        #region → Find

        /// <summary>
        /// Find a direct children even if he is inactive!
        /// </summary>
        /// <param name="target"></param>
        /// <param name="childName">direct child</param>
        /// <returns></returns>
        public static Transform FindInactive(Transform target, string childName)
        {
            for (int i = 0; i < target.childCount; i++)
            {
                Transform child = target.GetChild(i);
                if (child.gameObject.name == childName)
                {
                    return child/*e*/;
                }
            }

            return null;
        }
        
        /// <summary>
        /// Retrieve transform direct children.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static List<Transform> GetDirectChildren(Transform transform)
        {
            List<Transform> children = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                children.Add(child);
            }
            return children;
        }

        /// <summary>
        /// Try find and child at given path.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="path"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public static bool TryFind(Transform root, string path, out Transform child)
        {
            try
            {
                child = root.Find(path);
                return child != null;
            }
            catch 
            {
                child = null;
                return false;
            }
        }
        
        #endregion
        
        #region → Sorting Children Alphabetical

        /// <summary>
        /// Sort target children alphabetical.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="recursive"></param>
        public static void SortChildrenAlphabetical(Transform target, bool recursive)
        {
            SortChildrenAlphabeticalInternal(target, recursive, false, false);
        }

        /// <summary>
        /// Sort target children alphabetical.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="recursive"></param>
        /// <param name="editor"></param>
        /// <param name="debug"></param>
        public static void SortChildrenAlphabetical(Transform target, bool recursive, bool editor, bool debug = false)
        {
            SortChildrenAlphabeticalInternal(target, recursive, editor, debug);
        }

        /// <summary>
        /// Sort target children alphabetical.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="recursive"></param>
        /// <param name="editor">enables editor support</param>
        /// <param name="debug"></param>
        private static void SortChildrenAlphabeticalInternal(Transform target, bool recursive, bool editor, bool debug = false)
        {
            //If target is null returns.
            if (target == null)
            {
                if (debug)
                {
                    Debug.LogError(LogName + $"{nameof(SortChildrenAlphabeticalInternal)} {nameof(target)} is null.");
                }
                return;
            }
            
            Transform[] directUnsortedChildren = target.GetComponentsInChildren<Transform>(true).Where(c => c != target && c.parent == target).ToArray();
            //If children are null or empty returns.
            if (ArrayUtils.IsNullOrEmpty(directUnsortedChildren))
            {
                if (debug)
                {
                    Debug.LogWarning(LogName + $"{nameof(SortChildrenAlphabeticalInternal)} {nameof(target)} doesn't have children.", target);
                }
                return;
            }
      
#if UNITY_EDITOR
            if (editor)
            {
                Undo.RecordObject(target.gameObject, "Alphabetical Children Sorting");
            }
#endif
            //Unparent these children
            foreach (Transform child in directUnsortedChildren)
            {
#if UNITY_EDITOR
                if (editor)
                {
                    Undo.SetTransformParent(child, null, "Alphabetical Children Sorting");
                }
#endif
                child.SetParent(null, true);
            }

            //Sort children and reparent it again
            List<Transform> sortedChildren = directUnsortedChildren.OrderBy(c => c.gameObject.name).ToList();
            foreach (var child in sortedChildren)
            {
                child.SetParent(target, true);
            }

            //If recursive also sorted children are involved each to sort themselves.
            if (recursive)
            {
                foreach (var child in sortedChildren)
                {
                    SortChildrenAlphabeticalInternal(child, true, true);
                }
            }
            
#if UNITY_EDITOR
            if (editor)
            {
                EditorUtility.SetDirty(target.gameObject);
            }
#endif

            if (debug)
            {
                Debug.Log(LogName + $"{nameof(SortChildrenAlphabeticalInternal)} sorted {sortedChildren.Count.ToString()} children of {target.gameObject.name}.", target);
            }
        }
        
        #endregion
        
        #endregion
        
        #region Transform → Methods
         
        #region → Position
        
        /// <summary>
        /// Set local position X
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetLocalPositionX(Transform target, float value)
        {
            var localPosition = target.localPosition;
            localPosition.x = value;
            target.localPosition = localPosition; 
        }
      
        /// <summary>
        /// Set local position Y
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetLocalPositionY(Transform target, float value)
        {
            var localPosition = target.localPosition;
            localPosition.y = value;
            target.localPosition = localPosition;
        }
        
        /// <summary>
        /// Set local position Z
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetLocalPositionZ(Transform target, float value)
        {
            var localPosition = target.localPosition;
            localPosition.z = value;
            target.localPosition = localPosition; 
        }
        
        /// <summary>
        /// Set position X
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetPositionX(Transform target, float value)
        {
            var position = target.position;
            position.x = value;
            target.position = position; 
        }
      
        /// <summary>
        /// Set position Y
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetPositionY(Transform target, float value)
        {
            var position = target.position;
            position.y = value;
            target.position = position;
        }
        
        /// <summary>
        /// Set position Z
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetPositionZ(Transform target, float value)
        {
            var position = target.position;
            position.z = value;
            target.position = position; 
        }

        
        #endregion
        
        #region → Rotation

        /// <summary>
        /// Set local euler angles X value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetLocalEulerAngleX(Transform target, float value)
        {
            var localEulerAngles = target.localEulerAngles;
            localEulerAngles.x = value;
            target.localEulerAngles = localEulerAngles;
        }

        /// <summary>
        /// Set local euler angles Y value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetLocalEulerAngleY(Transform target, float value)
        {
            var localEulerAngles = target.localEulerAngles;
            localEulerAngles.y = value;
            target.localEulerAngles = localEulerAngles;
        }
        
        /// <summary>
        /// Set local euler angles Z value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetLocalEulerAngleZ(Transform target, float value)
        {
            var localEulerAngles = target.localEulerAngles;
            localEulerAngles.z = value;
            target.localEulerAngles = localEulerAngles;
        }
        
        /// <summary>
        /// Set local euler angles X value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetEulerAngleX(Transform target, float value)
        {
            var eulerAngles = target.eulerAngles;
            eulerAngles.x = value;
            target.eulerAngles = eulerAngles;
        }

        /// <summary>
        /// Set local euler angles Y value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetEulerAngleY(Transform target, float value)
        {
            var eulerAngles = target.eulerAngles;
            eulerAngles.y = value;
            target.eulerAngles = eulerAngles;
        }
        
        /// <summary>
        /// Set local euler angles Z value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetEulerAngleZ(Transform target, float value)
        {
            var eulerAngles = target.eulerAngles;
            eulerAngles.z = value;
            target.eulerAngles = eulerAngles;
        }
        
        #endregion

        #region → Scale

        /// <summary>
        /// Set Local Scale X
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetLocalScaleX(Transform target, float value)
        {
            var scale = target.localScale;
            scale.x = value;
            target.localScale = scale;
        }
        
        /// <summary>
        /// Set Local Scale Y
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetLocalScaleY(Transform target, float value)
        {
            var scale = target.localScale;
            scale.y = value;
            target.localScale = scale;
        }
        
        /// <summary>
        /// Set Local Scale Z
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetLocalScaleZ(Transform target, float value)
        {
            var scale = target.localScale;
            scale.z = value;
            target.localScale = scale;
        }

        /// <summary>
        /// Set Transform Lossy Scale 
        /// </summary>
        public static void SetLossyScale (Transform target, Vector3 lossyScale)
        {
            Transform parent = target.parent;
            target.SetParent(null);
            target.localScale = lossyScale;
            target.SetParent(parent);
        }
        
        #endregion
        
        #region → Resets
        
        /// <summary>
        /// Reset local position of a transform
        /// </summary>
        /// <param name="target"></param>
        public static void ResetLocalPosition(Transform target)
        {
            target.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Reset local rotation of a transform
        /// </summary>
        /// <param name="target"></param>
        public static void ResetLocalRotation(Transform target)
        {
            target.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// Reset local scale of a transform
        /// </summary>
        /// <param name="target"></param>
        public static void ResetLocalScale(Transform target)
        {
            target.localScale = Vector3.one;
        }  
        
        /// <summary>
        /// Reset locals of a transform
        /// </summary>
        /// <param name="target"></param>
        public static void ResetLocals(Transform target)
        {
            target.localPosition = Vector3.zero;
            target.localRotation = Quaternion.identity;
            target.localScale = Vector3.one;
        }
        
        /// <summary>
        /// Copy local properties of value transform into target.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value">must be different from null</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CopyLocalsFrom(Transform target, Transform value)
        {
            Transform originalParent = target.parent;
            target.SetParent(value.parent, false);
            target.localPosition = value.localPosition;
            target.localRotation = value.localRotation;
            target.localScale = value.localScale;
            target.SetParent(originalParent, true);
        }
        
        #region → Reset (preserve children transforms)

        /// <summary>
        /// Resets values of context transform while preserve children transforms.
        /// </summary>
        /// <param name="target"></param>
        public static void ResetLocalsPreserveChildren(Transform target)
        {
            ResetLocalsPreserveChildrenInternal(target, false, false);
        }

        /// <summary>
        /// Resets values of context transform while preserve children transforms.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="editor"></param>
        /// <param name="debug"></param>
        public static void ResetLocalsPreserveChildren(Transform target, bool editor, bool debug = false)
        {
            ResetLocalsPreserveChildrenInternal(target, editor, debug);
        }

        /// <summary>
        /// Resets values of context transform while preserve children transforms.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="editor"></param>
        /// <param name="debug"></param>
        private static void ResetLocalsPreserveChildrenInternal(Transform target, bool editor, bool debug = false)
        {
            //If target is null returns.
            if (target == null)
            {
                if (debug)
                {
                    Debug.LogError(LogName + $"{nameof(ResetLocalsPreserveChildrenInternal)} {nameof(target)} is null.");
                }
                return;
            }
            
            Transform[] directUnsortedChildren = target.GetComponentsInChildren<Transform>(true).Where(c => c != target && c.parent == target).ToArray();
            //If children are null or empty return.
            if (ArrayUtils.IsNullOrEmpty(directUnsortedChildren))
            {
                if (debug)
                {
                    Debug.LogWarning(LogName + $"{nameof(ResetLocalsPreserveChildrenInternal)} {nameof(target)} doesn't have children.");
                }

                ResetLocals(target);
                return;
            }
            
#if UNITY_EDITOR
            if (editor)
            {
                Undo.RecordObjects(directUnsortedChildren.Cast<Object>().ToArray(), "Reposition");
            }
#endif
            foreach (var unsortedChild in directUnsortedChildren)
            {
                unsortedChild.SetParent(null, true);
            }
            
            ResetLocals(target);
            
            foreach (var unsortedChild in directUnsortedChildren)
            {
                unsortedChild.SetParent(target, true);
            }
                       
#if UNITY_EDITOR
            if (editor)
            {
                EditorUtility.SetDirty(target.gameObject);
            }
#endif
            if (debug)
            {
                Debug.Log(LogName + $"{nameof(ResetLocalsPreserveChildrenInternal)} transform reset preserving children transform should be completed successful.", target);
            }
        }

        #endregion
        
        #endregion
   
        #region → Matrix

        /// <summary>
        /// Matrix TRS from Transform. Default is World Space.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Matrix4x4 TRS(Transform transform)
        {
            return Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        } 

        #endregion
        
        
        /// <summary>
        /// Returns true if target transform is in camera view.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool IsInViewOf(Transform target, Camera camera)
        {
            Vector3 screenPoint = camera.WorldToViewportPoint(target.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            return onScreen;
        }

        /// <summary>
        /// Returns true if the target is facing the other with an exclusive angle tolerance.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="other"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static bool IsFacing(Transform target, Transform other, float angle = 5.0f)
        {
            Vector3 lookDir = other.position - target.position;
 
            Vector3 myDir = target.forward;
            Vector3 yourDir =  other.forward;
 
            float myAngle = Vector3.Angle(myDir, lookDir);
            float yourAngle = Vector3.Angle(yourDir, -lookDir);
 
            return myAngle < angle && yourAngle < angle;
        }
        
        #endregion

        
        #region Transform → Path

        #region → Path
        
        /// <summary>
        /// Retrieve transform hierarchy path as string.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static string GetTransformPath(Transform transform)
        {
            string path = transform.gameObject.name;
            while (transform.parent != null)
            {
                transform = transform.parent;
                path = transform.gameObject.name + "/" + path;
            }
            return path;
        }

        /// <summary>
        /// Retrieve transform hierarchy relative path as string.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static string GetTransformRelativePath(Transform transform, Transform root)
        {
            string path = transform.gameObject.name;
            while (transform.parent != null)
            {
                transform = transform.parent;
                if (transform == root)
                {
                    break;
                }
                path = transform.gameObject.name + "/" + path;
            }
            return path;
        }
        
        #endregion

        #region → Nest Level
       
        /// <summary>
        /// Retrieve transform nest level.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static int GetTransformNestLevel(Transform transform)
        {
            int level = 0;
            while (transform.parent != null)
            {
                transform = transform.parent;
                level++;
            }
            return level;
        }
        
        #endregion
        
        #endregion
        
        
        #region Transform → Editor Utils
#if UNITY_EDITOR
        #region → Sort Children
        /// <summary>
        /// Sort children alphabetical
        /// </summary>
        /// <param name="command"></param>
        [MenuItem("CONTEXT/FolderObject/Sort Children Alphabetical")]
        [MenuItem("CONTEXT/Transform/Sort Children Alphabetical")]
        private static void SortChildrenAlphabetical(MenuCommand command)
        {
            // The transform component can be extracted from the menu command using the context field.
            Transform transform = command.context as Transform;
            SortChildrenAlphabeticalInternal(transform, false, true);
        }
        #endregion
        
        #region → Reset
        /// <summary>
        /// Reset values of context transform while preserve children transforms.
        /// </summary>
        /// <param name="command"></param>
        [MenuItem("CONTEXT/Transform/Reset (preserve children transform)")]
        private static void ResetLocalsPreserveChildren(MenuCommand command)
        {
            // The transform component can be extracted from the menu command using the context field.
            Transform transform = command.context as Transform;
            ResetLocalsPreserveChildrenInternal(transform, false, true);
        }
        #endregion
#endif
        #endregion
    }
}
