using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject selectArrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TogglePauseMenu();
        SelectionArrow();
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

    void SelectionArrow()
    {

        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>() != null)
        {
            Vector2 pos = new Vector2(EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.x - 220f, EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.y - 15f);
            selectArrow.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
}
