using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

    public string nextLevel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
        {
            print(nextLevel.ToString());
        }
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Player>() != null)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
