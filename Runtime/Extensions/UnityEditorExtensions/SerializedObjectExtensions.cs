#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;

namespace CippSharp.Core.EditorExtensions
{
    using SerializedObjectUtils = CippSharp.Core.EditorUtils.SerializedObjectUtils;
    using SerializedPropertyUtils = CippSharp.Core.EditorUtils.SerializedPropertyUtils;
    //Delegates
    using DrawSerializedPropertyDelegate = CippSharp.Core.EditorUtils.DrawSerializedPropertyDelegate;
    using SerializedPropertyAction = CippSharp.Core.EditorUtils.SerializedPropertyAction;

    public static class SerializedObjectExtensions
    {
        /// <summary>
        /// To override the inspector of a SerializedObject
        ///
        /// Usage:
        /// serializedObject.Update();
        /// serializedObject.DrawCascadeInspector(callback);
        /// serializedObject.ApplyModifiedProperties(); 
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="drawPropertyDelegate"></param>
        /// <returns></returns>
        public static bool DrawCascadeInspector(this SerializedObject serializedObject, DrawSerializedPropertyDelegate drawPropertyDelegate)
        {
            return SerializedObjectUtils.DrawInspector(serializedObject, drawPropertyDelegate);
        }

        /// <summary>
        /// To override the inspector of a SerializedObject
        /// Different wording.
        ///
        /// Usage:
        /// serializedObject.Update();
        /// serializedObject.DrawInspector(callback);
        /// serializedObject.ApplyModifiedProperties(); 
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="drawPropertyDelegate"></param>
        /// <returns></returns>
        public static bool DrawInspector(this SerializedObject serializedObject, DrawSerializedPropertyDelegate drawPropertyDelegate)
        {
            return SerializedObjectUtils.DrawInspector(serializedObject, drawPropertyDelegate);
        }
        
        
        #region SerializedPropertyUtils → Iterators

        /// <summary>
        /// Similar to <see cref="EditorGUILayoutUtils.DrawInspector"/>, but only to iterate properties
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="elementDelegate"></param>
        public static void IterateAllProperties(this SerializedObject serializedObject, SerializedPropertyAction elementDelegate)
        {
            SerializedPropertyUtils.IterateAllProperties(serializedObject, elementDelegate);
        }

        #endregion
        
        #region SerializedPropertyUtils → Methods
        
        #region → Get Properties
	    
        /// <summary>
        /// It retrieves all serialized properties from <param name="serializedObject"></param> iterator.
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        public static List<SerializedProperty> GetAllProperties(this SerializedObject serializedObject)
        {
            return SerializedPropertyUtils.GetAllProperties(serializedObject);
        }

        /// <summary>
        /// It retrieves all serialized properties from <param name="serializedObject"></param> iterator that match predicate
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static List<SerializedProperty> GetAllProperties(this SerializedObject serializedObject, Predicate<SerializedProperty> predicate)
        {
            return SerializedPropertyUtils.GetAllProperties(serializedObject, predicate);
        }

        /// <summary>
        /// Select T from properties in <param name="serializedObject"></param> iterator that match predicate
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static List<T> SelectFromAllProperties<T>(this SerializedObject serializedObject, Predicate<SerializedProperty> predicate, Func<SerializedProperty, T> selector)
        {
            return SerializedPropertyUtils.SelectFromAllProperties(serializedObject, predicate, selector);
        }

        #endregion
        
        #endregion
    }
}
#endif
