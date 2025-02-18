using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameispause;
    public GameObject pausemenu;
    public GameObject panel;
    NoteUI note;
  

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (gameispause) {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Resume();
             }
            else
            {
                Pause();
            }
        }

        
        
    }
    public void Resume()
    {
        pausemenu.SetActive(false);
        panel.SetActive(false);
        Time.timeScale = 1.0f;
        gameispause = false;
        Cursor.lockState = CursorLockMode.Locked;

        

    }
    public void Pause()
    {
        pausemenu.SetActive(true);
        panel.SetActive(true);
        Time.timeScale = 0f;
        gameispause = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

   
}
