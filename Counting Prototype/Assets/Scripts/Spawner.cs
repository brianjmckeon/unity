using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ballPrefab;
    private float xRange = 1.5f;
    private float yLowRange = 15;
    private float yHighRange = 20;
    private float lowPower = 15;
    private float highPower = 20;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("NewBall", 1, 0.5f);
    }

    void NewBall()
    {
        var ball = Instantiate(ballPrefab);

        var offset = new Vector3(0, Random.Range(2, 5), 0);
        ball.gameObject.transform.position = transform.position + offset;

        var rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(RandomThrow(), ForceMode.Impulse);
    }

    Vector3 RandomThrow()
    {
        return new Vector3(
            Random.Range(-xRange, xRange),
            Random.Range(yLowRange, yHighRange),
            Random.Range(lowPower, highPower)
        );
    }
}
