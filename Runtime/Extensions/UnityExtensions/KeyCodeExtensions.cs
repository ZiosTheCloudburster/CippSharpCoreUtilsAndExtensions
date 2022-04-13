using UnityEngine;

namespace CippSharp.Core.Extensions
{
    public static class KeyCodeExtensions 
    {
        /// <summary>
        /// InLine Input.GetKeyDown
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetKeyDown(this KeyCode key)
        {
            return Input.GetKeyDown(key);
        }

        /// <summary>
        /// InLine Input.GetKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetKey(this KeyCode key)
        {
            return Input.GetKey(key);
        }
        
        /// <summary>
        /// InLine Input.GetKeyUp
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetKeyUp(this KeyCode key)
        {
            return Input.GetKeyUp(key);
        }
    }
}
