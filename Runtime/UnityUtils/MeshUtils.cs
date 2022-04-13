using UnityEngine;

namespace CippSharp.Core
{
    public static class MeshUtils
    {
        /// <summary>
        /// Retrieve primitive shared mesh.
        /// </summary>
        /// <param name="primitiveType"></param>
        /// <returns></returns>
        public static Mesh GetPrimitiveSharedMesh(PrimitiveType primitiveType)
        {
            GameObject holder = GameObject.CreatePrimitive(primitiveType);
            Mesh mesh = holder.GetComponent<MeshFilter>().sharedMesh;
            Object.DestroyImmediate(holder);
            return mesh;
        }

    }
}
