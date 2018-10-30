using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject canvas;

    private bool isPaused = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        if(isPaused)
        {
            canvas.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            canvas.SetActive(true);
            Time.timeScale = 0f;
        }
        isPaused = !isPaused;
    }

    public void Resume()
    {
        Pause();
    }

    public void ReturnTitle()
    {
        Time.timeScale = 1f;
        SoundManager.instance.PlayMusic(true);
        SceneManager.LoadScene(0);
    }
}
