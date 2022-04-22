//
// Author: Alessandro Salani (Cippo)
//

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.EditorExtensions
{
	using SerializedPropertyUtils = CippSharp.Core.EditorUtils.SerializedPropertyUtils;
	
    public static class SerializedPropertyExtensions
    {
	    #region SerializedPropertyUtils → Methods
	    
	    #region → Brother(s)

	    /// <summary>
	    /// Has this property the target brother?
	    /// </summary>
	    /// <param name="property"></param>
	    /// <param name="brotherPropertyName"></param>
	    /// <returns></returns>
	    public static bool HasBrotherProperty(this SerializedProperty property, string brotherPropertyName)
	    {
		    return SerializedPropertyUtils.HasBrotherProperty(property, brotherPropertyName);
	    }

	    /// <summary>
	    /// Has this property the target brother?
	    /// Plus gives the brother property.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <param name="brotherPropertyName"></param>
	    /// <param name="brotherProperty"></param>
	    /// <returns></returns>
	    public static bool HasBrotherProperty(this SerializedProperty property, string brotherPropertyName, out SerializedProperty brotherProperty)
	    {
		    return SerializedPropertyUtils.HasBrotherProperty(property, brotherPropertyName, out brotherProperty);
	    }

	    /// <summary>
	    /// Retrieve a brother property of the interested one.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <param name="brotherPropertyName"></param>
	    /// <returns></returns>
	    public static SerializedProperty FindBrotherProperty(this SerializedProperty property, string brotherPropertyName)
	    {
		    return SerializedPropertyUtils.FindBrotherProperty(property, brotherPropertyName);
	    }
	    
	    #endregion
	    
	    #region Has Attribute

	    /// <summary>
	    /// Has attribute?
	    /// </summary>
	    /// <param name="property"></param>
	    /// <typeparam name="T"></typeparam>
	    /// <returns></returns>
	    public static bool HasAttribute<T>(this SerializedProperty property) where T : PropertyAttribute
	    {
		    return SerializedPropertyUtils.HasAttribute<T>(property);
	    }

	    /// <summary>
	    /// Has attribute?
	    /// </summary>
	    /// <param name="property"></param>
	    /// <param name="attributePredicate"></param>
	    /// <typeparam name="T"></typeparam>
	    /// <returns></returns>
	    public static bool HasAttribute<T>(this SerializedProperty property, Predicate<T> attributePredicate) where T : PropertyAttribute
	    {
		    return SerializedPropertyUtils.HasAttribute(property, attributePredicate);
	    }

	    #endregion
	    
	    #region → Get Properties
	    
	    //See SerializedObjectExtensions
	    
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
	    public static List<SerializedProperty> ToList(this SerializedProperty property)
	    {
		    return SerializedPropertyUtils.ToList(property);
	    }
	    
	    /// <summary>
	    /// Retrieve a serialized property that is an array as an array of serialized properties.
	    ///
	    /// USAGE: use this on properties that are arrays in inspector.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <returns></returns>
	    public static SerializedProperty[] ToArray(this SerializedProperty property)
	    {
		    return SerializedPropertyUtils.ToArray(property);
	    }

	    #endregion
	    
	    #region SerializedPropertyUtils → Generic
   
	    /// <summary>
	    /// Retrieve an array from serialized property array.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <typeparam name="T"></typeparam>
	    /// <returns></returns>
	    public static List<T> ToValueList<T>(this SerializedProperty property)
	    {
		    return SerializedPropertyUtils.GetValueList<T>(property);
	    }
		
		/// <summary>
		/// Retrieve value from serialized property relative if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="propertyRelative"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetValue<T>(this SerializedProperty property, string propertyRelative)
		{
			return SerializedPropertyUtils.GetValue<T>(property, propertyRelative);
		}

		/// <summary>
		/// Retrieve value from serialized property if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetValue<T>(this SerializedProperty property)
		{
			return SerializedPropertyUtils.GetValue<T>(property);
		}
	
		/// <summary>
		/// Overwrite array element values.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="objects"></param>
		/// <typeparam name="T"></typeparam>
		public static void SetValues<T>(this SerializedProperty property, ICollection<T> objects)
		{
			SerializedPropertyUtils.SetValues(property, objects);
		}
		
		/// <summary>
		/// Set value to serialized property relative if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="propertyRelative"></param>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static void SetValue<T>(this SerializedProperty property, string propertyRelative, T value)
		{
			SerializedPropertyUtils.SetValue(property, propertyRelative, value);
		}
		
		/// <summary>
		/// Set value to serialized property if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		public static void SetValue<T>(this SerializedProperty property, T value)
		{
			SerializedPropertyUtils.SetValue<T>(property, value);
		}

		#endregion
    }
}
#endif
