using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [SerializeField] GameObject pauseMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TogglePauseMenu();
	}

    private void TogglePauseMenu()
    {
        if(GameManager.instance.gameIsPaused)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }

    public void Unpause()
    {
        GameManager.instance.gameIsPaused = false;
    }

    public void RestartLevel()
    {
        GameManager.instance.RestartLevel();
        GameManager.instance.gameIsPaused = false;
    }

    public void LoadMenu()
    {
        GameManager.instance.gameIsPaused = false;
        if (GameManager.instance.levelLoadedFromLevelSelect)
        {
            SceneManager.LoadScene("LevelSelect");
        }
        else
        {
            SceneManager.LoadScene("TitleScreen");
        }

    }
}
