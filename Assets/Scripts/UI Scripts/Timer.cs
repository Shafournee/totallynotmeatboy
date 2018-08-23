using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float time = 0;
    public bool timerActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Keep track of time, and output the string
        GetComponent<Text>().text = time.ToString("F2");
        if (timerActive)
        {
            time += Time.deltaTime;
        }


    }

}
