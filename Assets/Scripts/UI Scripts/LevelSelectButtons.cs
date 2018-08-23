using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButtons : MonoBehaviour {

    [SerializeField] int levelIndex;
    [SerializeField] Sprite levelImage;
    [SerializeField] Level levelTimeLoader;
    [SerializeField] GameObject canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowPreview()
    {
        canvas.GetComponent<LevelSelectPreview>().SetLevelInfo(levelIndex, levelImage, levelTimeLoader);
    }
}
