using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float leftBound = -15;
    private PlayerController playerControllerScript;
    private float speed = 20;
    private int speedFactor = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver) return;

        transform.Translate(Vector3.left * Time.deltaTime * speed * speedFactor);

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    public void ToggleSpeedFactor()
    {
        speedFactor = (speedFactor == 1) ? 2 : 1;
    }

    public int GetSpeedFactor()
    {
        return speedFactor;
    }
}
