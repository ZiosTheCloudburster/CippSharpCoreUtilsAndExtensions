#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    }
}
#endif