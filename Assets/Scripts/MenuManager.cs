using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("TheGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
