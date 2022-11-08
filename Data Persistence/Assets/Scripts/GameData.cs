using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData : MonoBehaviour
{
    public static GameData Instance = null;
    public string Name;

    private string highScoreName;
    private int highScore;
    private const string filePath = "/gamedata.json";

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

        // We only provide a default name for testing the main scene in isolation
        Name = "Fred";
    }

    public (string name, int score) GetHighScorer()
    {
        return (highScoreName, highScore);
    }

    public void SetHighScorer(int newHighScore)
    {
        highScoreName = Name;
        highScore = newHighScore;
    }

    private void LoadGameData()
    {
        string path = Application.persistentDataPath + filePath;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);
            highScoreName = data.highScoreName;
            highScore = data.highScore;
        }
        else
        {
            highScoreName = "Fred";
            highScore = 0;
        }
    }

    public void SaveGameData(int newHighScore)
    {
        GameData data = new GameData();
        data.highScoreName = highScoreName;
        data.highScore = newHighScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + filePath, json);
    }
}
