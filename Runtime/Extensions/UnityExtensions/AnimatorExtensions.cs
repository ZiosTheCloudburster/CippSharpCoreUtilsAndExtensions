using UnityEngine;

namespace CippSharp.Core
{
    public static class AnimatorExtensions
    {
        /// <summary>
        /// Check if the current animator state info has any of the given names.
        /// </summary>
        /// <param name="animatorStateInfo"></param>
        /// <param name="names"></param>
        /// <param name="index"></param>
        public static bool IsAnyName(this AnimatorStateInfo animatorStateInfo, string[] names, out int index)
        {
            return AnimatorUtils.IsAnyName(animatorStateInfo, names, out index);
        }
        
        
        /// <summary>
        /// Check if the current animator state info is represented with the given hash.
        /// </summary>
        /// <param name="animatorStateInfo"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool IsHash(this AnimatorStateInfo animatorStateInfo, int hash)
        {
            return AnimatorUtils.IsHash(animatorStateInfo, hash);
        }
    }
}
