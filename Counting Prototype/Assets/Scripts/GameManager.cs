using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPoint()
    {
        score += 1;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
