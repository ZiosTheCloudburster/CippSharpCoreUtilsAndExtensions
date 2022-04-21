//
// Author: Alessandro Salani (Cippo)
//

namespace CippSharp.Core.EditorUtils
{
	using Object = UnityEngine.Object;
	using ArrayUtils = CippSharp.Core.Utils.ArrayUtils;
	
    public static class EditorObjectUtils
    {
	    //These methods are when not in editor 
	    #region → Set Dirty
	    
	    #region → Object
	    
		/// <summary>
		/// Set target object dirty.
		/// </summary>
		/// <param name="target"></param>
		public static void SetDirty(Object target)
		{
#if UNITY_EDITOR
			if (target == null)
			{
				return;
			}
			
			UnityEditor.EditorUtility.SetDirty(target);
#endif
		}
	    
	    #endregion
	    
	    #region → Object[]
	    
		/// <summary>
		/// Set target objects dirty.
		/// </summary>
		/// <param name="targets"></param>
		public static void SetDirty(Object[] targets)
		{
#if UNITY_EDITOR			
			if (ArrayUtils.IsNullOrEmpty(targets))
			{
				return;
			}
			
			foreach (var o in targets)
			{
				if (o == null)
				{
					continue;
				}

				UnityEditor.EditorUtility.SetDirty(o);
			}
#endif
		}
	    
	    #endregion    
		
		#endregion
    }
}
