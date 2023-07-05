using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class data_SaveLoad : MonoBehaviour
{
    public static data_SaveLoad instance;

    [SerializeField] AllData leaderboardList;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadData();
    }
    public void SaveData(ScoreData scoreData)
    {
        string folderPath = "Assets/saveData";
        string fileName = "ScoreSaveData.json";
        string filePath = Path.Combine(folderPath, fileName);


        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        leaderboardList.AddToList(scoreData); 

        string _data = JsonUtility.ToJson(leaderboardList);
        File.WriteAllText(filePath, _data);

        Debug.Log("SAVE");
    }

    public void LoadData()
    {
        string folderPath = "Assets/saveData";
        string fileName = "ScoreSaveData.json";
        string filePath = Path.Combine(folderPath, fileName);


        if (File.Exists(filePath))
        {
            string _data = File.ReadAllText(filePath);
            AllData allData = JsonUtility.FromJson<AllData>(_data);
            leaderboardList = allData;
            Debug.Log("Load");
        }
        else
        {
            Debug.LogWarning("File not found");
        }

    }

    public static bool CheckForSaveFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "ScoreData.json");
        return System.IO.File.Exists(path);
    }

    public string GetScoreData()
    {
        LoadData();

        string data = "";

        if(leaderboardList != null)
        {
            foreach(var player in leaderboardList.allScoreData)
            {
                data += player.username + " : " + player.score.ToString() + "| Level: " + player.levelScore + "\n";
            }
        }

        return data;
    }
}
