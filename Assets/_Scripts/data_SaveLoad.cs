using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class data_SaveLoad : MonoBehaviour
{
    public static void SaveData()
    {
        string path = Path.Combine(Application.persistentDataPath, "ScoreData.json");
        string data = JsonUtility.ToJson(ScoreData.instance.score);

        File.WriteAllText(path, data);

        Debug.Log("SAVE");
    }

    public static AllData LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "ScoreData.json");
        string data = File.ReadAllText(path);

        Debug.Log("Load");

        return JsonUtility.FromJson<AllData>(data);
    }

    public static bool CheckForSaveFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "ScoreData.json");
        return System.IO.File.Exists(path);
    }
}
