using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleScreen : MonoBehaviour {

    [SerializeField] GameObject sausageBoy;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject bestTime;
    [SerializeField] GameObject selectArrow;
    [SerializeField] List<GameObject> buttons;
    [SerializeField] GameObject FirstButton;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject creditsScreen;
    [SerializeField] GameObject backFromCreditButton;

    // ------ Options Menu Game Objects ----- //

    // Buttons
    [SerializeField] GameObject resetTimeButton;
    [SerializeField] GameObject musicVolumeButton;
    [SerializeField] GameObject effectVolumeButton;
    [SerializeField] GameObject resetPopupNo;
    [SerializeField] GameObject musicVolumeRaiseButton;
    [SerializeField] GameObject effectVolumeRaiseButton;
    [SerializeField] GameObject backButton;

    // Text
    [SerializeField] GameObject musicVolumeText;
    [SerializeField] GameObject effectVolumeText;

    // Popup Menus
    [SerializeField] GameObject resetTimesPopup;
    [SerializeField] GameObject musicVolumePopup;
    [SerializeField] GameObject effectVolumePopup;

    // Holds the current popupmenu
    GameObject currentPopupMenu;

    int MusicVolume = 5;
    int EffectVolume = 5;
    bool onOptionsScreen;

    // Use this for initialization
    void Start () {
        StartCoroutine(Running());
        ReturnToTitleScreen();
        if(GameManager.instance != null)
        {
            DisplayTimeText();
            GameManager.instance.levelLoadedFromLevelSelect = false;
            // Reset the timer to zero when they return to the menu
            GameManager.instance.totalTime = 0f;
            // Set the level index back to 2 for when they restart the game
            GameManager.instance.currentScene = 2;
        }

        // TODO save volume in player prefs

        EffectVolume = GameManager.instance.GetComponent<DataManager>().GetVolume("effect");
        MusicVolume = GameManager.instance.GetComponent<DataManager>().GetVolume("music");
        effectVolumeText.GetComponent<Text>().text = EffectVolume.ToString();
        musicVolumeText.GetComponent<Text>().text = MusicVolume.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        sausageBoy.transform.Rotate(Vector3.forward * Time.deltaTime * 5f);
        SelectionArrow();
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            if (titleScreen.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(FirstButton);
            }
            else if (optionsScreen.activeInHierarchy && currentPopupMenu == null)
            {
                EventSystem.current.SetSelectedGameObject(resetTimeButton);
            }
            else if (currentPopupMenu == resetTimesPopup)
            {
                EventSystem.current.SetSelectedGameObject(resetPopupNo);
            }
            else if (currentPopupMenu == musicVolumePopup)
            {
                EventSystem.current.SetSelectedGameObject(musicVolumeRaiseButton);
            }
            else if (currentPopupMenu == effectVolumePopup)
            {
                EventSystem.current.SetSelectedGameObject(effectVolumeRaiseButton);
            }
        }
        

        if (currentPopupMenu != null && Input.GetButtonDown("Cancel"))
        {
            closePopup();
        }
    }

    IEnumerator Running()
    {
        while(true)
        {
            for (int i = 0; i < 3; i++)
            {
                sausageBoy.GetComponent<Image>().sprite = sprites[i];


                yield return new WaitForSeconds(.1f);
            }
        }

    }

    public void MoveToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("level1");
    }

    //Function used for displaying the time
    void DisplayTimeText()
    {
        GameManager.instance.gameObject.GetComponent<DataManager>().LoadTimes();
        if(PlayerPrefs.GetFloat("fullGameTime") == 100000f)
        {
            bestTime.GetComponent<Text>().text = "N/A";
        }
        else
        {
            bestTime.GetComponent<Text>().text = GameManager.instance.gameObject.GetComponent<DataManager>().GetFastestPlayerTime().ToString("F2");
        }
        
    }

    void SelectionArrow()
    {

        if(EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>() != null && !onOptionsScreen)
        {
            Vector2 pos = new Vector2(EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.x - 350f, EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.y - 30f);
            selectArrow.GetComponent<RectTransform>().anchoredPosition = pos;
        }
        else if(EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>() != null && onOptionsScreen)
        {
            Vector2 pos = new Vector2(EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.x + 350f, EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.y - 30f);
            selectArrow.GetComponent<RectTransform>().anchoredPosition = pos;
        }

    }

    public void ReturnToOptionScreen()
    {
        selectArrow.GetComponent<RectTransform>().rotation = new Quaternion(0f, 180f, 0f, 0f);
        EventSystem.current.SetSelectedGameObject(resetTimeButton);
        titleScreen.SetActive(false);
        optionsScreen.SetActive(true);
        onOptionsScreen = true;
    }

    public void ReturnToCreditsScreen()
    {
        selectArrow.GetComponent<RectTransform>().rotation = new Quaternion(0f, 0f, 0f, 0f);
        EventSystem.current.SetSelectedGameObject(backFromCreditButton);
        titleScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    // ------------------------ Options Screen Code ----------------------- //

    public void ReturnToTitleScreen()
    {
        selectArrow.GetComponent<RectTransform>().rotation = new Quaternion(0f, 0f, 0f, 0f);
        optionsScreen.SetActive(false);
        creditsScreen.SetActive(false);
        titleScreen.SetActive(true);
        onOptionsScreen = false;
        EventSystem.current.SetSelectedGameObject(FirstButton);
    }

    public void DisplayResetTimesScreen()
    {
        currentPopupMenu = resetTimesPopup;
        showPopup();
        EventSystem.current.SetSelectedGameObject(resetPopupNo);
    }

    public void DisplayMusicScreen()
    {
        currentPopupMenu = musicVolumePopup;
        showPopup();
        EventSystem.current.SetSelectedGameObject(musicVolumeRaiseButton);
    }

    public void DisplayEffectsScreen()
    {
        currentPopupMenu = effectVolumePopup;
        showPopup();
        EventSystem.current.SetSelectedGameObject(effectVolumeRaiseButton);
    }

    void showPopup()
    {
        // Disable buttons in background
        backButton.SetActive(false);
        resetTimeButton.SetActive(false);
        musicVolumeButton.SetActive(false);
        effectVolumeButton.SetActive(false);

        currentPopupMenu.SetActive(true);
    }

    public void closePopup()
    {
        // Enable buttons in background
        backButton.SetActive(true);
        resetTimeButton.SetActive(true);
        musicVolumeButton.SetActive(true);
        effectVolumeButton.SetActive(true);


        if (currentPopupMenu == resetTimesPopup)
        {
            EventSystem.current.SetSelectedGameObject(resetTimeButton);
        }
        else if(currentPopupMenu == musicVolumeButton)
        {
            EventSystem.current.SetSelectedGameObject(musicVolumeButton);
        }
        else if (currentPopupMenu == musicVolumeButton)
        {
            EventSystem.current.SetSelectedGameObject(effectVolumeButton);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(resetTimeButton);
        }

        currentPopupMenu.SetActive(false);
        currentPopupMenu = null;
    }

    public void RaiseSoundEffects()
    {
        EffectVolume++;
        if(EffectVolume > 9)
        {
            EffectVolume = 0;
        }
        GameManager.instance.GetComponent<DataManager>().submitNewVolume("effect", EffectVolume);
        effectVolumeText.GetComponent<Text>().text = EffectVolume.ToString();
        GameManager.instance.EffectAudio.volume = (float)EffectVolume/9;
    }

    public void LowerSoundEffects()
    {
        EffectVolume--;
        if (EffectVolume < 0)
        {
            EffectVolume = 9;
        }
        GameManager.instance.GetComponent<DataManager>().submitNewVolume("effect", EffectVolume);
        effectVolumeText.GetComponent<Text>().text = EffectVolume.ToString();
        GameManager.instance.EffectAudio.volume = (float)EffectVolume / 9;
    }

    public void RaiseMusic()
    {
        MusicVolume++;
        if (MusicVolume > 9)
        {
            MusicVolume = 0;
        }
        GameManager.instance.GetComponent<DataManager>().submitNewVolume("music", MusicVolume);
        musicVolumeText.GetComponent<Text>().text = MusicVolume.ToString();
        GameManager.instance.MusicAudio.volume = (float)MusicVolume / 9;
    }

    public void LowerMusic()
    {
        MusicVolume--;
        if (MusicVolume < 0)
        {
            MusicVolume = 9;
        }
        GameManager.instance.GetComponent<DataManager>().submitNewVolume("music", MusicVolume);
        musicVolumeText.GetComponent<Text>().text = MusicVolume.ToString();
        GameManager.instance.MusicAudio.volume = (float)MusicVolume / 9;
    }


    // A test button used for resetting times
    public void ResetTimes()
    {
        PlayerPrefs.SetFloat("fullGameTime", 100000f);
        PlayerPrefs.SetFloat("Level1", 100000f);
        PlayerPrefs.SetFloat("Level2", 100000f);
        PlayerPrefs.SetFloat("Level3", 100000f);
        PlayerPrefs.SetFloat("Level4", 100000f);
        PlayerPrefs.SetFloat("Level5", 100000f);
        PlayerPrefs.SetFloat("Level6", 100000f);
        PlayerPrefs.SetFloat("Level7", 100000f);
        PlayerPrefs.SetFloat("Level8", 100000f);
        PlayerPrefs.SetFloat("Level9", 100000f);
        PlayerPrefs.SetFloat("Level10", 100000f);
    }
}
