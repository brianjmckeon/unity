using UnityEngine;

public class Ball : MonoBehaviour
{
    private float xBound = 50;
    private float yBound = 30;
    private float zBound = 70;

    // Update is called once per frame
    void Update()
    {
        // Remove the ball if it's off the game grid
        if (transform.position.x < -xBound ||
            transform.position.x > xBound ||
            transform.position.y < -yBound ||
            transform.position.y > yBound ||
            transform.position.z < -zBound ||
            transform.position.z > zBound)
        {
            Destroy(gameObject);
        }
    }
}
