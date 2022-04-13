//using UnityEngine;
//
//namespace CippSharp.Core
//{
//    public static class Vector3AxisUtils
//    {
//        /// <summary>
//        /// Converts Vector3Axis enum to Vector3.
//        /// Fallback Vector3.zero;
//        /// </summary>
//        /// <param name="axis"></param>
//        /// <param name="debugContext"></param>
//        /// <returns></returns>
//        public static Vector3 ToVector3(Vector3Axis axis, Object debugContext)
//        {
//            string logName = StringUtils.LogName(debugContext);
//
//            switch (axis)
//            {
//                case Vector3Axis.Back:
//                    return Vector3.back;
//                case Vector3Axis.Down:
//                    return Vector3.down;
//                case Vector3Axis.Forward:
//                    return Vector3.forward;
//                case Vector3Axis.Left:
//                    return Vector3.left;
//                case Vector3Axis.One:
//                    return Vector3.one;
//                case Vector3Axis.Right:
//                    return Vector3.right;
//                case Vector3Axis.Up:
//                    return Vector3.up;
//                case Vector3Axis.Zero:
//                    return Vector3.zero;
//                case Vector3Axis.Custom:
//                    Debug.Log(logName + " custom case not supported.", debugContext);
//                    break;
//                default:
//                    Debug.LogError(logName + " default case.", debugContext);
//                    break;
//            }
//
//            return Vector3.zero;
//        }
//
//        /// <summary>
//        /// Converts Vector3 to Vector3Axis enum.
//        /// </summary>
//        /// <param name="axis"></param>
//        /// <param name="debugContext"></param>
//        /// <returns></returns>
//        public static Vector3Axis ToVector3Axis(Vector3 axis, Object debugContext)
//        {
//            string logName = StringUtils.LogName(debugContext);
//
//            if (axis == Vector3.back)
//            {
//                return Vector3Axis.Back;
//            }
//            else if (axis == Vector3.down)
//            {
//                return Vector3Axis.Down;
//            }
//            else if (axis == Vector3.forward)
//            {
//                return Vector3Axis.Forward;
//            }
//            else if (axis == Vector3.left)
//            {
//                return Vector3Axis.Left;
//            }
//            else if (axis == Vector3.one)
//            {
//                return Vector3Axis.One;
//            }
//            else if (axis == Vector3.right)
//            {
//                return Vector3Axis.Right;
//            }
//            else if (axis == Vector3.up)
//            {
//                return Vector3Axis.Up;
//            }
//            else if (axis == Vector3.zero)
//            {
//                return Vector3Axis.Zero;
//            }
//            else
//            {
//                return Vector3Axis.Custom;
//            }
//        }
//
//    }
//}
