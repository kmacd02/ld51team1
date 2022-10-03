using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject pauseMenu;

    private GameObject[] spawners;

    private void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("Spawner"); // different ingredient spawners in midground
    }

    public void navButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit the application.");
    }
    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        foreach (GameObject spawner in spawners) // disable spawners to avoid dragging
        {
            spawner.SetActive(false);
        }
    }
    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        foreach (GameObject spawner in spawners) // disable spawners to avoid dragging
        {
            spawner.SetActive(true);
        }
    }

    public void openPanel()
    {
        if (panel != null)
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }


    }
}