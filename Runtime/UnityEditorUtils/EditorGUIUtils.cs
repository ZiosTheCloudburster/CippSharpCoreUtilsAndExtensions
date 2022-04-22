//
// Author: Alessandro Salani (Cippo)
//
#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.EditorUtils
{
    public static partial class EditorGUIUtils
    {
        /// <summary>
        /// Wrap of unity's default single line height.
        /// </summary>
        public static readonly float SingleLineHeight = EditorGUIUtility.singleLineHeight;
  
        /// <summary>
        /// Wrap of unity's default vertical spacing between lines.
        /// </summary>
        public static readonly float VerticalSpacing = EditorGUIUtility.standardVerticalSpacing;
  
        /// <summary>
        /// Sum of <see cref="SingleLineHeight"/> + <seealso cref="VerticalSpacing"/>.
        /// </summary>
        public static readonly float LineHeight = SingleLineHeight + VerticalSpacing;
        
        
        
        #region EditorGUIUtils → Inspector & Property Drawers
        
        #region → Draw Inspector
        
        /// <summary>
        /// Draws an inspector of a serialized object where you specify how properties are drawn.
        /// </summary>
        /// <param name="sharedRect">the edited rect for the whole iteration</param>
        /// <param name="serializedObject"></param>
        /// <param name="drawPropertyDelegate"></param>
        /// <returns>has any changed?</returns>
        public static bool DrawInspector(ref Rect sharedRect, SerializedObject serializedObject, DrawSerializedPropertyDelegate1 drawPropertyDelegate)
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.UpdateIfRequiredOrScript();
            SerializedProperty iterator = serializedObject.GetIterator();
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                using (new EditorGUI.DisabledScope(UtilsConstants.ScriptSerializedPropertyName == iterator.propertyPath))
                {
                    drawPropertyDelegate.Invoke(ref sharedRect, iterator.Copy());
                }
            }
            serializedObject.ApplyModifiedProperties();
            return EditorGUI.EndChangeCheck();
        }
        
        #endregion
        
        #region → Get Inspector Height
        
        /// <summary>
        /// Get of an inspector of a serialized object where you specify how height is retrieved.
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="getHeightDelegate"></param>
        /// <returns></returns>
        public static float GetInspectorHeight(SerializedObject serializedObject, GetPropertyHeightDelegate getHeightDelegate)
        {
            serializedObject.UpdateIfRequiredOrScript();
            SerializedProperty iterator = serializedObject.GetIterator();
            float height = 0;
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                using (new EditorGUI.DisabledScope(UtilsConstants.ScriptSerializedPropertyName == iterator.propertyPath))
                {
                    height += getHeightDelegate.Invoke(iterator.Copy());
                }
            }
            serializedObject.ApplyModifiedProperties();
            return height < LineHeight ? LineHeight : height;
        }
        
        #endregion

        
        #region → Draw Property

        /// <summary>
        /// It draws the property only if it is different from null.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        public static void DrawProperty(Rect rect, SerializedProperty property)
        {
            if (property != null)
            {
                EditorGUI.PropertyField(rect, property, property.isExpanded && property.hasChildren);
            }
        }

        /// <summary>
        /// It draws the property only if it is different from null.
        ///
        /// Retrieve property height
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="height"></param>
        public static void DrawProperty(Rect rect, SerializedProperty property, out float height)
        {
            DrawProperty(ref rect, property);
            height = rect.height;
        }

        /// <summary>
        /// It draws the property only if it is different from null.
        ///
        /// Referred Rect is edited accordingly to property
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        public static void DrawProperty(ref Rect rect, SerializedProperty property)
        {
            if (property != null)
            {
                rect.height = GetPropertyHeight(property);
                EditorGUI.PropertyField(rect, property, property.isExpanded && property.hasChildren);
                rect.y += rect.height + VerticalSpacing;
            }
        }
        
        //With Label
        /// <summary>
        /// It draws the property only if its different from null.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public static void DrawProperty(Rect rect, SerializedProperty property, GUIContent label)
        { 
            if (property != null)
            {
                EditorGUI.PropertyField(rect, property, label, property.isExpanded && property.hasChildren);
            }
        }

        /// <summary>
        /// It draws the property only if it is different from null.
        /// 
        /// Retrieve property height
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <param name="height"></param>
        public static void DrawProperty(Rect rect, SerializedProperty property, GUIContent label, out float height)
        {
            DrawProperty(ref rect, property, label);
            height = rect.height + VerticalSpacing;
        }

        /// <summary>
        /// It draws the property only if its different from null.
        ///
        /// Referred Rect is edited accordingly to property
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public static void DrawProperty(ref Rect rect, SerializedProperty property, GUIContent label)
        { 
            if (property != null)
            {
                rect.height = GetPropertyHeight(property);
                EditorGUI.PropertyField(rect, property, label, property.isExpanded && property.hasChildren);
                rect.y += rect.height + VerticalSpacing;
            }
        }

        #endregion

        #region → Draw NotEditable Property

        /// <summary>
        /// It draws the property only if it is different from null.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        public static void DrawNotEditableProperty(Rect rect, SerializedProperty property)
        {
            if (property != null)
            {
                bool enabled =  GUI.enabled; 
                GUI.enabled = false;
                EditorGUI.PropertyField(rect, property, property.isExpanded && property.hasChildren);
                GUI.enabled = enabled;
            }
        }

        /// <summary>
        /// It draws the property only if it is different from null.
        ///
        /// Retrieve property height
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="height"></param>
        public static void DrawNotEditableProperty(Rect rect, SerializedProperty property, out float height)
        {
            DrawProperty(ref rect, property);
            height = rect.height;
        }

        /// <summary>
        /// It draws the property only if it is different from null.
        ///
        /// Referred Rect is edited accordingly to property
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        public static void DrawNotEditableProperty(ref Rect rect, SerializedProperty property)
        {
            if (property != null)
            {
                bool enabled =  GUI.enabled; 
                GUI.enabled = false;
                rect.height = GetPropertyHeight(property);
                EditorGUI.PropertyField(rect, property, property.isExpanded && property.hasChildren);
                rect.y += rect.height + VerticalSpacing;
                GUI.enabled = enabled;
            }
        }

        //With Label
        /// <summary>
        /// It draws the property only if its different from null.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public static void DrawNotEditableProperty(Rect rect, SerializedProperty property, GUIContent label)
        { 
            if (property != null)
            {
                bool enabled =  GUI.enabled; 
                GUI.enabled = false;
                EditorGUI.PropertyField(rect, property, label, property.isExpanded && property.hasChildren);
                GUI.enabled = enabled;
            }
        }

        /// <summary>
        /// It draws the property only if it is different from null.
        /// 
        /// Retrieve property height
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <param name="height"></param>
        public static void DrawNotEditableProperty(Rect rect, SerializedProperty property, GUIContent label, out float height)
        {
            DrawProperty(ref rect, property, label);
            height = rect.height + VerticalSpacing;
        }

        /// <summary>
        /// It draws the property only if its different from null.
        ///
        /// Referred Rect is edited accordingly to property
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public static void DrawNotEditableProperty(ref Rect rect, SerializedProperty property, GUIContent label)
        { 
            if (property != null)
            {
                bool enabled =  GUI.enabled; 
                GUI.enabled = false;
                rect.height = GetPropertyHeight(property);
                EditorGUI.PropertyField(rect, property, label, property.isExpanded && property.hasChildren);
                rect.y += rect.height + VerticalSpacing;
                GUI.enabled = enabled;
            }
        }

       

        #endregion

        #region → Draw Property (with delegats or iterators)

        /// <summary>
        /// Draws a foldout at position, then
        /// if it is expanded it invokes the delegate.
        ///
        /// USAGE: use this for expandable properties, or
        /// per fast CustomPropertyDrawers
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="drawSerializedPropertyDelegate"></param>
        /// <returns></returns>
        public static void DrawPropertyWithFoldout(Rect position, SerializedProperty property, DrawSerializedPropertyDelegate1 drawSerializedPropertyDelegate)
        {
            DrawFoldout(ref position, property);

            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                drawSerializedPropertyDelegate.Invoke(ref position, property);

                EditorGUI.indentLevel--;
            }
        }

        /// <summary>
        /// Draws a foldout at position, then 
        /// if it is expanded it draws by iterator
        ///
        /// USAGE: use this for expandable properties, or
        /// per fast CustomPropertyDrawers
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="editable"></param>
        public static void DrawPropertyIterator(Rect position, SerializedProperty property, bool editable = true)
        {
            DrawFoldout(ref position, property);
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                bool enabled =  GUI.enabled; 
                GUI.enabled = editable;
                
                IEnumerator iterator = property.GetEnumerator();
                while (iterator.MoveNext())
                {
                    SerializedProperty element = ((SerializedProperty) iterator.Current)?.Copy();
                    position.height = GetPropertyHeight(element);
                    DrawProperty(ref position, element);
                    position.y += position.height + VerticalSpacing;
                }
                
                GUI.enabled = enabled;
                EditorGUI.indentLevel--;
            }
        }

        #endregion
        
        
        #region → Get Property Height

        /// <summary>
        /// Retrieve the height of property's rect.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="includeVerticalSpacing"></param>
        /// <returns></returns>
        public static float GetPropertyHeight(SerializedProperty property, bool includeVerticalSpacing = false)
        {
            if (property == null) { return 0; }
            
            return EditorGUI.GetPropertyHeight(property, property.isExpanded && property.hasChildren) + (includeVerticalSpacing ? VerticalSpacing : 0);
        }

        /// <summary>
        /// Retrieve the height of property's rect.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <param name="includeVerticalSpacing"></param>
        /// <returns></returns>
        public static float GetPropertyHeight(SerializedProperty property, GUIContent label, bool includeVerticalSpacing = false)
        {
            if (property == null) { return 0; }
            
            return EditorGUI.GetPropertyHeight(property, label, property.isExpanded && property.hasChildren) + (includeVerticalSpacing ? VerticalSpacing : 0);
        }
        
        #endregion
        
        #region → Get Property Height (with delegats or iterators)
        
        /// <summary>
        /// Calculate a standard generic property height with a delegate.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="getPropertyHeightDelegate"></param>
        /// <returns></returns>
        public static float GetPropertyHeight(SerializedProperty property, GetPropertyHeightDelegate getPropertyHeightDelegate)
        {
            float h = LineHeight;
            if (property.isExpanded)
            {
                h += getPropertyHeightDelegate.Invoke(property);
            }
            return h;
        }

        /// <summary>
        /// Get Height by iterator
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static float GetPropertyHeightIterator(SerializedProperty property)
        {
            float h = LineHeight;

            if (property.isExpanded)
            {
                IEnumerator iterator = property.GetEnumerator();
                while (iterator.MoveNext())
                {
                    SerializedProperty element = ((SerializedProperty) iterator.Current)?.Copy();
                    h += GetPropertyHeight(element, true);
                }
            }

            return h;
        }
        
        #endregion
        
        #endregion
        
        #region EditorGUIUtils → Other Drawers

        #region → Draw Button

        /// <summary>
        /// Draws a button
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="style"></param>
        public static void DrawButton(Rect position, string name, GUIStyle style = null)
        {
            if (style == null)
            {
                if (GUI.Button(position, name))
                {
                    
                }
            }
            else
            {
                if (GUI.Button(position, name, style))
                {
                    
                }
            }
        }

        /// <summary>
        /// Draws a button with callback
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="clickCallback"></param>
        /// <param name="style"></param>
        public static void DrawButtonWithCallback(Rect position, string name, Action clickCallback, GUIStyle style = null)
        {
            if (style == null)
            {
                if (GUI.Button(position, name))
                {
                    clickCallback.Invoke();
                }
            }
            else
            {
                if (GUI.Button(position, name, style))
                {
                    clickCallback.Invoke();
                }
            }
        }

        /// <summary>
        /// Draws a Button with callbacks
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="clickCallback"></param>
        /// <param name="notClickedCallback"></param>
        /// <param name="style"></param>
        public static void DrawButtonWithCallback(Rect position, string name, Action clickCallback, Action notClickedCallback, GUIStyle style = null)
        {
            if (style == null)
            {
                if (GUI.Button(position, name))
                {
                    clickCallback.Invoke();
                }
                else
                {
                    notClickedCallback.Invoke();
                }
            }
            else
            {
                if (GUI.Button(position, name, style))
                {
                    clickCallback.Invoke();
                }
                else
                {
                    notClickedCallback.Invoke();
                }
            }
        }

        #endregion

        #region → Draw Foldout
        
        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        public static void DrawFoldout(Rect position, SerializedProperty property)
        {
            DrawFoldout(ref position, property);
        }

        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="editedPosition">the edited position value</param>
        public static void DrawFoldout(Rect position, SerializedProperty property, out Rect editedPosition)
        {
            DrawFoldout(ref position, property);
            editedPosition = position;
        }

        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="height">rect height</param>
        public static void DrawFoldout(Rect position, SerializedProperty property, out float height)
        {
            DrawFoldout(ref position, property);
            height = position.height + VerticalSpacing;
        }

        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position">will be edited on height to SingleLineHeight and in Y by adding the LineHeight</param>
        /// <param name="property"></param>
        public static void DrawFoldout(ref Rect position, SerializedProperty property)
        {
            position.height = SingleLineHeight;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, property.displayName);
            position.y += LineHeight;
        }

        #endregion
        
        #region → Draw HelpBox
        
        /// <summary>
        /// Draw an help box with the passed rect and text.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(Rect rect, string text, MessageType messageType = MessageType.Info)
        {
            EditorGUI.HelpBox(rect, text, messageType);
        }

        /// <summary>
        /// Draw an help box with the passed rect and text.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        /// <param name="textHeight"></param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(Rect rect, string text, out float textHeight, MessageType messageType = MessageType.Info)
        {
            DrawHelpBox(ref rect, text, messageType);
            textHeight = rect.height;
        }
        
        /// <summary>
        /// Draw an help box with referred rect and text.
        /// Rect will be edited accordingly to text.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(ref Rect rect, string text, MessageType messageType = MessageType.Info)
        {
            rect.height = GetHelpBoxTextHeight(rect.width, text);
            EditorGUI.HelpBox(rect, text, messageType);
            rect.y += rect.height + VerticalSpacing;
        }

        /// <summary>
        /// The height of an help box based on his text message.
        /// </summary>
        /// <param name="helpBoxMessage"></param>
        /// <returns></returns>
        public static float GetHelpBoxTextHeight(string helpBoxMessage)
        {
            return GetHelpBoxTextHeight(Screen.width, helpBoxMessage);
        }

        /// <summary>
        /// The height of an help box based on his text message.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="helpBoxMessage"></param>
        /// <returns></returns>
        public static float GetHelpBoxTextHeight(float width, string helpBoxMessage)
        {
            GUIStyle style = EditorStyles.helpBox;
            GUIContent descriptionWrapper = new GUIContent(helpBoxMessage);
            return style.CalcHeight(descriptionWrapper, width);
        }

        #endregion
        
        #region → Draw Labels
        
        /// <summary>
        /// Draw a label with the passed text
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text">doesn't draw if text is null or empty</param>
        public static void DrawHeader(Rect rect, string text)
        {
            DrawHeader(ref rect, text);
        }

        /// <summary>
        /// Draw a label with the passed text
        ///
        /// referred rect is edited
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text">doesn't draw if text is null or empty</param>
        public static void DrawHeader(ref Rect rect, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                rect.height = SingleLineHeight;
                EditorGUI.LabelField(rect, new GUIContent(text), EditorStyles.boldLabel);
                rect.y += rect.height + VerticalSpacing;
            }
        }

        #endregion
        
        #endregion
        
        
        
        #region EditorGUIUtils → Obsolescences

        #region → Draw NotEditable Property
            
        /// <summary>
        /// It draws the property only in a readonly way only if its different from null.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        [Obsolete("2022/04/22 → split methods, use DrawNotEditableProperty instead.")]
        public static void DrawNotEditableProperty1(Rect rect, SerializedProperty property, GUIContent label = null)
        {
            DrawNotEditableProperty(ref rect, property, label);
        }
        
        /// <summary>
        /// It draws the property only in a readonly way only if its different from null.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        [Obsolete("2022/04/22 → split methods, use DrawNotEditableProperty instead.")]
        public static void DrawNotEditableProperty1(ref Rect rect, SerializedProperty property, GUIContent label = null)
        { 
            if (property != null)
            {
                bool enabled =  GUI.enabled; 
                GUI.enabled = false;
                
                if (label != null)
                {
                    EditorGUI.PropertyField(rect, property, label, property.isExpanded && property.hasChildren);
                }
                else
                {
                    EditorGUI.PropertyField(rect, property, property.isExpanded && property.hasChildren);
                }
                
                GUI.enabled = enabled;
            }
        }
        
        #endregion
        
        #endregion
    }
}
#endif
