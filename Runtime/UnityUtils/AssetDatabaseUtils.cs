/* 
    Author: Alessandro Salani (Cippo) 
*/

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Utils
{
    public static class AssetDatabaseUtils
    {
        /// <summary>
        /// A better name for logs
        /// </summary>
        private static readonly string LogName = $"[{nameof(AssetDatabaseUtils)}]: ";
        
        /// <summary>
        /// Retrieve the asset database path during editor.
        /// In build it retrieve "";
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public static string GetAssetPath(Object asset)
        {
#if UNITY_EDITOR
            return AssetDatabase.GetAssetPath(asset);
#else
            return string.Empty;
#endif
        }
        
        /// <summary>
        /// Retrieve true if the path of the object is null or empty.
        /// </summary>
        /// <param name="interestedObject"></param>
        /// <returns></returns>
        public static bool IsObjectPathNullOrEmpty<T>(T interestedObject) where T : Object
        {
            Object target = interestedObject;
            if (interestedObject is Component c)
            {
                target = (Object)(c.gameObject);
            }
            string assetPath = GetAssetPath(target);
            return string.IsNullOrEmpty(assetPath);
        }

        #region Load Target Asset
        
        /// <summary>
        /// This load the first asset found with given assetDatabase filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadTargetAsset<T>(string filter) where T : Object
        {
#if UNITY_EDITOR
            string[] guids = AssetDatabase.FindAssets(filter);
            if (ArrayUtils.IsNullOrEmpty(guids))
            {
                return null;
            }

            List<string> filteredPaths = guids.Select(AssetDatabase.GUIDToAssetPath).ToList();

            try
            {
                return AssetDatabase.LoadAssetAtPath<T>(filteredPaths.FirstOrDefault());
            }
            catch (Exception e)
            {
                Debug.LogError(LogName+$"{nameof(LoadTargetAsset)} failed. Caught exception: {e.Message}.");
            }

            return null;
#else
            return null;
#endif
        }

        
        /// <summary>
        /// This load the first asset found with given assetDatabase filter.
        /// If it fails the first time it tries to load it from resources by assetName.
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="filter"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadTargetAsset<T>(string assetName, string filter) where T : Object
        {
#if UNITY_EDITOR
            string[] guids = AssetDatabase.FindAssets(filter);
            if (ArrayUtils.IsNullOrEmpty(guids))
            {
                Debug.LogWarning("No assets found.");
                return null;
            }

            List<string> filteredPaths = guids.Select(AssetDatabase.GUIDToAssetPath).ToList();
            T loaded = null;
            try
            {
                loaded = AssetDatabase.LoadAssetAtPath<T>(filteredPaths.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == assetName));
            }
            catch (Exception e)
            {
                loaded = null;
                Debug.LogError(LogName+$"{nameof(LoadTargetAsset)} failed. Caught exception: {e.Message}.");
            }

            if (loaded == null)
            {
                try
                {
                    loaded = Resources.Load<T>(assetName);
                }
                catch (Exception e)
                {
                    loaded = null;
                    Debug.LogError(LogName + $"{nameof(LoadTargetAsset)} failed. Caught exception: {e.Message}.");
                }
            }

            return loaded;
#else
            return Resources.Load<T>(assetName);
#endif
        }
        
        #endregion
  
    }
}
