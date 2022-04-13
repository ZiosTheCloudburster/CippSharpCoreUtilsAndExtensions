//
// Author: Alessandro Salani (Cippo)
//

using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core
{
	public static class ResourcesUtils
	{
		/// <summary>
		/// Async loading of an asset and pass it through a callback.
		///
		/// WARNING: only 1 loading of 'anything' is allowed by unity by default at once. This means that
		/// if you're loading a resource asyncly you cannot load another resource. Also includes scene async loading.
		/// </summary>
		/// <param name="resourcePath"></param>
		/// <param name="onAssetLoaded"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static IEnumerator AsyncLoadAsset<T>(string resourcePath, Action<T> onAssetLoaded) where T : Object
		{
			ResourceRequest request = Resources.LoadAsync<T>(resourcePath);
			yield return request;
			
			T loadedAsset = request.asset as T;
			if (loadedAsset == null)
			{
				Debug.LogWarning("The loaded asset is null. Check is resourcesPath and/or his type "+typeof(T).Name+".");
			}
        
			onAssetLoaded.Invoke(loadedAsset);
			yield return request;
		}
	}
}
