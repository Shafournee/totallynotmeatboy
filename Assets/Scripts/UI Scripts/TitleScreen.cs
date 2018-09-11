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
    [SerializeField] GameObject resetTime;
    [SerializeField] GameObject MusicVolumeText;
    [SerializeField] GameObject EffectVolumeText;
    int MusicVolume = 9;
    int EffectVolume = 9;
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
        EffectVolumeText.GetComponent<Text>().text = EffectVolume.ToString();
        MusicVolumeText.GetComponent<Text>().text = MusicVolume.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        sausageBoy.transform.Rotate(Vector3.forward * Time.deltaTime * 5f);
        SelectionArrow();
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(FirstButton);
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

    public void ReturnToTitleScreen()
    {
        selectArrow.GetComponent<RectTransform>().rotation = new Quaternion(0f, 0f, 0f, 0f);
        optionsScreen.SetActive(false);
        titleScreen.SetActive(true);
        onOptionsScreen = false;
    }

    public void ReturnToOptionScreen()
    {
        selectArrow.GetComponent<RectTransform>().rotation = new Quaternion(0f, 180f, 0f, 0f);
        EventSystem.current.SetSelectedGameObject(resetTime);
        titleScreen.SetActive(false);
        optionsScreen.SetActive(true);
        onOptionsScreen = true;
    }

    public void AdjustSoundEffects()
    {
        EffectVolume++;
        if(EffectVolume > 9)
        {
            EffectVolume = 0;
        }
        EffectVolumeText.GetComponent<Text>().text = EffectVolume.ToString();
        GameManager.instance.EffectAudio.volume = (float)EffectVolume/9;
    }

    public void AdjustMusic()
    {
        MusicVolume++;
        if (MusicVolume > 9)
        {
            MusicVolume = 0;
        }
        MusicVolumeText.GetComponent<Text>().text = MusicVolume.ToString();
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
