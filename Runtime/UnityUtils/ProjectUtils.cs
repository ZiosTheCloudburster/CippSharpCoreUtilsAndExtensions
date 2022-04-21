using UnityEngine;

namespace CippSharp.Core.Utils
{
    public static class ProjectUtils
    {
        /// <summary>
        /// Retrieve project name by Application.dataPath;
        /// </summary>
        /// <returns></returns>
        public static string GetProjectName()
        {
            string[] s = Application.dataPath.Split('/');
            string projectName = s[s.Length - 2];
            return projectName;
        }
    }
}
