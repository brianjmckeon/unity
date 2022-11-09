using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmyard : MonoBehaviour
{
    public GameObject resourcePrefab;
    private GameObject ground;

    // The number of resource tile rows and columns
    private int rows, columns;

    // The 2D array of resource tiles
    private List<List<GameObject>> resources = new List<List<GameObject>>();

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
        rows = (int)(groundMesh.bounds.extents.z / resourceMesh.bounds.extents.z);
        columns = (int)(groundMesh.bounds.extents.x / resourceMesh.bounds.extents.x);

        //Debug.Log($"Ground: z: {groundMesh.bounds.extents.z} x: {groundMesh.bounds.extents.x}");
        Debug.Log($"Resource: z: {resourceMesh.bounds.extents.z} x: {resourceMesh.bounds.extents.x}");
        Debug.Log($"Ground {groundMesh.bounds.min.z} {groundMesh.bounds.max.z}");

        float startX = groundMesh.bounds.min.x + resourceMesh.bounds.extents.x;
        float startZ = groundMesh.bounds.min.z + resourceMesh.bounds.extents.z;

        // Loop over rows and columns creating resource tiles and store them for later in our resources array.
        for (int r = 0; r < rows; r++)
        {
            List<GameObject> rowList = new List<GameObject>();

            for (int c = 0; c < columns; c++)
            {
                // Calculate new position for this resource and instantiate a prefab
                var pos = new Vector3(
                    startX + c * resourceMesh.bounds.extents.x * 2,
                    0,
                    startZ + r * resourceMesh.bounds.extents.z * 2);
                GameObject tile = Instantiate(resourcePrefab, pos, resourcePrefab.transform.rotation);

                rowList.Add(tile);
            }

            resources.Add(rowList);
        }

        debugResourcesArray();
    }

    private void debugResourcesArray()
    {
        Debug.Log("Resources Array:");

        int totalTiles = 0;

        for (int r = 0; r < resources.Count; r++)
        {
            var rowList = resources[r];
            totalTiles += rowList.Count;

            string rowString = "";
            for (int c = 0; c < rowList.Count; c++)
            {
                rowString += $"[{c}, {r}: {rowList[c].ToString()}]    ";
            }
            Debug.Log(rowString);
        }

        Debug.Log($"Total: {totalTiles}");
    }
}
