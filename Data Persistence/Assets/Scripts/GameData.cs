using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    public string Name;

    private HighScoreData highScoreData;
    private const string filePath = "/highscore.json";

    [System.Serializable]
    class HighScoreData
    {
        public string name;
        public int score;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadGameData();
    }

    public (string name, int score) GetHighScorer()
    {
        return (highScoreData.name, highScoreData.score);
    }

    public void SetHighScorer(int newHighScore)
    {
        highScoreData.name = Name;
        highScoreData.score = newHighScore;
    }

    private void LoadGameData()
    {
        string path = Application.persistentDataPath + filePath;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            highScoreData = JsonUtility.FromJson<HighScoreData>(json);
        }
        else
        {
            highScoreData = new HighScoreData();
            highScoreData.name = "Fred";
            highScoreData.score = 0;
        }
    }

    public void SaveGameData()
    {
        string json = JsonUtility.ToJson(highScoreData);
        File.WriteAllText(Application.persistentDataPath + filePath, json);
    }
}
