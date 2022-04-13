
using UnityEngine;

namespace CippSharp.Core
{
    public static class SpriteUtils
    {
        /// <summary>
        /// Retrieve Sprite's aspect ratio
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public static float GetAspectRatio(Sprite sprite)
        {
            Rect rect = sprite.rect;
            float w = rect.width;
            float h = rect.height;
            return (float)((double)w / (double)h);
        }
    }
}
