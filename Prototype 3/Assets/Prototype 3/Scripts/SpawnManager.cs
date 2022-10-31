using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs = new GameObject[10];
    private float startDelay = 2;
    private float repeatDelay = 2;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatDelay);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver) return;

        // Select a random obstacle
        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject newObstacle = obstaclePrefabs[index];

        // Ensure it's close to the ground
        BoxCollider collider = newObstacle.GetComponent<BoxCollider>();
        float startY = collider.size.y / 2f;

        // Sometimes multiple objects of the same type will be spawned
        int count = Random.Range(1, 3);

        for (int i = 0; i < count; i++)
        {
            // Multiple objects will be stacked on top of each other
            Vector3 spawnPos = new Vector3(30, startY * (i + 1), 0);
            Instantiate(newObstacle, spawnPos, newObstacle.transform.rotation);
        }
    }
}
