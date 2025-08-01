namespace Environment.Instancing
{
    using UnityEngine;

    using System.Collections.Generic;
    using Environment.Utilities;

    /// <summary>
    /// Instances behaviour to generate on surfaces of meshes.
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MeshInstancesBehaviour : InstancesBehaviour
    {
        [Header("Sub Mesh Details")]
        public bool UseSubMesh = false;
        public int SubMeshIndex = 0;

        [Header("Instance Settings")]
        public float Density = 1f;
        public InstancingSettings[] InstancingSettings;


        /// <summary>
        /// Calculates the bounds for the instances used for culling by Unity.
        /// </summary>
        public override Bounds CalculateInstancesBounds()
        {
            var rendererBounds = this.GetComponent<MeshRenderer>().bounds;
            var diff = this.transform.position - rendererBounds.center;
            var absDiff = new Vector3(Mathf.Abs(diff.x),
                                      Mathf.Abs(diff.y),
                                      Mathf.Abs(diff.z));

            return new Bounds(this.transform.position, (rendererBounds.extents + absDiff) * 2f);
        }

        /// <summary>
        /// Implementation of instance data logic for instances behaviour.
        /// </summary>
        public override Dictionary<InstancingSettings, List<InstanceData>> GetInstanceData()
{
    if (InstancingSettings == null || InstancingSettings.Length == 0)
    {
        Debug.LogWarning("Instancing settings should be defined in the Unity Editor.");
        return null;
    }

    foreach (var settings in InstancingSettings)
    {
        if (settings.Scale <= 0f || settings.Material == null || settings.Mesh == null)
        {
            Debug.LogError("Each Instance Configuration must have a valid material, mesh, and a scale > 0.");
            return null;
        }
    }

    var meshFilter = GetComponent<MeshFilter>();
    var mesh = meshFilter.sharedMesh;

    if (mesh == null)
    {
        Debug.LogError("MeshFilter does not have a mesh assigned.");
        return null;
    }

    if (UseSubMesh)
    {
        if (!mesh.isReadable)
        {
            Debug.LogWarning($"Mesh '{mesh.name}' is not readable. Using full mesh instead of submesh.");
            UseSubMesh = false; // fallback
        }
        else if (SubMeshIndex >= mesh.subMeshCount || SubMeshIndex < 0)
        {
            Debug.LogWarning($"Invalid SubMeshIndex ({SubMeshIndex}). Mesh has only {mesh.subMeshCount} submeshes. Using full mesh instead.");
            UseSubMesh = false; // fallback
        }
        else
        {
            var subMesh = new Mesh
            {
                vertices = mesh.vertices,
                triangles = mesh.GetTriangles(SubMeshIndex)
            };
            subMesh.RecalculateNormals();
            mesh = subMesh;
        }
    }

    var instanceData = RandomMeshInstanceData(mesh, Density, InstancingSettings);
    return DivideInstanceData(instanceData, InstancingSettings);
}


        /// <summary>
        /// Gets random mesh instance data on a mesh.
        /// </summary>
        public static InstanceData[] RandomMeshInstanceData(Mesh mesh, float density, InstancingSettings[] configurations)
        {
            if (mesh == null || density <= 0 || configurations == null || configurations.Length == 0)
            {
                return null;
            }

            var instanceAmount = Mathf.CeilToInt(MeshUtilities.GetMeshArea(mesh) / density);

            var samples = MeshUtilities.RandomMeshPoints(mesh, instanceAmount);

            var data = new List<InstanceData>();

            for (var i = 0; i < samples.Length; i++)
            {
                var (vertex, normal) = samples[i];

                var instance = new InstanceData
                {
                    TRS = Matrix4x4.TRS(
                        vertex,
                        Quaternion.identity,
                        Vector3.one
                    ),
                    Normal = normal
                };
                data.Add(instance);
            }

            return data.ToArray();
        }
    }
}