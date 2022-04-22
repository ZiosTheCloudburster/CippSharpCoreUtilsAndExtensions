#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.EditorUtils
{
	using ArrayUtils = CippSharp.Core.Utils.ArrayUtils;
	using StringUtils = CippSharp.Core.Utils.StringUtils;
	using ReflectionUtils = CippSharp.Core.Utils.ReflectionUtils;
	
    public static partial class SerializedPropertyUtils
    {
        /// <summary>
        /// Property Special Name suffix for BackingField Properties,
        /// marked with [field: ]
        /// </summary>
        public const string k_BackingField = "k__BackingField";
        
        private const string PropertyIsNullError = "Property is null.";
        private const string PropertyIsNotArrayError = "Property isn't an array.";
        
        /// <summary>
        /// Some years ago the gradient value wasn't exposed!
        /// </summary>
        private static readonly PropertyInfo gradientValuePropertyInfo = typeof(SerializedProperty).GetProperty("gradientValue", (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance), null, typeof(Gradient), new Type[0], null);

	    /// <summary>
	    /// A better name for logs
	    /// </summary>
	    private static readonly string LogName = $"[{nameof(SerializedPropertyUtils)}]: ";
	    
        #region SerializedPropertyUtils → Generic

        #region → Get Value
	    
        /// <summary>
		/// Retrieve value from serialized property relative if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="propertyRelative"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetValue<T>(SerializedProperty property, string propertyRelative)
		{
			SerializedProperty foundProperty = property.FindPropertyRelative(propertyRelative);
			return GetValue<T>(foundProperty);
		}

		/// <summary>
		/// Retrieve value from serialized property if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public static T GetValue<T>(SerializedProperty property)
		{
			Type type = typeof(T);

			// First, do special Type checks
			if (type.IsEnum)
				return (T) (object) property.intValue;

			// Next, check for literal UnityEngine struct-types
			// @note: ->object->ValueT double-casts because C# is too dumb to realize that that the ValueT in each situation is the exact type needed.
			// 	e.g. `return thisSP.colorValue` spits _error CS0029: Cannot implicitly convert type `UnityEngine.Color' to `ValueT'_
			// 	and `return (ValueT)thisSP.colorValue;` spits _error CS0030: Cannot convert type `UnityEngine.Color' to `ValueT'_
			if (typeof(Color).IsAssignableFrom(type))
				return (T) (object) property.colorValue;
			else if (typeof(LayerMask).IsAssignableFrom(type))
				return (T) (object) property.intValue;
			else if (typeof(Vector2).IsAssignableFrom(type))
				return (T) (object) property.vector2Value;
			else if (typeof(Vector3).IsAssignableFrom(type))
				return (T) (object) property.vector3Value;
			else if (typeof(Vector4).IsAssignableFrom(type))
				return (T) (object) property.vector4Value;
			else if (typeof(Rect).IsAssignableFrom(type))
				return (T) (object) property.rectValue;
			else if (typeof(AnimationCurve).IsAssignableFrom(type))
				return (T) (object) property.animationCurveValue;
			else if (typeof(Bounds).IsAssignableFrom(type))
				return (T) (object) property.boundsValue;
			else if (typeof(Gradient).IsAssignableFrom(type))
				return (T) (object) SafeGetGradientValue(property);
			else if (typeof(Quaternion).IsAssignableFrom(type))
				return (T) (object) property.quaternionValue;

			// Next, check if derived from UnityEngine.Object base class
			if (typeof(UnityEngine.Object).IsAssignableFrom(type))
				return (T) (object) property.objectReferenceValue;

			// Finally, check for native type-families
			if (typeof(int).IsAssignableFrom(type))
				return (T) (object) property.intValue;
			else if (typeof(bool).IsAssignableFrom(type))
				return (T) (object) property.boolValue;
			else if (typeof(float).IsAssignableFrom(type))
				return (T) (object) property.floatValue;
			else if (typeof(string).IsAssignableFrom(type))
				return (T) (object) property.stringValue;
			else if (typeof(char).IsAssignableFrom(type))
				return (T) (object) property.intValue;
			
			// And if all fails, throw an exception.
			throw new NotImplementedException("Unimplemented propertyType " + property.propertyType + ".");
		}
        
	    /// <summary>
	    /// Access to SerializedProperty's internal gradientValue property getter,
	    /// in a manner that'll only soft break (returning null) if the property changes
	    /// or disappears in future Unity revs.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <returns></returns>
	    private static Gradient SafeGetGradientValue(SerializedProperty property)
	    {
		    if (gradientValuePropertyInfo == null)
		    {
			    Debug.LogError(LogName+$"{nameof(SafeGetGradientValue)} Gradient Property Info is null!");
			    return null;
		    }
		
		    Gradient gradientValue = gradientValuePropertyInfo.GetValue(property, null) as Gradient;
		    return gradientValue;
	    }
	    
        #endregion

	    #region → Get Value List
	    
	    /// <summary>
	    /// Retrieve an array of 'T' from serialized property array.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <typeparam name="T"></typeparam>
	    /// <returns></returns>
	    public static List<T> GetValueList<T>(SerializedProperty property)
	    {
		    if (property == null)
		    {
			    Debug.LogError(LogName+ $"{nameof(GetValueList)} {PropertyIsNullError}.");
			    return null;
		    }
			
		    if (!property.isArray)
		    {
			    Debug.LogError(LogName+ $"{nameof(GetValueList)} {PropertyIsNotArrayError}.");
			    return null;
		    }
			
		    List<T> objects = new List<T>();
		    for (int i = 0; i < property.arraySize; i++)
		    {
			    SerializedProperty element = property.GetArrayElementAtIndex(i);
			    try
			    {
				    objects.Add(GetValue<T>(element));
			    }
			    catch (Exception e)
			    {
				    Debug.LogError(LogName+$"{nameof(GetValueList)} property at {i.ToString()} failed to retrieve {typeof(T).Name} value." +
				                   $"\nCaught exception: {e.Message}.");
			    }
		    }
		    
		    return objects;
	    }
        
	    #endregion

	    #region → Set Value
 
        /// <summary>
		/// Set value to serialized property relative if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="propertyRelative"></param>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static void SetValue<T>(SerializedProperty property, string propertyRelative, T value)
		{
			SerializedProperty foundProperty = property.FindPropertyRelative(propertyRelative);
			SetValue(foundProperty, value);
		}
		
		/// <summary>
		/// Set value to serialized property if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		/// <exception cref="NotImplementedException"></exception>
		public static void SetValue<T>(SerializedProperty property, T value)
		{
			Type type = typeof(T);

			// First, do special Type checks
			if (type.IsEnum)
			{
				property.intValue = (int)(object)value;
				return;
			}

			// Next, check for literal UnityEngine struct-types
			// @note: ->object->ValueT double-casts because C# is too dumb to realize that that the ValueT in each situation is the exact type needed.
			// 	e.g. `return thisSP.colorValue` spits _error CS0029: Cannot implicitly convert type `UnityEngine.Color' to `ValueT'_
			// 	and `return (ValueT)thisSP.colorValue;` spits _error CS0030: Cannot convert type `UnityEngine.Color' to `ValueT'_
			if (typeof(Color).IsAssignableFrom(type))
			{
				property.colorValue = (Color)(object)value;
				return;
			}
			else if (typeof(LayerMask).IsAssignableFrom(type))
			{
				property.intValue = (int)(object)value;
				return;
			}
			else if (typeof(Vector2).IsAssignableFrom(type))
			{
				property.vector2Value = (Vector2)(object)value;
				return;
			}
			else if (typeof(Vector3).IsAssignableFrom(type))
			{
				property.vector3Value = (Vector3)(object)value;
				return;
			}
			else if (typeof(Vector4).IsAssignableFrom(type))
			{
				property.vector4Value = (Vector4)(object)value;
				return;
			}
			else if (typeof(Rect).IsAssignableFrom(type))
			{
				property.rectValue = (Rect)(object)value;
				return;
			}
			else if (typeof(AnimationCurve).IsAssignableFrom(type))
			{
				property.animationCurveValue = (AnimationCurve)(object)value;
				return;
			}
			else if (typeof(Bounds).IsAssignableFrom(type))
			{
				property.boundsValue = (Bounds)(object)value;
				return;
			}
			else if (typeof(Gradient).IsAssignableFrom(type))
			{
				SafeSetGradientValue(property, (Gradient)(object)value);
				return;
			}
			else if (typeof(Quaternion).IsAssignableFrom(type))
			{
				property.quaternionValue = (Quaternion)(object)value;
				return;
			}

			// Next, check if derived from UnityEngine.Object base class
			if (typeof(UnityEngine.Object).IsAssignableFrom(type))
			{
				property.objectReferenceValue = (UnityEngine.Object)(object)value;
				return;
			}

			// Finally, check for native type-families
			if (typeof(int).IsAssignableFrom(type))
			{
				property.intValue = (int)(object)(value);
				return;
			}
			else if (typeof(bool).IsAssignableFrom(type))
			{
				property.boolValue = (bool)(object)value;
				return;
			}
			else if (typeof(float).IsAssignableFrom(type))
			{
				property.floatValue = (float)(object)value;
				return;
			}
			else if (typeof(string).IsAssignableFrom(type))
			{
				property.stringValue = (string)(object)value;
				return;
			}
			else if (typeof(char).IsAssignableFrom(type))
			{
				property.intValue = (int)(object)(value);
				return;
			}
			
			// And if all fails, throw an exception.
			throw new NotImplementedException("Unimplemented propertyType " + property.propertyType + ".");
		}

	    /// <summary>
	    /// Access to SerializedProperty's internal gradientValue property getter,
	    /// in a manner that'll only soft break (returning null) if the property changes
	    /// or disappears in future Unity revs.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <param name="newGradient"></param>
	    /// <returns></returns>
	    private static void SafeSetGradientValue(SerializedProperty property, Gradient newGradient)
	    {
		    if (gradientValuePropertyInfo == null)
		    {
			    Debug.LogError(LogName+$"{nameof(SafeSetGradientValue)} Gradient Property Info is null!");
			    return;
		    }
		
		    gradientValuePropertyInfo.SetValue(property, newGradient, null);
	    }

	    #endregion
	    
	    #region → Set Value List
	    
	    /// <summary>
	    /// Overwrite array of T values.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <param name="objects"></param>
	    /// <typeparam name="T"></typeparam>
	    public static void SetValues<T>(SerializedProperty property, ICollection<T> objects)
	    {
		    if (property == null)
		    {
			    Debug.LogError(LogName+ $"{nameof(SetValues)} {PropertyIsNullError}.");
			    return;
		    }
			
		    if (!property.isArray)
		    {
			    Debug.LogError(LogName+ $"{nameof(SetValues)} {PropertyIsNotArrayError}.");
			    return;
		    }

		    int propertyArraySize = (property.arraySize < 0 ? 0 : property.arraySize);
		    int count = objects.Count;
		    int delta = count - propertyArraySize;
			
		    //Increase or decrease elements for the array property.
		    if (delta > 0)
		    {
			    for (int i = 0; i < Mathf.Abs(delta); i++)
			    {
				    property.InsertArrayElementAtIndex(0);
			    }
		    }
		    else if (delta < 0)
		    {
			    for (int i = 0; i < Mathf.Abs(delta); i++)
			    {
				    property.DeleteArrayElementAtIndex(0);
			    }
		    }
			
		    //Property array size and objects length must match.
		    property.arraySize = count;
		    IList<T> list = objects is IList<T> l ? l : objects.ToArray();
		    for (int i = 0; i < count; i++)
		    {
			    SerializedProperty element = property.GetArrayElementAtIndex(i);
			    try
			    {
				    SetValue(element, list[i]);
			    }
			    catch (Exception e)
			    {
				    Debug.LogError(LogName+$"{nameof(GetValueList)} property at {i.ToString()} failed to set {typeof(T).Name} value." +
				                   $"\nCaught exception: {e.Message}.");
			    }
		    }
	    }
	    
	    #endregion
	    
        #endregion
	    
	    #region SerializedPropertyUtils → Cast

	    /// <summary>
	    /// Retrieve a serialized property that is an array as an array of serialized properties.
	    ///
	    /// USAGE: use this on properties that are arrays in inspector.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <returns></returns>
	    public static List<SerializedProperty> ToList(SerializedProperty property)
	    {
		    if (property == null)
		    {
			    Debug.LogError(LogName+ $"{nameof(ToList)} {PropertyIsNullError}.");
			    return null;
		    }
			
		    if (!property.isArray)
		    {
			    Debug.LogError(LogName+ $"{nameof(ToList)} {PropertyIsNotArrayError}.");
			    return null;
		    }

		    List<SerializedProperty> elements = new List<SerializedProperty>();
		    for (int i = 0; i < property.arraySize; i++)
		    {
			    elements.Add(property.GetArrayElementAtIndex(i));
		    }

		    return elements;
	    }

	    /// <summary>
	    /// Retrieve a serialized property that is an array as an array of serialized properties.
	    ///
	    /// USAGE: use this on properties that are arrays in inspector.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <returns></returns>
	    public static SerializedProperty[] ToArray(SerializedProperty property)
	    {
		    return ToList(property).ToArray();
	    }
	    
	    #endregion
	    
	    #region SerializedPropertyUtils → Iterators
	    
	    /// <summary>
	    /// Invokes a callback during a for iteration of a serialized property array.
	    /// </summary>
	    /// <param name="array"></param>
	    /// <param name="callback"></param>
	    public static void For(SerializedProperty[] array, ForSerializedPropertyAction callback)
	    {
		    if (ArrayUtils.IsNullOrEmpty(array))
		    {
			    return;
		    }
		    
		    for (int i = 0; i < array.Length; i++)
		    {
			    callback.Invoke(array[i], i);
		    }
	    }

	    /// <summary>
	    /// Invokes a callback during a for iteration of a serialized property.
	    /// </summary>
	    /// <param name="arrayProperty"></param>
	    /// <param name="callback"></param>
	    public static void For(SerializedProperty arrayProperty, ForSerializedPropertyAction callback)
	    {
		    if (arrayProperty == null)
		    {
			    Debug.LogError(LogName+ $"{nameof(For)} {PropertyIsNullError}.");
			    return;
		    }
		    
		    if (!arrayProperty.isArray)
		    {
			    Debug.LogError(LogName+ $"{nameof(GetValueList)} {PropertyIsNotArrayError}.");
			    return;
		    }

		    for (int i = 0; i < arrayProperty.arraySize; i++)
		    {
			    callback.Invoke(arrayProperty.GetArrayElementAtIndex(i), i);
		    }
	    }

	    /// <summary>
	    /// Similar to <see cref="EditorGUILayoutUtils.DrawInspector"/>, but only to iterate properties
	    /// </summary>
	    /// <param name="serializedObject"></param>
	    /// <param name="elementDelegate"></param>
	    public static void IterateAllProperties(SerializedObject serializedObject, DrawSerializedPropertyDelegate elementDelegate)
	    {
		    if (serializedObject == null)
		    {
			    Debug.LogError(LogName+ $"{nameof(IterateAllProperties)} passed {nameof(serializedObject)} is null!");
			    return;
		    }
		    
		    SerializedProperty iterator = serializedObject.GetIterator();
		    for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
		    {
			    using (new EditorGUI.DisabledScope(UtilsConstants.ScriptSerializedPropertyName == iterator.propertyPath))
			    {
				    elementDelegate.Invoke(iterator.Copy());
			    }
		    }
	    }

	    #endregion
	    
	    #region SerializedPropertyUtils → Methods

	    #region → Brother(s)
	    
	    /// <summary>
	    /// Has this property the target brother?
	    /// </summary>
	    /// <param name="property"></param>
	    /// <param name="brotherPropertyName"></param>
	    /// <returns></returns>
	    public static bool HasBrotherProperty(SerializedProperty property, string brotherPropertyName)
	    {
		    return HasBrotherProperty(property, brotherPropertyName, out _);
	    }

	    /// <summary>
	    /// Has this property the target brother?
	    /// Plus gives the brother property.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <param name="brotherPropertyName"></param>
	    /// <param name="brotherProperty"></param>
	    /// <returns></returns>
	    public static bool HasBrotherProperty(SerializedProperty property, string brotherPropertyName, out SerializedProperty brotherProperty)
	    {
		    try
		    {
			    string propertyPath = property.propertyPath;
			    SerializedObject serializedObject = property.serializedObject;
			    propertyPath = StringUtils.ReplaceLastOccurrence(propertyPath, property.name, brotherPropertyName);
			    brotherProperty = serializedObject.FindProperty(propertyPath);
			    return brotherProperty != null;
		    }
		    catch
		    {
			    brotherProperty = null;
			    return false;
		    }
	    }

	    /// <summary>
	    /// Try to find a brother property of the interested one.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <param name="brotherPropertyName"></param>
	    /// <returns></returns>
	    public static SerializedProperty FindBrotherProperty(SerializedProperty property, string brotherPropertyName)
	    {
		    try
		    {
			    string propertyPath = property.propertyPath;
			    SerializedObject serializedObject = property.serializedObject;
			    propertyPath = StringUtils.ReplaceLastOccurrence(propertyPath, property.name, brotherPropertyName);
			    return serializedObject.FindProperty(propertyPath);
		    }
		    catch (Exception e)
		    {
			    Debug.LogError(LogName+$"{nameof(FindBrotherProperty)} failed. Caught exception: {e.Message}.");
			    return null;
		    }
	    }
	    
	    #endregion
	   
	    #region → Get Property BackingFieldName

	    /// <summary>
	    /// Retrieve property backing field name;
	    /// </summary>
	    /// <param name="originalPropertyName">of a property exposed with [field:]</param>
	    /// <returns></returns>
	    public static string GetPropertyBackingFieldName(string originalPropertyName)
	    {
		    return $"<{originalPropertyName}>{k_BackingField}";
	    }

	    /// <summary>
	    /// Retrieve property original name;
	    /// </summary>
	    /// <param name="backingFieldName">of a property exposed with [field:]</param>
	    /// <returns></returns>
	    public static string GetPropertyNameFromPropertyBackingFieldName(string backingFieldName)
	    {
		    return backingFieldName.TrimStart(new[] {'<'}).Replace(k_BackingField, string.Empty).TrimEnd(new[] {'>'});
	    }
		
	    #endregion
	    
	    #region → Get Properties
	    
	    /// <summary>
	    /// It retrieves all serialized properties from <param name="serializedObject"></param> iterator.
	    /// </summary>
	    /// <param name="serializedObject"></param>
	    /// <returns></returns>
	    public static List<SerializedProperty> GetAllProperties(SerializedObject serializedObject)
	    {
		    return GetAllProperties(serializedObject, p => true);
	    }

	    /// <summary>
	    /// It retrieves all serialized properties from <param name="serializedObject"></param> iterator that match predicate
	    /// </summary>
	    /// <param name="serializedObject"></param>
	    /// <param name="predicate"></param>
	    /// <returns></returns>
	    public static List<SerializedProperty> GetAllProperties(SerializedObject serializedObject, Predicate<SerializedProperty> predicate)
	    {
		    if (serializedObject == null)
		    {
			    Debug.LogError(LogName+ $"{nameof(GetAllProperties)} passed {nameof(serializedObject)} is null!");
			    return null;
		    }
		    
		    List<SerializedProperty> properties = new List<SerializedProperty>();
		    SerializedProperty iterator = serializedObject.GetIterator();
		    for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
		    {
			    using (new EditorGUI.DisabledScope(UtilsConstants.ScriptSerializedPropertyName == iterator.propertyPath))
			    {
				    SerializedProperty copy = iterator.Copy();
				    if (predicate.Invoke(copy))
				    {
					    properties.Add(copy);
				    }
			    }
		    }
		    return properties;
	    }

	    /// <summary>
	    /// Select T from properties in <param name="serializedObject"></param> iterator that match predicate
	    /// </summary>
	    /// <param name="serializedObject"></param>
	    /// <param name="predicate"></param>
	    /// <param name="selector"></param>
	    /// <returns></returns>
	    public static List<T> SelectFromAllProperties<T>(SerializedObject serializedObject, Predicate<SerializedProperty> predicate, Func<SerializedProperty, T> selector)
	    {
		    if (serializedObject == null)
		    {
			    Debug.LogError(LogName+ $"{nameof(GetAllProperties)} passed {nameof(serializedObject)} is null!");
			    return null;
		    }
		    
		    List<T> selection = new List<T>();
		    SerializedProperty iterator = serializedObject.GetIterator();
		    for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
		    {
			    using (new EditorGUI.DisabledScope(UtilsConstants.ScriptSerializedPropertyName == iterator.propertyPath))
			    {
				    SerializedProperty copy = iterator.Copy();
				    if (predicate.Invoke(copy))
				    {
					    selection.Add(selector.Invoke(copy));
				    }
			    }
		    }
		    return selection;
	    }

	    #endregion
	    
	    #endregion
	    
	    
	    #region SerializedPropertyUtils → Reflection
	    
	    #region → Get Property ParentLevel (read)
        
        /// <summary>
        /// Retrieve last parent of Serialized Property as object.
        ///
        /// Use this for 'read-only' behaviour.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static bool TryGetParentLevel(SerializedProperty property, out object parent)
        {
            if (TryGetParentsLevels(property, out object[] parents))
            {
                parent = parents.Last();
                return true;
            }
            else
            {
                parent = null;
                return false;
            }
        }
        
        /// <summary>
        /// Retrieve a list of all parents Serialized Property as sorted objects array.
        ///
        /// Use this for 'read-only' behaviour.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="parents"></param>
        /// <returns></returns>
        public static bool TryGetParentsLevels(SerializedProperty property, out object[] parents)
        {
            if (property == null)
            {
                parents = null;
                return false;
            }

            string name = property.name;
            string path = property.propertyPath;
            string revisedPath = path.Replace(name, string.Empty);
            
            try
            {
                bool value = true;
                List<object> tmpParents = new List<object>();
                
                value = GetParentsLevelsInternal(property, ref tmpParents);

                parents = tmpParents.ToArray();
                return value;
            }
            catch (Exception e)
            {
                Debug.LogError(LogName+$"{nameof(TryGetParentsLevels)} failed retrieve parents at: {revisedPath}. Caught exception: {e.Message}.");
                parents = null;
                return false;
            }
        }
        
	    /// <summary>
	    /// Retrieve a list of all parents Serialized Property as sorted objects array.
	    ///
	    /// Use this for 'read-only' behaviour.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <param name="parents"></param>
	    /// <param name="debug"></param>
	    /// <returns></returns>
        private static bool GetParentsLevelsInternal(SerializedProperty property, ref List<object> parents, bool debug = false)
        {
            if (parents == null)
            {
                return false;
            }
            
            string name = property.name;
            string path = property.propertyPath;
            string revisedPath = path.Replace(name, string.Empty);
       
            bool value = true;
                
            Object targetObject = property.serializedObject.targetObject;
            object contextObject = targetObject;
                
            string[] splitResults = revisedPath.Split(new []{'.'});
            if (debug)
            {
                Debug.Log(LogName + $"{nameof(GetParentsLevelsInternal)} length of split results is: {splitResults.Length.ToString()}.");
            }

            int i = 0;

            #region Get Parents Levels 

            while (i < splitResults.Length)
            {
                string fieldName = splitResults[i];
                if (string.IsNullOrEmpty(fieldName))
                {
                    if (debug)
                    {
                        Debug.Log(LogName+ $"{nameof(GetParentsLevelsInternal)} {i.ToString()} {nameof(fieldName)} is null or empty.");
                    }
                }
                else if (fieldName == UtilsConstants.Array && i + 1 < splitResults.Length && splitResults[i + 1].Contains("data"))
                {
                    #region Array Element Property
                        
                    string data = splitResults[i + 1];
                    if (int.TryParse(data.Replace("data[", string.Empty).Replace("]", string.Empty), out int w))
                    {
                        if (ArrayUtils.IsArray(contextObject) && ArrayUtils.TryToObjectArray(contextObject, out object[] array))
                        {
                            if (ArrayUtils.TryGetValue(array, w, out object element))
                            {
                                object previousContextObject = contextObject;
                                parents.Add(previousContextObject);
                                contextObject = element;

                                i += 2;
                                continue;
                            }
                            else
                            {
                                value = false;
                                break;
                            }
                        }
                        else
                        {
                            value = false;
                            break;
                        }
                    }
                    else
                    {
                        value = false;
                        break;
                    }
                        
                    #endregion
                }
                else if (ReflectionUtils.HasField(contextObject, fieldName, out FieldInfo field))
                {
                    if (debug)
                    {
                        Debug.Log(LogName+$"{nameof(GetParentsLevelsInternal)} {i.ToString()} → {fieldName}");
                    }

                    object previousContextObject = contextObject;
                    parents.Add(previousContextObject);
                    contextObject = field.GetValue(previousContextObject);
                }
                else
                {
                    value = false;
                    break;
                }
                    
                i++;
            }
                
            #endregion
                
            parents.Add(contextObject);
            if (debug)
            {
                Debug.Log(LogName+$"{nameof(GetParentsLevelsInternal)} Parents Length {parents.Count.ToString()} / Split Results Length {splitResults.Length.ToString()}");
            }

            return value;
        }
        
        #endregion
	    
	    #region → Edit ParentLevel (read/callback/write)

        /// <summary>
        /// References the last parent of a property and regardless of method called it folds back 'exposed' values by reflection.
        /// Use this for get-set behaviour of inspector nest structure.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static bool TryEditLastParentLevel(SerializedProperty property, GenericRefAction callback)
        {
	        return TryEditLastParentLevelInternal(property, callback);
        }

	    private static bool TryEditLastParentLevelInternal(SerializedProperty property, GenericRefAction callback, bool debug = false)
	    {
			if (property == null)
            {
                return false;
            }

            string name = property.name;
            string path = property.propertyPath;
            string revisedPath = path.Replace(name, string.Empty);
            
            try
            {
                bool value = true;
                List<object> tmpParents = new List<object>();
                
                Object targetObject = property.serializedObject.targetObject;
                object contextObject = targetObject;
                
                string[] splitResults = revisedPath.Split(new []{'.'}, StringSplitOptions.RemoveEmptyEntries);
                List<string> aggregatedSplitResults = new List<string>();
                if (debug)
                {
                    Debug.Log(LogName+ $"{nameof(TryEditLastParentLevelInternal)} Length {splitResults.Length.ToString()}");
                }

                #region Unfold Parents
                
                int i = 0;
                
                while (i < splitResults.Length)
                {
                    string fieldName = splitResults[i];
                    if (string.IsNullOrEmpty(fieldName))
                    {
                        if (debug)
                        {
                            Debug.Log(LogName+ $"{nameof(TryEditLastParentLevelInternal)} {i.ToString()} --> {fieldName}");
                        }

                        //Do Nothing
                    }
                    else if (fieldName == UtilsConstants.Array && i + 1 < splitResults.Length && splitResults[i + 1].Contains("data"))
                    {
                        #region Array Element Property
                        
                        string data = splitResults[i + 1];
                        if (int.TryParse(data.Replace("data[", string.Empty).Replace("]", string.Empty), out int w))
                        {
                            if (ArrayUtils.IsArray(contextObject) && ArrayUtils.TryToObjectArray(contextObject, out object[] array))
                            {
                                if (ArrayUtils.TryGetValue(array, w, out object element))
                                {
                                    object previousContextObject = ((object)(Array)array);
                                    tmpParents.Add(previousContextObject);
                                    contextObject = element;

                                    if (debug)
                                    {
                                        Debug.Log(LogName+ $"{nameof(TryEditLastParentLevelInternal)} {i.ToString()} --> {fieldName}." +
                                                  $"\n{(i + 1).ToString()} --> {data}");
                                    }

                                    aggregatedSplitResults.Add(fieldName + "." + data);

                                    i += 2;
                                    continue;
                                }
                                else
                                {
                                    value = false;
                                    break;
                                }
                            }
                            else
                            {
                                value = false;
                                break;
                            }
                        }
                        else
                        {
                            value = false;
                            break;
                        }
                        
                        #endregion
                    }
                    else if (ReflectionUtils.HasField(contextObject, fieldName, out FieldInfo field))
                    {
                        if (debug)
                        {
                            Debug.Log(LogName+ $"{nameof(TryEditLastParentLevelInternal)} {i.ToString()} --> {fieldName}");
                        }

                        aggregatedSplitResults.Add(fieldName);
                        var previousContextObject = contextObject;
                        tmpParents.Add(previousContextObject);
                        contextObject = field.GetValue(previousContextObject);

                    }
                    else
                    {
                        value = false;
                        break;
                    }
                    
                    i++;
                }
                
                #endregion
                
                callback?.Invoke(ref contextObject);
                tmpParents.Add(contextObject);
	            
                if (debug)
                {
                    Debug.Log(LogName+ $"{nameof(TryEditLastParentLevelInternal)} Parents Length {tmpParents.Count.ToString()} / Split Results Length {splitResults.Length.ToString()} / Aggregated Split Results Length {aggregatedSplitResults.Count.ToString()}");
                }

                splitResults = aggregatedSplitResults.ToArray();
                i = splitResults.Length - 1;
                object previousContext = (i >= 0) ? tmpParents[i] : targetObject;
	            
                #region Fold Parents

	            if (i >= 0)
	            {
		            while (i >= 0)
		            {
			            string fieldName = splitResults[i];
			            if (string.IsNullOrEmpty(fieldName))
			            {
				            if (debug)
				            {
					            Debug.Log(LogName+ $"{nameof(TryEditLastParentLevelInternal)} Reverse loop {i.ToString()} --> {fieldName}");
				            }
			            }
			            else if (fieldName.Contains(UtilsConstants.Array) && fieldName.Contains("data"))
			            {
				            #region Array Element Property

				            string data = fieldName;
				            string parsingString = data.Replace(UtilsConstants.Array, string.Empty)
					            .Replace(".", string.Empty).Replace("data[", string.Empty).Replace("]", string.Empty);
				            if (int.TryParse(parsingString, out int w))
				            {
					            if (ArrayUtils.IsArray(previousContext) &&
					                ArrayUtils.TryToObjectArray(previousContext, out object[] array))
					            {
						            object element = tmpParents[i + 1];
						            if (ArrayUtils.TrySetValue(array, w, element))
						            {
							            Array destinationArray = Array.CreateInstance(element.GetType(), array.Length);
							            Array.Copy(array, destinationArray, array.Length);
							            tmpParents[i] = destinationArray;
							            if (debug)
							            {
								            Debug.Log(LogName+ $"{nameof(TryEditLastParentLevelInternal)} Reverse loop {i.ToString()} --> {fieldName}");
							            }
						            }
						            else
						            {
							            value = false;
							            break;
						            }
					            }
					            else
					            {
						            value = false;
						            break;
					            }
				            }
				            else
				            {
					            value = false;
					            break;
				            }

				            #endregion
			            }
			            else if (ReflectionUtils.HasField(previousContext, fieldName, out FieldInfo field))
			            {
				            if (debug)
				            {
					            Debug.Log(LogName+ $"{nameof(TryEditLastParentLevelInternal)} Reverse loop {i.ToString()} --> {fieldName}");
				            }

				            field.SetValue(previousContext, tmpParents[i + 1]);
			            }

			            i--;
			            if (i < 0)
			            {
				            break;
			            }

			            previousContext = tmpParents[i];
		            }
	            }
	            else
	            {
		            value = true;
		            
		            if (debug)
		            {
			            Debug.Log(LogName+ $"{nameof(TryEditLastParentLevelInternal)} {i.ToString()} is less than 0");
		            }
	            }

	            #endregion

                return value;
            }
            catch (Exception e)
            {
                Debug.LogError(LogName+ $"{nameof(TryEditLastParentLevelInternal)} failed. Caught exception: {e.Message}");
                return false;
            }
	    }

	    #endregion
	    
	    #endregion
    }
}
#endif

