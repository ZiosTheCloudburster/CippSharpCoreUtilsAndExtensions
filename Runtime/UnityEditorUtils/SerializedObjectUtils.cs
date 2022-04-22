#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

namespace CippSharp.Core.EditorUtils
{
    using ArrayUtils = CippSharp.Core.Utils.ArrayUtils;
    
    public static class SerializedObjectUtils
    {
        #region SerializedObjectUtils → Draw Inspector
        
        /// <summary>
        /// Wrap of <see cref="EditorGUILayoutUtils.DrawInspector"/>
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="drawPropertyDelegate"></param>
        /// <returns></returns>
        public static bool DrawInspector(SerializedObject serializedObject, DrawSerializedPropertyDelegate drawPropertyDelegate)
        {
            return EditorGUILayoutUtils.DrawInspector(serializedObject, drawPropertyDelegate);
        }
        
        #endregion

        
        #region SerializedObjectUtils → Methods
        
        #region → Get
        
	    /// <summary>
	    /// It retrieves all serialized properties from <param name="serializedObject"></param> iterator.
	    /// </summary>
	    /// <param name="serializedObject"></param>
	    /// <returns></returns>
	    public static List<SerializedProperty> GetAllProperties(SerializedObject serializedObject)
	    {
		    return SerializedPropertyUtils.GetAllProperties(serializedObject);
	    }

	    /// <summary>
        /// Retrieve all targets objects from a <see cref="SerializedObject"/>
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<Object> GetTargetObjects<T>(T serializedObject) where T : SerializedObject
        {
            if (serializedObject == null)
            {
                return null;
            }

            Object target = serializedObject.targetObject;
            Object[] targets = serializedObject.targetObjects;
            List<Object> allObjects = new List<Object>();
            if (target != null)
            {
                allObjects.Add(target);
            }
            
            if (ArrayUtils.IsNullOrEmpty(targets))
            {
                return allObjects;
            }
            
            foreach (var o in targets)
            {
                if (o == null)
                {
                    continue;
                }

                if (allObjects.Contains(o))
                {
                    continue;
                }
					
                allObjects.Add(o);
            }

            return allObjects;
        }
        
        /// <summary>
        /// Get Pairs between active editor and edited objects
        /// </summary>
        /// <returns></returns>
        public static KeyValuePair<Editor, List<Object>>[] GetActiveEditorTargetsObjectsPairs()
        {
            Editor[] editors = ActiveEditorTracker.sharedTracker.activeEditors;
            KeyValuePair<Editor, List<Object>>[] pairs = new KeyValuePair<Editor, List<Object>>[editors.Length];
            for (int i = 0; i < editors.Length; i++)
            {
                Editor editor = editors[i];
                List<Object> editorTargets = GetTargetObjects(editor);
                pairs[i] = new KeyValuePair<Editor, List<Object>>(editor, editorTargets);
            }
            return pairs;
        }
        
        #endregion
        
        #endregion
    }
}
#endif