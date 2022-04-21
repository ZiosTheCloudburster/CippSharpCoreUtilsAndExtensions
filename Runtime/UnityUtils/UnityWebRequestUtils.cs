using UnityEngine.Networking;

namespace CippSharp.Core.Utils
{
    public static class UnityWebRequestUtils
    {
        /// <summary>
        /// Try to get text from unity web request
        /// </summary>
        /// <param name="webRequest"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool TryGetText(UnityWebRequest webRequest, out string text)
        {
            try
            {
                text = webRequest.downloadHandler.text;
                return true;
            }
            catch
            {
                text = string.Empty;
                return false;
            }
        }
    }
}
