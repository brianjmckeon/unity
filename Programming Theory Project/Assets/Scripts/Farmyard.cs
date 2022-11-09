using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmyard : MonoBehaviour
{
    public GameObject resourcePrefab;
    private GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.Find("Ground");
        buildFarmyard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void buildFarmyard()
    {
        var groundMesh = ground.GetComponent<MeshRenderer>();
        var resourceMesh = resourcePrefab.GetComponent<MeshRenderer>();

        // Determine how many rows and columns of resource prefabs we can fit within our bounds
        var rows = groundMesh.bounds.extents.z / resourceMesh.bounds.extents.z;
        var columns = groundMesh.bounds.extents.x / resourceMesh.bounds.extents.x;

        //Debug.Log($"Ground: z: {groundMesh.bounds.extents.z} x: {groundMesh.bounds.extents.x}");
        Debug.Log($"Resource: z: {resourceMesh.bounds.extents.z} x: {resourceMesh.bounds.extents.x}");

        // x -25 25
        // z -15 35
        Debug.Log($"Ground {groundMesh.bounds.min.z} {groundMesh.bounds.max.z}");

        float startX = groundMesh.bounds.min.x + resourceMesh.bounds.extents.x;
        float startZ = groundMesh.bounds.min.z + resourceMesh.bounds.extents.z;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                var pos = new Vector3(
                    startX + c * resourceMesh.bounds.extents.x * 2,
                    0,
                    startZ + r * resourceMesh.bounds.extents.z * 2);
                Instantiate(resourcePrefab, pos, resourcePrefab.transform.rotation);
            }
        }
    }
}
