
namespace CippSharp.Core.Utils
{
    using Color = UnityEngine.Color;
    using Vector4 = UnityEngine.Vector4;
    
    public static class ColorUtils
    {
        /// <summary>
        /// Retrieve a color with random values
        /// </summary>
        /// <param name="includeAlpha"></param>
        /// <returns></returns>
        public static Color Random(bool includeAlpha = false)
        {
            const float zero = 0.000f;
            const float one = 1.000f;
            
            float r = UnityEngine.Random.Range(zero, one);
            float g = UnityEngine.Random.Range(zero, one);
            float b = UnityEngine.Random.Range(zero, one);

            if (!includeAlpha)
            {
                return new Color(r, g, b);
            }

            float a = UnityEngine.Random.Range(zero, one);
            return new Color(r, g, b, a);

        }
        
        /// <summary>
        /// Converts a Color to Vector4
        /// </summary>
        /// <returns></returns>
        public static Vector4 ToVector4(Color color)
        {
            return new Vector4(color.r, color.g, color.b, color.a);
        }
    }
}
