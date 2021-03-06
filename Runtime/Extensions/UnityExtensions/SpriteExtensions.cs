using UnityEngine;

namespace CippSharp.Core.Extensions
{
    using SpriteUtils = CippSharp.Core.Utils.SpriteUtils;
    
    public static class SpriteExtensions
    {
        /// <summary>
        /// Retrieve Sprite's aspect ratio
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public static float GetAspectRatio(this Sprite sprite)
        {
            return SpriteUtils.GetAspectRatio(sprite);
        }
    }
}
