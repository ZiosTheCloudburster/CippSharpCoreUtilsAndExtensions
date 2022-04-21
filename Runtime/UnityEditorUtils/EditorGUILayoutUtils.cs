#if UNITY_EDITOR
//
// Author: Alessandro Salani (Cippo)
//
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace CippSharp.Core.EditorUtils
{
	public static partial class EditorGUILayoutUtils
	{
		/// <summary>
		/// The name that I like for logs.
		/// </summary>
		private static readonly string LogName = $"[{nameof(EditorGUILayoutUtils)}]: ";
		
		//CONSTANTS
		public const string inspectorModePropertyName = "inspectorMode";
		
		private const string instanceIdLabelValue = "Instance ID";
		private const string identifierLabelValue = "Local Identfier in File";
		private const string selfLabelValue = "Self";
		
//		public const string k_BackingField = SerializedPropertyUtils.k_BackingField;

		/// <summary>
		/// UnityEditor's default name for inspected objects local identfier.
		///
		/// That's not a typo. It is the real name of Unity variable.
		/// </summary>
		public const string LocalIdentfierInFilePropertyName = "m_LocalIdentfierInFile";
		
		
		#region EditorGUILayout → Generic Object 
		
		#region → GetLocalIdentfierInFile
		
		/// <summary>
		/// WARNING: This works only in a custom editor OnEnable(). You must cache it if you want to display it.
		/// It retrieves the m_LocalIdentfierInFile of an Object.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public static int GetLocalIdentfierInFile(Object target)
		{
			try
			{
				if (EditorUtility.IsPersistent(target))
				{
					PropertyInfo inspectorModeInfo = typeof(SerializedObject).GetProperty(inspectorModePropertyName, BindingFlags.NonPublic | BindingFlags.Instance);
					SerializedObject serializedObject = new SerializedObject(target);
					inspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);
					SerializedProperty localIdProp =serializedObject.FindProperty(LocalIdentfierInFilePropertyName);
					return localIdProp.intValue;
				}
			}
			catch (Exception e)
			{
				Debug.LogError(LogName +$"{nameof(GetLocalIdentfierInFile)} failed to retrieve {LocalIdentfierInFilePropertyName}. Caught exception: {e.Message}.");	
			}
			
			return 0;
		}
		
		#endregion
		
		#endregion

		#region EditorGUILayout → Inspector & Property Drawers

		#region → Draw Inspector
     
		/// <summary>
		/// Allow to draw an entire inspector by passing a delegate to draws the SerializedProperty.
		///
		/// USAGE: foreach element (<see cref="SerializedProperty"/>) found in the <param name="serializedObject"></param> iterator,
		/// this will invoke a delegate where you can override the draw of each or of some properties.
		/// </summary>
		/// <param name="serializedObject"></param>
		/// <param name="drawPropertyDelegate"></param>
		/// <returns></returns>
		public static bool DrawInspector(SerializedObject serializedObject, DrawSerializedPropertyDelegate drawPropertyDelegate)
		{
			EditorGUI.BeginChangeCheck();
			serializedObject.UpdateIfRequiredOrScript();
			SerializedProperty iterator = serializedObject.GetIterator();
			for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
			{
				using (new EditorGUI.DisabledScope(UtilsConstants.ScriptSerializedPropertyName == iterator.propertyPath))
				{
					drawPropertyDelegate.Invoke(iterator.Copy());
				}
			}
			serializedObject.ApplyModifiedProperties();
			return EditorGUI.EndChangeCheck();
		}

		#endregion

		#region → Draw Property

		/// <summary>
		/// It draws the property only if its different from null.
		/// </summary>
		/// <param name="property"></param>
		public static void DrawProperty(SerializedProperty property)
		{
			if (property != null)
			{
				EditorGUILayout.PropertyField(property, property.isExpanded && property.hasChildren);
			}
		}
		
		/// <summary>
		/// It draws the property only if its different from null.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="label"></param>
		public static void DrawProperty(SerializedProperty property, GUIContent label)
		{
			if (property != null)
			{
				EditorGUILayout.PropertyField(property, label, property.isExpanded && property.hasChildren);
			}
		}
	
		/// <summary>
		/// It draws the property only if its different from null in a not-editable way.
		/// </summary>
		/// <param name="property"></param>
		public static void DrawNotEditableProperty(SerializedProperty property)
		{	
			bool guiEnabled;
			guiEnabled = GUI.enabled;
			GUI.enabled = false;
			if (property != null)
			{
				EditorGUILayout.PropertyField(property, property.isExpanded && property.hasChildren);
			}
			GUI.enabled = guiEnabled;
		}
		
		#endregion
		
		#region → Draw ArrayProperty

		/// <summary>
		/// Draws an array only if it isn't null or empty.
		/// </summary>
		/// <param name="arrayProperty"></param>
		public static void DrawArrayIfNotEmpty(SerializedProperty arrayProperty)
		{
			if (arrayProperty != null && arrayProperty.isArray && arrayProperty.arraySize > 0)
			{
				EditorGUILayout.PropertyField(arrayProperty, arrayProperty.isExpanded && arrayProperty.hasChildren);
			}
		}

		/// <summary>
		/// Draws a readonly array only if it isn't null or empty.
		/// </summary>
		/// <param name="arrayProperty"></param>
		public static void DrawNotEditableArrayIfNotEmpty(SerializedProperty arrayProperty)
		{
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			if (arrayProperty != null && arrayProperty.isArray && arrayProperty.arraySize > 0)
			{
				EditorGUILayout.PropertyField(arrayProperty, arrayProperty.isExpanded && arrayProperty.hasChildren);
			}
			GUI.enabled = enabled;
		}
	    
		#endregion
		
		#endregion
		
		#region EditorGUILayout → Other Drawers

		#region → Draw ArrayPage
        
        /// <summary>
		/// Display array per pages to avoid high usage to show all elements at once.
		/// Return the index of displayed page.
		/// </summary>
		/// <param name="label"></param>
		/// <param name="currentPage">.</param>
		/// <param name="arrayProperty"></param>
		/// <param name="elementsPerPage"></param>
		/// <param name="notEditable"></param>
		public static int DrawArrayPage(string label, int currentPage, SerializedProperty arrayProperty, int elementsPerPage = 10, bool notEditable = true)
		{
			if (arrayProperty == null || !arrayProperty.isArray)
			{
				const string PropertyIsNotArrayError = "Property isn't an array.";
				Debug.LogError(LogName+$"{nameof(DrawArrayPage)} {PropertyIsNotArrayError}");
				return currentPage;
			}

			if (arrayProperty.isArray && arrayProperty.arraySize < 1)
			{
				const string PropertyIsNotValidArrayWarning = "Property isn't a valid array.";
				Debug.LogWarning(LogName+ $"{nameof(DrawArrayPage)} {PropertyIsNotValidArrayWarning}");
				return currentPage;
			}
			
			GUILayout.Space(15);
			arrayProperty.isExpanded = EditorGUILayout.Foldout(arrayProperty.isExpanded, label, EditorStyles.foldout);

			if (!arrayProperty.isExpanded)
			{
				return currentPage;
			}
			
			int pagesLength = (Mathf.CeilToInt((float) arrayProperty.arraySize / (float) elementsPerPage))-1;
			if (currentPage > pagesLength)
			{
				Debug.LogWarning("Pages index out of range! Last page will be drawn instead.");
			}
			
			EditorGUI.indentLevel++;
			int pagesIndex = Mathf.Clamp(currentPage, 0, pagesLength);
			pagesIndex = EditorGUILayout.IntSlider(pagesIndex, 0, pagesLength);
			
			EditorGUILayout.LabelField($"Displaying page: {pagesIndex.ToString()}/{pagesLength.ToString()}.");
			EditorGUI.indentLevel++;
			bool guiEnabled = GUI.enabled;
			GUI.enabled = !notEditable;
			int startingArrayElementsIndex = pagesIndex * elementsPerPage;
			int displayedElementsCount = 0;
			for (int i = 0; i < elementsPerPage; i++)
			{
				int arrayElementIndex = startingArrayElementsIndex + i;
				if (arrayElementIndex >= arrayProperty.arraySize)
				{
					continue;
				}
				
				SerializedProperty element = arrayProperty.GetArrayElementAtIndex(arrayElementIndex);
				DrawProperty(element);
				displayedElementsCount++;
			}
			GUI.enabled = guiEnabled;
			
			EditorGUILayout.LabelField($"Displaying elements: {displayedElementsCount.ToString()}/{elementsPerPage.ToString()}");
			
			EditorGUI.indentLevel--;
			EditorGUI.indentLevel--;
			return pagesIndex;
		}
        
        #endregion
		
		#region → Draw Button

		/// <summary>
		/// Draws a minibutton
		/// </summary>
		/// <param name="name"></param>
		public static void DrawMiniButton(string name)
		{
			DrawButton(name, EditorStyles.miniButton);
		}

		/// <summary>
		/// Draws a minibutton that calls, if pressed, the action.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="action"></param>
		public static void DrawMiniButton(string name, Action action)
		{
			DrawButton(name, action, EditorStyles.miniButton);
		}
		
		/// <summary>
		/// Draws a button
		/// </summary>
		/// <param name="name"></param>
		/// <param name="style"></param>
		public static void DrawButton(string name, GUIStyle style)
		{
			if (GUILayout.Button(name, style))
			{
			    
			}
		}

		/// <summary>
		/// Draws a button that calls, if pressed, the action
		/// </summary>
		/// <param name="name"></param>
		/// <param name="action"></param>
		/// <param name="style"></param>
		public static void DrawButton(string name, Action action, GUIStyle style)
		{
			if (GUILayout.Button(name, style))
			{
				action.Invoke();
			}
		}

		#endregion

		#region → Draw Drag'n'DropArea
	
		/// <summary>
		/// Display a drag 'n' drop area with a callback 
		/// </summary>
		/// <param name="hint"></param>
		/// <param name="onDrop">for drag 'n' dropped objects</param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public static void DragNDropArea(string hint, Action<Object[]> onDrop, float width = 0, float height = 20)
		{
			GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
			Rect rect = GUILayoutUtility.GetRect(width, height, GUILayout.ExpandWidth(true));
			GUI.Box(rect, hint, EditorStyles.miniButton);
			if (rect.Contains(Event.current.mousePosition))
			{
				if (Event.current.type == EventType.DragUpdated)
				{
					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
					Event.current.Use ();
				}   
				else if (Event.current.type == EventType.DragPerform)
				{
					Object[] objects = DragAndDrop.objectReferences;
					onDrop.Invoke(objects);
					Event.current.Use ();
				}
			}
			GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
		}

		/// <summary>
		/// Display a drag 'n' drop area with on drop callback
		/// </summary>
		/// <param name="onDrop">use this to retrieve dropped object using DragAndDrop.objectReferences from your script</param>
		/// <param name="height"></param>
		public static void DragNDropArea(Action onDrop, float height = 45)
		{
			DragNDropArea("Drag and Drop files to this Box!", onDrop, 0, height);
		}

		/// <summary>
		/// Display a drag 'n' drop area with on drop callback
		/// </summary>
		/// <param name="hint"></param>
		/// <param name="onDrop">use this to retrieve dropped object using DragAndDrop.objectReferences from your script</param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public static void DragNDropArea(string hint, Action onDrop, float width = 0, float height = 45)
		{
			Rect rect = GUILayoutUtility.GetRect(width, height, GUILayout.ExpandWidth(true));
			GUI.Box(rect,hint);
			if (rect.Contains(Event.current.mousePosition))
			{
				if (Event.current.type == EventType.DragUpdated)
				{
					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
					Event.current.Use ();
				}   
				else if (Event.current.type == EventType.DragPerform)
				{
					onDrop.Invoke();
					Event.current.Use ();
				}
			}
			GUILayout.Space(5);
		}
		
		#endregion
		
		#region → Draw Enum

		/// <summary>
		/// Draws an enum.
		/// </summary>
		/// <param name="displayedName"></param>
		/// <param name="enum"></param>
		/// <returns></returns>
		public static int DrawEnum(string displayedName, Enum @enum)
		{
			return Convert.ToInt32(EditorGUILayout.EnumPopup(displayedName, @enum, EditorStyles.popup));
		}
	    
		#endregion
		
		#region → Draw Labels

		/// <summary>
		/// Draw an help box with the passed text.
		/// </summary>
		/// <param name="text"></param>
		public static void DrawHelpBox(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				EditorGUILayout.HelpBox(text, MessageType.Info);
			}
		}
		
		/// <summary>
		/// Draw a warning box with the passed text.
		/// </summary>
		/// <param name="text"></param>
		public static void DrawWarningBox(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				EditorGUILayout.HelpBox(text, MessageType.Warning);
			}
		}

		/// <summary>
		/// Draw an error box with the passed text.
		/// </summary>
		/// <param name="text"></param>
		public static void DrawErrorBox(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				EditorGUILayout.HelpBox(text, MessageType.Error);
			}
		}

		/// <summary>
		/// Draw a label with the passed text
		/// </summary>
		/// <param name="text"></param>
		public static void DrawLabel(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				EditorGUILayout.LabelField(new GUIContent(text), EditorStyles.label);
			}
		}

		/// <summary>
		/// Draw a not editable label with a purpose text inside
		///
		/// Property must be a string
		/// </summary>
		/// <param name="property"></param>
		public static void DrawLabel(SerializedProperty property)
		{
			if (property.propertyType != SerializedPropertyType.String)
			{
				return;
			}
			
			DrawNotEditableProperty(property);
		}

		/// <summary>
		/// Draw a label with the passed text
		/// </summary>
		/// <param name="text"></param>
		/// <param name="space"></param>
		public static void DrawHeader(string text, int space = 3)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			
			GUILayout.Space(space);
			EditorGUILayout.LabelField(new GUIContent(text), EditorStyles.boldLabel);
		}

		/// <summary>
		/// Draw an header and then a foldout with the same text.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="foldout"></param>
		/// <param name="space"></param>
		/// <returns></returns>
		public static bool DrawHeaderThenFoldout(string text, bool foldout, int space = 10)
		{
			DrawHeader(text, space);
			return EditorGUILayout.Foldout(foldout, text);
		}
		
		#endregion
		
		#region → Draw Object Field
		
		/// <summary>
		/// It draws the property only if its different from null in a not-editable way.
		/// </summary>
		/// <param name="displayedName"></param>
		/// <param name="target"></param>
		public static void DrawNotEditableObjectField(string displayedName, Object target)
		{	
			bool guiEnabled = GUI.enabled;
			GUI.enabled = false;
			EditorGUILayout.ObjectField(displayedName, target, typeof(Object), true);
			GUI.enabled = guiEnabled;
		}
		
		#endregion
		
		#region → Draw SerializedObject Infos
		
		/// <summary>
        /// It help to draw easily infos of a class in custom editors.
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="localIdentfierInFile"></param>
        public static void DrawSerializedObjectData(SerializedObject serializedObject, int localIdentfierInFile = 0)
        {
            EditorGUILayout.BeginVertical();
	        bool enabled = GUI.enabled;
	        GUI.enabled = false;
            
            DrawLocalIdentifierInFile(serializedObject, localIdentfierInFile);
	        DrawTargetInstanceID(serializedObject);
			
	        DrawScriptReferenceField(serializedObject);
            DrawTargetObjectReferenceField(serializedObject);
            
	        GUI.enabled = enabled;
            EditorGUILayout.EndVertical();
        }

		/// <summary>
		/// Draws the local identfier in file of the serialized object target.
		/// </summary>
		/// <param name="serializedObject"></param>
		/// <param name="identfier"></param>
		public static void DrawLocalIdentifierInFile(SerializedObject serializedObject, int identfier = 0)
		{
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			Object targetObject = serializedObject.targetObject;
			ulong showedIdentfier = 0;
#if UNITY_2019_2_OR_NEWER
			showedIdentfier = (identfier != 0) ? (ulong)identfier : Unsupported.GetLocalIdentifierInFileForPersistentObject(targetObject);
#else
			showedIdentfier = (ulong)((identfier != 0) ? identfier : Unsupported.GetLocalIdentifierInFile(targetObject.GetInstanceID()));
#endif
			EditorGUILayout.LabelField(identifierLabelValue, showedIdentfier.ToString());
			GUI.enabled = enabled;
		}
		
		/// <summary>
		/// Draw the instance id of the serialized object target.
		/// </summary>
		/// <param name="serializedObject"></param>
		public static void DrawTargetInstanceID(SerializedObject serializedObject)
		{
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			int instanceID = serializedObject.targetObject.GetInstanceID();
			EditorGUILayout.LabelField(instanceIdLabelValue, instanceID.ToString());      
			GUI.enabled = enabled;
		}
		
		/// <summary>
		/// Draw a reference to self: it's useful to navigate different window and ping again the same object.
		/// </summary>
		/// <param name="serializedObject"></param>
		public static void DrawTargetObjectReferenceField(SerializedObject serializedObject)
		{	
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			EditorGUILayout.ObjectField(selfLabelValue, serializedObject.targetObject, typeof(Object), true);
			GUI.enabled = enabled;
		}
		
		#endregion
		
		#region → Draw ScriptReference Field
	    
		/// <summary>
		/// Draw a reference to the editor monoscript asset.
		/// </summary>
		/// <param name="customEditor"></param>
		public static void DrawScriptReferenceField(Editor customEditor)
		{
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			SerializedProperty m_Script = new SerializedObject(customEditor).FindProperty(UtilsConstants.ScriptSerializedPropertyName);
			EditorGUILayout.PropertyField(m_Script, new GUIContent(nameof(Editor)), m_Script.isExpanded && m_Script.hasChildren);
			GUI.enabled = enabled;
		}

		/// <summary>
		/// Draw a reference to monoscript asset.
		/// </summary>
		/// <param name="serializedObject"></param>
		public static void DrawScriptReferenceField(SerializedObject serializedObject)
		{
			if (serializedObject == null)
			{
				return;
			}
			
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			SerializedProperty m_Script = serializedObject.FindProperty(UtilsConstants.ScriptSerializedPropertyName);
			if (m_Script != null)
			{
				EditorGUILayout.PropertyField(m_Script, m_Script.isExpanded && m_Script.hasChildren);
			}
			GUI.enabled = enabled;
		}
		
		#endregion
		
		#region → Draw Texture

		/// <summary>
		/// Draws a texture
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="scaleMode"></param>
		public static void DrawTexture(Texture texture, ScaleMode scaleMode = ScaleMode.ScaleToFit)
		{
			if (texture == null)
			{
				return;
			}
			
			Rect rect = GUILayoutUtility.GetRect(texture.width*0.5f, texture.height*0.5f);
			GUI.DrawTexture(rect, texture, scaleMode);
		}
		
		#endregion
		
		#endregion
		
		
		#region EditorGUILayout → Obsolescences
		
		#region → GetPropertyBackingFieldName

		/// <summary>
		/// Retrieve property backing field name;
		/// </summary>
		/// <param name="originalPropertyName">of a property exposed with [field:]</param>
		/// <returns></returns>
		[Obsolete("2021/12/03 → Use SerializedPropertyUtils.GetPropertyBackingFieldName instead.")]
		public static string GetPropertyBackingFieldName(string originalPropertyName)
		{
			return SerializedPropertyUtils.GetPropertyBackingFieldName(originalPropertyName);
		}

		/// <summary>
		/// Retrieve property original name;
		/// </summary>
		/// <param name="backingFieldName">of a property exposed with [field:]</param>
		/// <returns></returns>
		[Obsolete("2021/12/03 → Use SerializedPropertyUtils.GetPropertyNameFromPropertyBackingFieldName instead.")]
		public static string GetPropertyNameFromPropertyBackingFieldName(string backingFieldName)
		{
			return SerializedPropertyUtils.GetPropertyNameFromPropertyBackingFieldName(backingFieldName);
		}
		
		#endregion
	
		#region → Draw Inspector
		
		[Obsolete("2021/08/14 → Use DrawInspector instead. This will be removed in future versions.")]
		public static bool DrawCascadeInspector(SerializedObject serializedObject, DrawSerializedPropertyDelegate drawPropertyDelegate)
		{
			return DrawInspector(serializedObject, drawPropertyDelegate);
		}
		
		#endregion
		
		/// <summary>
		/// Draws an enum. It returns the int value of a property.
		/// </summary>
		/// <param name="displayedName"></param>
		/// <param name="enum"></param>
		/// <returns></returns>
		[Obsolete("2021/08/14 → Use Draw Enum instead. This will be removed in future versions.")]
		public static int DrawEnumField(string displayedName, Enum @enum)
		{
			return DrawEnum(displayedName, @enum);
		}
		
		#endregion
	}
}
#endif
