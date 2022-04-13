//using CippSharp.Core.UI;
//using UnityEngine;
//using UnityEngine.UI;
//
//namespace CippSharp.Core.Extensions
//{
//    using SpriteChangedEvent = CippSharp.Core.UI.SpriteChangedEvent;
//    
//    public static class ImageExtensions
//    {
//        /// <summary>
//        /// Add listener on sprite changed event
//        /// </summary>
//        /// <param name="image"></param>
//        /// <param name="callback"></param>
//        public static void AddSpriteChangedListener(this Image image, SpriteChangedEvent callback)
//        {
//            if (image == null)
//            {
//                return;
//            }
//
//            image.gameObject.GetOrAddComponent<ImageSpriteChangedListener>().onSpriteChanged += callback;
//        }
//
//        /// <summary>
//        /// Remove listener on sprite changed event
//        /// </summary>
//        /// <param name="image"></param>
//        /// <param name="callback"></param>
//        public static void RemoveSpriteChangedListener(this Image image, SpriteChangedEvent callback)
//        {
//            if (image == null)
//            {
//                return;
//            }
//
//            image.gameObject.GetOrAddComponent<ImageSpriteChangedListener>().onSpriteChanged -= callback;
//        }
//    }
//}
