//#if UNITY_EDITOR
//using System.Linq;
//using UnityEditor;
//using UnityEngine;
//
//namespace CippSharp.Core.EditorUtils
//{
//    public static partial class EditorGUIUtils
//    {
//        #region Draw Inspector
//
////        /// <summary>
////        /// Draws an inspector of a serialized object where you specify how properties are drawn.
////        /// </summary>
////        /// <param name="sharedRect">the edited rect for the whole iteration</param>
////        /// <param name="serializedObject"></param>
////        /// <param name="drawPropertyDelegate"></param>
////        /// <returns>has any changed?</returns>
////        public static bool DrawInspector(ref Rect sharedRect, SerializedObject serializedObject, DrawSerializedPropertyDelegate1 drawPropertyDelegate)
////        {
////            EditorGUI.BeginChangeCheck();
////            serializedObject.UpdateIfRequiredOrScript();
////            SerializedProperty[] allProperties = SerializedPropertyUtils.GetAllProperties(serializedObject);
////            foreach (var property in allProperties)
////            {
////                drawPropertyDelegate.Invoke(ref sharedRect, property);
////            }
////            
////            serializedObject.ApplyModifiedProperties();
////            return EditorGUI.EndChangeCheck();
////        }
//
//      
//        
//        #endregion
//    }
//}
//#endif