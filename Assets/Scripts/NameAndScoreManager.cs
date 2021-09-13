using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NameAndScoreManager : MonoBehaviour
{
    public static NameAndScoreManager Instance;

    public string playerName;
    public string bestPlayer;
    public int bestScore;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);

        LoadNameAndBestScore();
    }

    
    [System.Serializable]

    public class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    public void SaveNameAndBestScore()
    {
        SaveData data = new SaveData();

        data.bestPlayerName = bestPlayer;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadNameAndBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayer = data.bestPlayerName;
            bestScore = data.bestScore;
        }
    }
}
