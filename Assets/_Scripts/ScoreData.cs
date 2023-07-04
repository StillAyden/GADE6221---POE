using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData : MonoBehaviour
{
    public static ScoreData instance;

    public int score = 0;
    public int levelScore = 0;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

[System.Serializable]
public class AllData
{
    public List<ScoreData> allScoreData;

    public void AddToList(ScoreData data)
    {
        allScoreData.Add(data);
    }

}
