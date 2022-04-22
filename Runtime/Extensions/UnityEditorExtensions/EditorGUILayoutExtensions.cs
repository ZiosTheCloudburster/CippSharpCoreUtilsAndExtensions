#if UNITY_EDITOR
using UnityEditor;
using Object = UnityEngine.Object;

namespace CippSharp.Core.EditorExtensions
{
    using EditorGUILayoutUtils = CippSharp.Core.EditorUtils.EditorGUILayoutUtils;
    using DrawSerializedPropertyDelegate = CippSharp.Core.EditorUtils.DrawSerializedPropertyDelegate;
    
    
    public static class EditorGUILayoutExtensions
    {
        #region EditorGUILayout → GetLocalIdentfierInFile
        
        /// <summary>
        /// This works only in a custom editor OnEnable().
        /// It retrieves the m_LocalIdentfierInFile of an Object.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int GetLocalIdentfierInFile(this Object target)
        {
            return EditorGUILayoutUtils.GetLocalIdentfierInFile(target);
        }

        #endregion
        
//        #region EditorGUILayout → Inspector & Property Drawers
//
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="serializedObject"></param>
//        /// <param name="drawPropertyDelegate"></param>
//        /// <returns></returns>
//        
//        public static bool DrawInspector(this SerializedObject serializedObject, DrawSerializedPropertyDelegate drawPropertyDelegate)
//        {
//            return EditorGUILayoutUtils.DrawInspector(serializedObject, drawPropertyDelegate);
//        }
//
//        #endregion
        
        #region EditorGUILayout → Other Drawers
        
        #region EditorGUILayout → Draw SerializedObject Infos

        /// <summary>
        /// Draws the local identfier in file of the serialized object target.
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="identfier"></param>
        public static void DrawLocalIdentifierInFile(this SerializedObject serializedObject, int identfier = 0)
        {
            EditorGUILayoutUtils.DrawLocalIdentifierInFile(serializedObject, identfier);
        }
        
        /// <summary>
        /// Draw the instance id of the serialized object target.
        /// </summary>
        /// <param name="serializedObject"></param>
        public static void DrawTargetInstanceID(this SerializedObject serializedObject)
        {
            EditorGUILayoutUtils.DrawTargetInstanceID(serializedObject);
        }

        
        /// <summary>
        /// Draw a reference to monoscript asset.
        /// </summary>
        /// <param name="serializedObject"></param>
        public static void DrawScriptReferenceField(this SerializedObject serializedObject)
        {
            EditorGUILayoutUtils.DrawScriptReferenceField(serializedObject);
        }
        
        /// <summary>
        /// Draw a reference to the editor monoscript asset.
        /// </summary>
        /// <param name="customEditor"></param>
        public static void DrawScriptReferenceField(this Editor customEditor)
        {
            EditorGUILayoutUtils.DrawScriptReferenceField(customEditor);
        }
        
        /// <summary>
        /// Draw a reference to self: it's useful to navigate different window and ping again the same object.
        /// </summary>
        /// <param name="serializedObject"></param>
        public static void DrawTargetObjectReferenceField(this SerializedObject serializedObject)
        {
            EditorGUILayoutUtils.DrawTargetObjectReferenceField(serializedObject);
        }
        
        /// <summary>
        /// It help to draw easily infos of a class in custom editors.
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="localIdentfierInFile"></param>
        public static void DrawSerializedObjectData(this SerializedObject serializedObject, int localIdentfierInFile = 0)
        {
            EditorGUILayoutUtils.DrawSerializedObjectData(serializedObject, localIdentfierInFile);
        }  
        
        #endregion
        
        #endregion
    }
}
#endif

