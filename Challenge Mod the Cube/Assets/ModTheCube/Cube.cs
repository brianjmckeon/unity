using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    private float degrees;
    private bool expanding = true;
    private float scale = 1f;
    private float lowestScale = 1.0f;
    private float highestScale = 10f;
    private float scaleFactor = 20f;

    private Color expandColor = new Color(1f, 1f, 1f);
    private Color shrinkColor = new Color(0f, 0f, 0f);

    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * scale;

        Material material = Renderer.material;
        material.color = expandColor;

        degrees = Random.Range(45, 90);
    }

    void Update()
    {
        transform.Rotate(degrees * Time.deltaTime, 0.0f, 0.0f);
        transform.localScale = Vector3.one * scale;

        if (expanding)
        {
            // Increase scale until upper limit reached
            scale += Time.deltaTime * scaleFactor;
            if (scale > highestScale)
            {
                expanding = !expanding;
                Renderer.material.color = shrinkColor;
            }
        }
        else
        {
            // Decrease scale until lower limit reached
            scale -= Time.deltaTime * scaleFactor;
            if (scale < lowestScale)
            {
                expanding = !expanding;
                Renderer.material.color = expandColor;
            }
        }
    }
}
