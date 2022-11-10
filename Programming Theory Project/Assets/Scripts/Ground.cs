using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all ground types
public abstract class Ground : MonoBehaviour
{
    // The food each type of ground produces, if any
    [SerializeField]
    protected GameObject foodPrefab;

    // The food this tile currently has, if any
    private GameObject food;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Create some food
        if (food == null)
        {
            if (Random.Range(0, 100) > 70)
            {
                food = produceFood();
            }
        }
    }

    // Instantiate the appropriate food and return it
    private GameObject produceFood()
    {
        GameObject newFood = Instantiate(foodPrefab);
        newFood.transform.position = gameObject.transform.position;
        return newFood;
    }
}
