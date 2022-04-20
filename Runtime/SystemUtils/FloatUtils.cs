using System;
using UnityEngine;

namespace CippSharp.Core.Utils
{
    public static class FloatUtils
    {
        private static readonly string LogName = $"[{nameof(FloatUtils)}]: ";

        #region → Comparison

        /// <summary>
        /// Returns true if a float is equal to target 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsEqualTo(float value, float target)
        {
            if (target == value)
            {
                return true;
            }
            else if (target < value)
            {
                return false;
            }
            else if (target > value)
            {
                return false;
            }
            else
            {
                Debug.LogError(LogName+$"{nameof(IsEqualTo)} what is this case?");
                return true;
            }
        }
                
        /// <summary>
        /// Returns true if the passed measure is equal to the distance with a tolerance
        /// </summary>
        /// <param name="measure"></param>
        /// <param name="distance"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsInRange(float measure, float distance, float tolerance)
        {
            return Math.Abs(measure - distance) < tolerance;
        }
        
        #endregion

        #region → Methods
         
        /// <summary>
        /// Retrieve a percentage float of passed one
        /// </summary>
        /// <returns></returns>
        public static float Perc(float value, float perc)
        {
            return value * (perc / 100.0000000f);
        }
        
        /// <summary>
        /// Round a measure to target x numbers after comma.
        /// </summary>
        /// <param name="measure"></param>
        /// <param name="places"></param>
        /// <returns></returns>
        public static float Round(float measure, int places = 1)
        {
            int amount = 10 * places;
            return (float)Math.Round((measure * amount) / amount);
        }

        /// <summary>
        /// Returns index of largest of two or more values.
        /// </summary>
        /// <returns></returns>
        public static int IndexOfMax(float[] values)
        {
            if (values == null || values.Length <= 0)
            {
                return -1;
            }
            
            int length = values.Length;
            float num = values[0];
            int index = 0;
            for (int i = 1; i < length; ++i)
            {
                if ((double) values[i] > (double) num)
                {
                    num = values[i];
                    index = i;
                }
            }
            return index;
        }
        
        #endregion
      
        #region → Rolls

        /// <summary>
        /// Roll a d100 with floating numbers.
        /// </summary>
        /// <returns></returns>
        public static float floatD100()
        {
            return UnityEngine.Random.Range(0.00f, 100.00f);
        }
        
        /// <summary>
        /// Roll a d100 with floating numbers.
        /// </summary>
        /// <returns></returns>
        public static float floatDice(float max = 100.00f)
        {
            return UnityEngine.Random.Range(0.00f, max);
        }
        
        #endregion
      
    }
}
