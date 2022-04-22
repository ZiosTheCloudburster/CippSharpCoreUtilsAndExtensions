#if UNITY_EDITOR
using UnityEditor;

namespace CippSharp.Core.EditorExtensions
{
    using SerializedObjectUtils = CippSharp.Core.EditorUtils.SerializedObjectUtils;
    using DrawSerializedPropertyDelegate = CippSharp.Core.EditorUtils.DrawSerializedPropertyDelegate;
    
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
    }
}
#endif
