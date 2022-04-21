//
// Author: Alessandro Salani (Cippo)
//

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CippSharp.Core.Utils
{
    /// <summary>
    /// (as long as GUIContent is not under UnityEditor's jurisdiction...)
    /// </summary>
    public static class GUIContentUtils
    {
        /// <summary>
        /// A string array to an array of GUI Contents
        /// </summary>
        /// <returns></returns>
        public static GUIContent[] ToGUIContents(IEnumerable<string> contents)
        {
            return contents.Select(c => new GUIContent(c)).ToArray();
        }
    }
}
