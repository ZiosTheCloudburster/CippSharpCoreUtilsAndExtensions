using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core
{
    public static class MeshUtils
    {
        /// <summary>
        /// Store Primitives Meshes here.
        /// (for easy access during runtime if they're called often)
        /// </summary>
        private static readonly Dictionary<PrimitiveType, Mesh> LoadedMeshes = new Dictionary<PrimitiveType, Mesh>();
        
        /// <summary>
        /// Retrieve primitive shared mesh.
        /// </summary>
        /// <param name="primitiveType"></param>
        /// <returns></returns>
        public static Mesh GetPrimitiveSharedMesh(PrimitiveType primitiveType)
        {
            if (LoadedMeshes.TryGetValue(primitiveType, out Mesh storedMesh) && storedMesh != null)
            {
                return storedMesh;
            }
            else
            {
                GameObject holder = GameObject.CreatePrimitive(primitiveType);
                Mesh mesh = holder.GetComponent<MeshFilter>().sharedMesh;
                LoadedMeshes[primitiveType] = mesh;
                return mesh;
            }
        }

    }
}
