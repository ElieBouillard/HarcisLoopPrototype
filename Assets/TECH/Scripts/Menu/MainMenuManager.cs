using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string _mapScene = "";

    public void ChangeToMainScene()
    {
        SceneManager.LoadScene(_mapScene);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
