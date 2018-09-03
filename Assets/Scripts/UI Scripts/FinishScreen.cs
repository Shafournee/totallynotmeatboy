using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishScreen : MonoBehaviour {

    [SerializeField] GameObject finishText;

	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void finishTime()
    {
        if (GameManager.instance != null)
        {
            finishText.GetComponent<Text>().text = GameManager.instance.totalTime.ToString("F2") + " Seconds";
            GameManager.instance.gameObject.GetComponent<DataManager>().SubmitNewTime(GameManager.instance.totalTime, Level.Finish);
            GameManager.instance.totalTime = 0f;
        }
    }

    public void ReturnToMenu()
    {
        GameManager.instance.LevelFinished();
    }
}
