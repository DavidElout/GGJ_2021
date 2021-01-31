using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void ToggleMenu(bool enable)
    {
        gameObject.SetActive(enable);
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        ToggleMenu(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
