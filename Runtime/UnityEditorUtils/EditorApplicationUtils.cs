#if UNITY_EDITOR
//
// Author: Alessandro Salani (Cippo)
//

using System;
using Object = UnityEngine.Object;

namespace CippSharp.Core.EditorUtils
{
	[Obsolete("Use EditorObjectUtils instead.")]
	public static class EditorApplicationUtils
	{
		[Obsolete("2021/08/14 → moved to EditorObjectUtils. This will be removed in future versions.")]
		public static void SetDirty(Object target)
		{
			return;
		}

		[Obsolete("2021/08/14 → moved to SerializedObjectUtils. This will be removed in future versions.")]
		public static Object[] GetTargetObjects<T>(T serializedObject)
		{
			return null;
		}
	}
}
#endif
