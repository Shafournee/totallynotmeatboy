using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelectPreview : MonoBehaviour {

    [SerializeField] GameObject PreviewBox;
    [SerializeField] GameObject imageBox;
    [SerializeField] GameObject timeText;

    int levelIndex;
    Sprite levelImage;
    Level levelTimeLoad;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetLevelInfo(int index, Sprite image, Level levelTimeLoader)
    {
        levelIndex = index;
        levelImage = image;
        levelTimeLoad = levelTimeLoader;
        GameManager.instance.checkLevelTime = levelTimeLoad;
        GameManager.instance.currentScene = index;
        DisplayInfo();
    }

    void DisplayInfo()
    {
        if(!PreviewBox.activeInHierarchy)
        {
            PreviewBox.SetActive(true);
        }
        imageBox.GetComponent<Image>().sprite = levelImage;
        // If the time is equal to our test time, then the time is set to N/A
        if(PlayerPrefs.GetFloat(levelTimeLoad.ToString()) == 100000f)
        {
            timeText.GetComponent<Text>().text = "N/A";
        }
        else
        {
            timeText.GetComponent<Text>().text = GameManager.instance.gameObject.GetComponent<DataManager>().GetLevelTime(levelTimeLoad).ToString("F2");
        }
        
    }

    public void Play()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
