using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    // Important Game Objects for the game manager to track
    GameObject player;
    GameObject timer;

    // Tracks the total time of the player
    float totalTime;


    [SerializeField] List<string> sceneNames;
    int currentScene;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = GameObject.FindGameObjectWithTag("Timer");
    }
	
	// Update is called once per frame
	void Update () {

        // If the player dies reload the level
		if(player == null)
        {
            ReloadLevel();
        }
	}

    // Add one to the current scene and load the next level
    public void FinishLevel()
    {
        totalTime += timer.GetComponent<Timer>().time;

        currentScene++;
        SceneManager.LoadScene(currentScene);
    }

    // If the scene needs to be reloaded, then reload the current scene
    public void ReloadLevel()
    {
        SceneManager.LoadScene(currentScene);
    }
}
