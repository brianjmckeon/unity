using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all ground types.
public abstract class Ground : MonoBehaviour
{
    // The food each type of ground produces.
    [SerializeField]
    protected GameObject foodPrefab;

    // The amount of food this tile has produced.
    private int foodProduced = 0;

    // Number of seconds until a new food is created.
    protected abstract int productionRate { get; }

    // The maximum amount of food that can be produced.
    protected abstract int productionLimit { get; }

    // Number of seconds to lay dormant after hitting the production limit.
    protected abstract int dormantPeriod { get; }


    void Start()
    {
        StartCoroutine(productionLoop());
    }

    IEnumerator productionLoop()
    {
        while (true)
        {
            if (foodProduced == productionLimit)
            {
                // Going dormant
                yield return new WaitForSeconds(dormantPeriod);
                foodProduced = 0;
            }
            else
            {
                // Still producing
                yield return new WaitForSeconds(productionRate);
                var newFood = produceFood();
                foodProduced++;
            }
        }
    }

    // Instantiate the appropriate food and return it
    private GameObject produceFood()
    {
        GameObject newFood = Instantiate(foodPrefab);
        var offset = new Vector3(Random.Range(2, 4), Random.Range(0.5f, 1), Random.Range(2, 4));
        newFood.transform.position = gameObject.transform.position + offset;
        return newFood;
    }
}
