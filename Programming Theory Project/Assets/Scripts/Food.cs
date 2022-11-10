using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for food.
public abstract class Food : MonoBehaviour
{
    // Number of seconds until a food spoils. 
    protected abstract int spoilageRate { get; }

    private void Awake()
    {
        StartCoroutine(lifeCycle());
    }

    IEnumerator lifeCycle()
    {
        yield return new WaitForSeconds(spoilageRate);
        Destroy(gameObject);
    }
}
