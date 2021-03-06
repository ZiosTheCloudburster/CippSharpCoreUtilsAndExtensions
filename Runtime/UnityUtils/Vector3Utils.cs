using System;
using UnityEngine;

namespace CippSharp.Core.Utils
{
    public static class Vector3Utils
    {
        #region Vector3 → Methods

        #region → ABS
       
        /// <summary>
        /// Mathf Abs on each float of referred Vector3
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static void Abs(ref Vector3 input)
        {
            input.x = Mathf.Abs(input.x);
            input.y = Mathf.Abs(input.y);
            input.z = Mathf.Abs(input.z);
        }

        /// <summary>
        /// Mathf Abs on each float of Vector3
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Vector3 Abs(Vector3 input)
        {
            Abs(ref input);
            return input;
        }
        
        #endregion

        #region → Clamp Magnitude
        
        /// <summary>
        /// Clamp magnitude of a referred vector
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static void ClampMagnitude(ref Vector3 vector, float maxLength)
        {
            float sqrmag = vector.sqrMagnitude;
            if (sqrmag > maxLength * maxLength)
            {
                float mag = (float) Math.Sqrt(sqrmag);
                //these intermediate variables force the intermediate result to be
                //of float precision. without this, the intermediate result can be of higher
                //precision, which changes behavior.
                float normalized_x = vector.x / mag;
                float normalized_y = vector.y / mag;
                float normalized_z = vector.z / mag;
                vector.x = normalized_x * maxLength;
                vector.y = normalized_y * maxLength;
                vector.z = normalized_z * maxLength;
            }
        }

        /// <summary>
        /// Clamp magnitude of a vector
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
        {
            ClampMagnitude(ref vector, maxLength);
            return vector;
        }
        
        #endregion
        
        /// <summary>
        /// Retrieve the closest point from a Vector3[].
        /// </summary>
        /// <param name="point"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Vector3 Closest(Vector3 point, Vector3[] array)
        {
            float minDistance = Mathf.Infinity;
            Vector3 closest = Vector3.zero;
            foreach (var v in array)
            {
                float currentDistance = Vector3.Distance(point, v);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closest = v;
                }
            }

            return closest;
        }
        
        /// <summary>
        /// Contains a value
        /// </summary>
        /// <param name="v"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains(Vector3 v, float value)
        {
            for (int i = 0; i < 3; i++)
            {
                float element = v[i];
                if (FloatUtils.IsEqualTo(element, value))
                {
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Retrieve if current vector c is in line with a and b.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsBetweenAB(Vector3 c, Vector3 a, Vector3 b)
        {
            return Vector3.Dot((b - a).normalized, (c - b).normalized) < 0f &&
                   Vector3.Dot((a - b).normalized, (c - a).normalized) < 0f;
        }
        
        /// <summary>
        /// Check if a vector is equal to another with a tolerance
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsSimilar (Vector3 a, Vector3 b, float tolerance = 0.0001f) 
        {
            return Vector3.SqrMagnitude(a - b) <= tolerance;
        }
		
        /// <summary>
        /// Retrieve if current vector is inside an area.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="areaMinValues"></param>
        /// <param name="areaMaxValues"></param>
        /// <returns></returns>
        public static bool IsInArea(Vector3 position, Vector3 areaMinValues, Vector3 areaMaxValues)
        {
            return position.x >= areaMinValues.x && position.x <= areaMaxValues.x
                   && position.y >= areaMinValues.y && position.y <= areaMaxValues.y
                   && position.z >= areaMinValues.z && position.z <= areaMaxValues.z;
			
        }

        #region → Random
        
        /// <summary>
        /// Retrieve a random vector between two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Random(Vector3 a, Vector3 b)
        {
            return new Vector3(UnityEngine.Random.Range(a.x, b.x), UnityEngine.Random.Range(a.y, b.y), UnityEngine.Random.Range(a.z, b.z));
        }
       
        /// <summary>
        /// Retrieve a random vector to represent a direction between -1 and 1
        /// "double greed 1"
        /// </summary>
        /// <returns></returns>
        public static Vector3 RandomDirection()
        {
            return UnityEngine.Random.insideUnitSphere.normalized;
        }
        
        /// <summary>
        /// Retrieve a random vector that should be on sphere surface with X radius
        /// </summary>
        /// <param name="radius">the sphere radius</param>
        /// <returns></returns>
        public static Vector3 RandomOnSphere(float radius)
        {
            Vector3 dir = RandomDirection();
            return dir * radius;
        }
        
        /// <summary>
        /// Retrieve a random vector that should be on sphere surface with X radius
        /// </summary>
        /// <param name="center">the sphere center</param>
        /// <param name="radius">the sphere radius</param>
        /// <returns></returns>
        public static Vector3 RandomOnSphere(Vector3 center, float radius)
        {
            return center + RandomOnSphere(radius);
        }
        
        /// <summary>
        /// Randomize a Vector3 by passing an already cached random
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="random"></param>
        /// <param name="xyz">filter</param>
        public static void RandomizeVector(ref Vector3 vector, ref Vector3 random, params bool[] xyz)
        {
            if (xyz == null || xyz.Length <= 0)
            {
                vector.x = random.x;
                vector.y = random.y;
                vector.z = random.z;
            }
            else
            {
                int length = xyz.Length;
                if (xyz[0])
                {
                    vector.x = random.x;
                }
                if (ArrayUtils.ElementAtOrDefault(xyz, 1))
                {
                    vector.y = random.y;
                }
                if (ArrayUtils.ElementAtOrDefault(xyz, 2))
                {
                    vector.z = random.z;
                }
            }
        }
        
        #endregion

        #region → Set

        /// <summary>
        /// Set Vector3 X by ref
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        public static void SetX(ref Vector3 input, float value)
        {
            input.x = value;
        }

        /// <summary>
        /// Set Vector3 X
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        public static Vector3 SetX(Vector3 input, float value)
        {
            SetX(ref input, value);
            return input;
        }

        /// <summary>
        /// Set Vector3 Y by ref
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        public static void SetY(ref Vector3 input, float value)
        {
            input.y = value;
        }

        /// <summary>
        /// Set Vector3 Y
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        public static Vector3 SetY(Vector3 input, float value)
        {
            SetY(ref input, value);
            return input;
        }

        /// <summary>
        /// Set Vector3 Z by ref
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        public static void SetZ(ref Vector3 input, float value)
        {
            input.y = value;
        }

        /// <summary>
        /// Set Vector3 Z
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        public static Vector3 SetZ(Vector3 input, float value)
        {
            SetZ(ref input, value);
            return input;
        }

        #endregion
        
        //Do you like scales? There's need to UnScale sometimes.
        // → https://youtu.be/wuO_r6Qre9Q
        #region → UnScale
        
        /// <summary>
        /// Divides every component of this vector by the same component of scale.
        /// <see cref="Vector3.Scale"/>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void UnScale(ref Vector3 a, Vector3 b)
        {
            a.x /= b.x;
            a.y /= b.y;
            a.z /= b.z;
        }

        /// <summary>
        /// Divides every component of this vector by the same component of scale.
        /// <see cref="Vector3.Scale"/>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Vector3 UnScale(Vector3 a, Vector3 b)
        {
            UnScale(ref a, b);
            return a;
        }
        
        #endregion
        
        #endregion
    }
}
