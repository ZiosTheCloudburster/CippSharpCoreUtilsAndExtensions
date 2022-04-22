//#if UNITY_EDITOR
//using System;
//using System.Collections;
//using UnityEditor;
//using UnityEngine;
//
//namespace CippSharp.Core.EditorUtils
//{
//    public static partial class EditorGUIUtils
//    {
////        #region Draw Property
////        
////
////        
////        #endregion
//        
//        #region Draw Not Editable Property
//
//        
//        #endregion
//
//        #region Draw Default Property
//
////        public static bool DrawPropertyFieldInternal(Rect position, SerializedProperty property, GUIContent label)
////        {
////
////        }
//
////        /// <summary>
////        /// Draw single line default property field (yes single line even if it has children)
////        /// </summary>
////        /// <param name="position"></param>
////        /// <param name="property"></param>
////        /// <param name="label"></param>
////        /// <returns></returns>
////        public static bool DrawDefaultSingleLinePropertyField(Rect position, SerializedProperty property, GUIContent label)
////        {
////            return (bool)DefaultPropertyFieldMethodInfo.Invoke(null, new object[] {position, property, label});
////        }
////
////        
////        /// <summary>
////        /// Draw single line not editable default property field (yes single line even if it has children)
////        /// </summary>
////        /// <param name="position"></param>
////        /// <param name="property"></param>
////        /// <param name="label"></param>
////        /// <returns></returns>
////        public static bool DrawNotEditableDefaultSingleLinePropertyField(Rect position, SerializedProperty property, GUIContent label)
////        {
////            bool enabled =  GUI.enabled; 
////            GUI.enabled = false;
////            
////            bool b = (bool) DefaultPropertyFieldMethodInfo.Invoke(null, new object[] {position, property, label});
////            
////            GUI.enabled = enabled;
////            
////            return b;
////        }
//
//        #endregion
//
////        #region Get Property Height
////
////
////
////        #endregion
//        
////        #region Draw Property (with delegates / iterators)
////
////
////        
////
////
////        #endregion
////        
//        #region Get Property Height (with delegates / iterators)
//        
//
//        
//        #endregion
//    }
//}
//#endif
