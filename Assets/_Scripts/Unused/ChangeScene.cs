using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MoveToScene(int SceneID)
    {
        //Use to load a different scene
        //Jumps between Menu, death screen and game screen
        SceneManager.LoadScene(SceneID);
    }
}
