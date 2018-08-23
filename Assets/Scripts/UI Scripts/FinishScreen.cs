using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreen : MonoBehaviour {

    [SerializeField] GameObject finishText;

	// Use this for initialization
	void Start () {
        if(GameManager.instance != null)
        {
            finishText.GetComponent<Text>().text = GameManager.instance.totalTime.ToString("F2");
            GameManager.instance.gameObject.GetComponent<DataManager>().SubmitNewTime(GameManager.instance.totalTime, Level.Finish);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
