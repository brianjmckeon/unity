using UnityEngine;

public class MyCounter : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        gameManager.AddPoint();
        Destroy(other.gameObject);
    }
}
