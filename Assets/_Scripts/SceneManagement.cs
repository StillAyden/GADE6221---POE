using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] Canvas canvasReference;

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MoveToScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowReferences()
    {
        canvasReference.gameObject.SetActive(true);
    }

    public void HideReferences()
    {
        canvasReference.gameObject.SetActive(false);
    }
}
