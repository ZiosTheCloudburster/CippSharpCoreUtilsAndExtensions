using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
	#if UNITY_2018_3_OR_NEWER
using UnityEditor.Experimental.SceneManagement;
	#endif
#endif

namespace CippSharp.Core.Utils
{
    public static class PrefabUtils
    {
        /// <summary>
        /// Check if we're in a prefab context.
        /// </summary>
        /// <returns></returns>
        public static bool IsPrefabContext()
        {
#if UNITY_EDITOR
	#if UNITY_2018_3_OR_NEWER
            return PrefabStageUtility.GetCurrentPrefabStage() != null;
	#else
			return false;
	#endif
#else
			return false;
#endif
        }
		
        /// <summary>
        /// Check if an object is in a prefab.
        /// </summary>
        /// <param name="interestedObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsPrefab<T>(T interestedObject) where T : Object
        {
#if UNITY_EDITOR
	#if UNITY_2019_4_OR_NEWER
	        bool isPartOfAnyPrefab = UnityEditor.PrefabUtility.IsPartOfAnyPrefab(interestedObject);
	        if (isPartOfAnyPrefab)
	        {
		        return true;
	        }

	        GameObject g = GameObjectUtils.From(interestedObject);
	        if (g != null)
	        {
		        var stage = PrefabStageUtility.GetCurrentPrefabStage();
		        return stage != null && stage.IsPartOfPrefabContents(g);;
	        }
	        else
	        {
		        return false;
	        }
	#elif UNITY_2018_3_OR_NEWER
            return PrefabStageUtility.GetCurrentPrefabStage() != null;
	#else
			return false;
	#endif
#else
			return false;
#endif
        }
    }
}