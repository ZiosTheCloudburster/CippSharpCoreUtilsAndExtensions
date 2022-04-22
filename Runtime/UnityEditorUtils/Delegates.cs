#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.EditorUtils
{
    /// <summary>
    /// For Iteration of an array of SerializedProperties
    /// </summary>
    /// <param name="element"></param>
    /// <param name="index"></param>
    public delegate void ForSerializedPropertyAction(SerializedProperty element, int index);
    
    /// <summary>
    /// Reference object callback.
    /// </summary>
    /// <param name="context"></param>
    public delegate void GenericRefAction(ref object context);
    
    /// <summary>
    /// Generic callback with serialized property
    /// </summary>
    /// <param name="property"></param>
    public delegate void SerializedPropertyAction(SerializedProperty property);
    
    
    /// <summary> 
    /// Custom Delegate to draw a serialized property
    /// 
    /// EditorGUILayout Context
    /// </summary>
    /// <param name="property"></param>
    public delegate void DrawSerializedPropertyDelegate(SerializedProperty property); 
    
    /// <summary>
    /// Custom Delegate to draw a serialized property <see cref="EditorGUI"/>
    ///
    /// EditorGUI Context
    ///
    /// Purpose: use a single referred rect and a delegate to draw anything
    /// </summary>
    /// <param name="rect">the rect used and edited to draw the property</param>
    /// <param name="property">the property to draw</param>
    public delegate void DrawSerializedPropertyDelegate1(ref Rect rect, SerializedProperty property);
    
    /// <summary>
    /// Custom Delegate to get property height
    /// </summary>
    /// <param name="property">the property to retrieve the height</param>
    public delegate float GetPropertyHeightDelegate(SerializedProperty property);
    
    
    /// <summary>
    /// Properties with attribute delegate
    /// </summary>
    /// <param name="target"></param>
    /// <param name="serializedObject"></param>
    /// <param name="properties"></param>
    public delegate void PropertiesWithAttributeDelegate(Object target, SerializedObject serializedObject, IList<SerializedProperty> properties);

}
#endif
