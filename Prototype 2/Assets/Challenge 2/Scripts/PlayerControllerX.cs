using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float delay = 0.5f;
    private float lastPressed = 0;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if ((Time.fixedTime - lastPressed) >= delay)
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                lastPressed = Time.fixedTime;
            }
        }
    }
}
