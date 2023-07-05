using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] InputField nameInput;

    [SerializeField] Text score;
    [SerializeField] Text level;

    private void Awake()
    {
        score.text = ScoreData.instance.score.ToString();
        level.text = "Level: " + ScoreData.instance.levelScore;
    }
    public void MoveToScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
        Time.timeScale = 1f;

        if (SceneID == 0)
        {
            if (GameObject.FindWithTag("GameManager"))
            {
                Destroy(GameObject.FindWithTag("GameManager"));
            }

            if (GameObject.FindWithTag("Player"))
            {
                Destroy(GameObject.FindWithTag("Player"));
            }
        }
    }

    public void updateData()
    {
        ScoreData.instance.username = nameInput.text;

        data_SaveLoad.instance.SaveData(ScoreData.instance);
    }
}
