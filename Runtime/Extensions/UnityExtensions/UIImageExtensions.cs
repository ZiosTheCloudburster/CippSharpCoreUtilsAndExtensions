using UnityEngine;
using UnityEngine.UI;

namespace CippSharp.Core.Extensions
{
    public static class UIImageExtensions
    {
        /// <summary>
        /// Retrieve image's color alpha
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static float GetAlpha(this Image image)
        {
            return image.color.a;
        }

        /// <summary>
        /// Set image's color alpha
        /// </summary>
        /// <param name="image"></param>
        /// <param name="alpha"></param>
        public static void SetAlpha(this Image image, float alpha)
        {
            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }
    }
}
