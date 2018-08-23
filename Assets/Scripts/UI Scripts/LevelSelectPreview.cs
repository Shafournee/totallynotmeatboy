using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void DisplayInfo()
    {
        if(!PreviewBox.activeInHierarchy)
        {
            PreviewBox.SetActive(true);
        }
        imageBox.GetComponent<Image>().sprite = levelImage;
        timeText.GetComponent<Text>().text = GameManager.instance.gameObject.GetComponent<DataManager>().GetLevelTime(levelTimeLoad).ToString("F2");
    }

    public void SetLevelInfo(int index, Sprite image, Level levelTimeLoader)
    {
        levelIndex = index;
        levelImage = image;
        levelTimeLoad = levelTimeLoader;
        GameManager.instance.checkLevelTime = levelTimeLoad;
        DisplayInfo();
    }

    public void Play()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
