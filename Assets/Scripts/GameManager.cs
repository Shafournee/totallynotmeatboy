using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager instance = null;

    // Important Game Objects for the game manager to track
    GameObject player;
    GameObject timer;

    // Tracks the total time of the player
    float totalTime;

    [SerializeField] GameObject spawnParticle;
    [SerializeField] GameObject deathParticle;
    [SerializeField] List<string> sceneNames;
    int currentScene;

    private void Awake()
    {

        Application.targetFrameRate = 60;
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }
        // If instance already exists and it's not this:
        else if (instance != this)
        {
            // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = GameObject.FindGameObjectWithTag("Timer");
    }
	
	// Update is called once per frame
	void Update () {

	}

    // Add one to the current scene and load the next level
    public void FinishLevel()
    {
        totalTime += timer.GetComponent<Timer>().time;

        currentScene++;
        SceneManager.LoadScene(currentScene);
    }


    public void playerDeathAnimation(Vector2 pos)
    {
        GameObject newDeathParticle = Instantiate(deathParticle, pos, Quaternion.identity);
        newDeathParticle.GetComponent<ParticleSystem>().Play();
        StartCoroutine(RestartLevel());
    }

    // If the scene needs to be reloaded, then reload the current scene
    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(currentScene);
    }
}
