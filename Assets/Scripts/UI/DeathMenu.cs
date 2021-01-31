using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public static DeathMenu Instance;

    public void ToggleMenu(bool enable)
    {
        gameObject.SetActive(enable);
    }

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(transform.parent.gameObject);
        DontDestroyOnLoad(transform.parent.gameObject);
        ToggleMenu(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        ToggleMenu(false);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
