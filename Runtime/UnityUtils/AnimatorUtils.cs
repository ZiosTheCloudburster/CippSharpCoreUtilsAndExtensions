/* 
    Author: Alessandro Salani (Cippo) 
*/

using UnityEngine;

namespace CippSharp.Core
{
    public static class AnimatorUtils
    {
        #region Animator State Info Name
        
        /// <summary>
        /// Check if the current animator state info has any of the given names.
        /// </summary>
        /// <param name="animatorStateInfo"></param>
        /// <param name="names"></param>
        /// <param name="index"></param>
        public static bool IsAnyName(AnimatorStateInfo animatorStateInfo, string[] names, out int index)
        {
            index = -1;
            for (int i = 0; i < names.Length; i++)
            {
                string name = names[i];
                if (!animatorStateInfo.IsName(name))
                {
                    continue;
                }
                
                index = i;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if the current animator state info is represented with the given hash.
        ///
        /// Listen: https://youtu.be/u1kZ9zYr7kk (hash)
        /// </summary>
        /// <param name="animatorStateInfo"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool IsHash(AnimatorStateInfo animatorStateInfo, int hash)
        {
            return hash == animatorStateInfo.fullPathHash || hash == animatorStateInfo.shortNameHash ||
#pragma warning disable 618
                   hash == animatorStateInfo.nameHash;
#pragma warning restore 618
        }

        /// <summary>
        /// Returns the first valid hash
        /// </summary>
        /// <param name="animatorStateInfo"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool GetValidHash(AnimatorStateInfo animatorStateInfo, out int hash)
        {
            hash = 0;
            if (animatorStateInfo.fullPathHash != 0)
            {
                hash = animatorStateInfo.fullPathHash;
                return true;
            }
            else if (animatorStateInfo.shortNameHash != 0)
            {
                hash = animatorStateInfo.shortNameHash;
                return true;
            }
#pragma warning disable 618
            else if (animatorStateInfo.nameHash != 0)
            {
                hash = animatorStateInfo.nameHash;
                return true;
            }
#pragma warning restore 618
            else
            {
                return false;
            }
        }
        
        #endregion
    }
}
