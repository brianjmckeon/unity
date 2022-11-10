using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmyard : MonoBehaviour
{
    public List<GameObject> groundPrefabs = new List<GameObject>();
    public GameObject fencePrefab;
    private GameObject ground;

    // The number of resource tile rows and columns
    private int rows, columns;

    // The 2D array of resource tiles
    private List<List<GameObject>> resources = new List<List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.Find("Ground");
        //buildFarmyard();
        buildFence();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void buildFence()
    {
        var groundMesh = ground.GetComponent<MeshRenderer>();

        // We'll need to rotate the fence prefab to orient it correctly for each side of the yard.
        // So we use a reference object for this purpose.
        Quaternion quat = Quaternion.AngleAxis(90, Vector3.up);
        GameObject fence = Instantiate(fencePrefab, Vector3.zero, quat);
        var fenceMesh = fence.GetComponent<MeshRenderer>();

        //float startZ = groundMesh.bounds.min.z + fenceMesh.bounds.extents.z;

        // Build north and south walls at same time since the fence rotation is the same
        float northStartX = groundMesh.bounds.min.x;
        float southStartX = groundMesh.bounds.max.x;
        float startZ = groundMesh.bounds.min.z - fenceMesh.bounds.extents.z / 6;

        int count = (int)(groundMesh.bounds.extents.z / fenceMesh.bounds.extents.z);

        //Debug.Log(groundMesh + " " + fenceMesh + " " + fencePrefab);

        for (int i = 0; i <= count; i++)
        {
            Instantiate(
                fencePrefab,
                new Vector3(northStartX, 0, startZ + i * fenceMesh.bounds.extents.z * 2),
                quat);

            Instantiate(
                fencePrefab,
                new Vector3(southStartX, 0, startZ + i * fenceMesh.bounds.extents.z * 2),
                quat);
        }

        // Rotate the fence again and build the east and west walls
        quat = Quaternion.AngleAxis(90, Vector3.up);

        float northStartZ = groundMesh.bounds.min.z;
        float southStartZ = groundMesh.bounds.max.z;
        float startX = groundMesh.bounds.min.x - fenceMesh.bounds.extents.x / 6;

        for (int i = 0; i <= count; i++)
        {
            Instantiate(
                fencePrefab,
                new Vector3(startX + i * fenceMesh.bounds.extents.x * 2, 0, northStartZ),
                quat);

            Instantiate(
                fencePrefab,
                new Vector3(startX + i * fenceMesh.bounds.extents.x * 2, 0, southStartZ),
                quat);
        }

        // Destroy the fence instant we were using for reference
        Destroy(fence);
    }

    void buildFarmyard()
    {
        var groundMesh = ground.GetComponent<MeshRenderer>();
        // All ground prefabs should be the same size otherwise it will throw off the calculation below!
        var resourceMesh = groundPrefabs[0].GetComponent<MeshRenderer>();

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

                // Choose one of our ground types at random
                int i = Random.Range(0, groundPrefabs.Count);
                GameObject tile = Instantiate(groundPrefabs[i], pos, groundPrefabs[i].transform.rotation);

                //rowList.Add(tile);
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
