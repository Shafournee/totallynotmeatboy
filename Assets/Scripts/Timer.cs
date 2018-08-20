using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float time = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Keep track of time, and output the string
        time += Time.deltaTime;
        GetComponent<Text>().text = time.ToString("F2");
	}
}
