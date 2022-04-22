using System;
using UnityEngine;

namespace CippSharp.Core.Utils
{
    public static class RectUtils
    {
        public enum Alignment : byte
        {
            Left,
            Right
        }
        
        #region Rect → DivideSpace

        #region → Horizontal
        
        /// <summary>
        /// Retrieve the original rect space divided in horizontal by length.
        /// By default a space of 2 is considered between each element.
        ///
        /// Count must be >= 1
        /// </summary>
        /// <returns></returns>
        public static Rect[] DivideSpaceHorizontal(Rect position, int count)
        {
            return DivideSpaceHorizontal(position, count, 2.0f);
        }
        
        /// <summary>
        /// Retrieve the original rect space divided in horizontal by length.
        /// By default a space of 2 is considered between each element.
        ///
        /// Count must be >= 1
        /// </summary>
        /// <returns></returns>
        public static Rect[] DivideSpaceHorizontal(Rect position, int count, float spaceBetweenElements)
        {
            if (count < 1)
            {
                return null;
            }

            if (count == 1)
            {
                return new[] {position};
            }

            Rect[] subdivisions = new Rect[count];
            float startingX = position.x;
            float totalWidth = position.width;
            float lastX = startingX;
            for (int i = 0; i < count; i++)
            {
                Rect rI = position;
                float elementWidth = (totalWidth / count) - spaceBetweenElements * 0.5f;
                if (i != 0)
                {
                    rI.x = lastX + spaceBetweenElements * 0.5f;
                }
                if (i == count -1)
                {
                    elementWidth += spaceBetweenElements * 0.5f;
                }
                rI.width = elementWidth;
                lastX += rI.width;
                subdivisions[i] = rI;
            }

            return subdivisions;
        }
        
         /// <summary>
        /// Retrieve the original rect space divided in horizontal by length.
        /// By default a space of 2 is considered between each element.
        ///
        /// Count must be >= 1
        /// </summary>
        /// <returns></returns>
        public static Rect[] DivideSpaceHorizontal(Rect position, int count, float minElementWidth, float spaceBetweenElements)
        {
            if (count < 1)
            {
                return null;
            }

            if (count == 1)
            {
                return new[] {position};
            }

            Rect[] subdivisions = new Rect[count];
            float startingX = position.x;
            float totalWidth = position.width;
            float lastX = startingX;
            for (int i = 0; i < count; i++)
            {
                Rect rI = position;
                float elementWidth = Mathf.Max((totalWidth / count) - spaceBetweenElements * 0.5f, minElementWidth);
                if (i != 0)
                {
                    rI.x = lastX + spaceBetweenElements * 0.5f;
                }
                if (i == count -1)
                {
                    elementWidth += spaceBetweenElements * 0.5f;
                }
                rI.width = elementWidth;
                lastX += rI.width;
                subdivisions[i] = rI;
            }

            return subdivisions;
        }

        /// <summary>
        /// Retrieve the original rect space divided in horizontal by length.
        /// By default a space of 2 is considered between each element.
        ///
        /// Count must be >= 1
        /// </summary>
        /// <returns></returns>
        public static Rect[] DivideSpaceHorizontal(Rect position, int count, float maxElementWidth, float minElementWidth, float spaceBetweenElements)
        {
            if (minElementWidth > maxElementWidth)
            {
                throw new Exception("You are more imbecil than one hilichurl!");
            }
            
            if (count < 1)
            {
                return null;
            }

            if (count == 1)
            {
                return new[] {position};
            }

            Rect[] subdivisions = new Rect[count];
            float startingX = position.x;
            float totalWidth = position.width;
            float lastX = startingX;
            for (int i = 0; i < count; i++)
            {
                Rect rI = position;
                float elementWidth = Mathf.Max((totalWidth / count) - spaceBetweenElements * 0.5f, minElementWidth);
                elementWidth = Mathf.Min(elementWidth, maxElementWidth);
                if (i != 0)
                {
                    rI.x = lastX + spaceBetweenElements * 0.5f;
                }
                if (i == count -1)
                {
                    elementWidth += spaceBetweenElements * 0.5f;
                }
                rI.width = elementWidth;
                lastX += rI.width;
                subdivisions[i] = rI;
            }

            return subdivisions;
        }
        
        #endregion
        
        #region → Horizontal by Ratio
        
        /// <summary>
        /// Divide two thirds the horizontal space of a rect 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="spaceBetweenElements"></param>
        /// <param name="alignment"></param>
        /// <returns>rect[] of length 2</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Rect[] DivideSpaceHorizontalTwoThirds (Rect position, float spaceBetweenElements, Alignment alignment = Alignment.Left)
        {
            return DivideSpaceHorizontalByRatio(position, 2.0f / 3.0f, spaceBetweenElements, alignment);
        }

        /// <summary>
        /// Divide space by ratio in 2 rects.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="ratio"></param>
        /// <param name="spaceBetweenElements"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Rect[] DivideSpaceHorizontalByRatio (Rect position, float ratio, float spaceBetweenElements, Alignment alignment = Alignment.Left)
        {
            Rect[] subdivisions = new Rect[2];
            float startingX = position.x;
            float totalWidth = position.width;
            float delta = (totalWidth * ratio);
            float width0 = totalWidth - delta;
            float width1 = totalWidth - width0 ;
            
            switch (alignment)
            {
                case Alignment.Left:
                    width0 -= spaceBetweenElements * 0.5f;
                    subdivisions[0] = new Rect(startingX, position.y, width0, position.height);
                    subdivisions[1] = new Rect(startingX + subdivisions[0].width + spaceBetweenElements * 0.5f, position.y, width1, position.height);
                    break;
                case Alignment.Right:
                    width1 -= spaceBetweenElements * 0.5f;
                    subdivisions[0] = new Rect(startingX, position.y, width1, position.height);
                    subdivisions[1] = new Rect(startingX + subdivisions[0].width + spaceBetweenElements * 0.5f, position.y, width0, position.height);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null);
            }

            return subdivisions;
        }


        #endregion

        #endregion
    }
}
