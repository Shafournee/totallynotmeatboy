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

    public Level checkLevelTime;

    // Tracks the total time of the player
    public float totalTime;

    [SerializeField] GameObject playerSpawn;
    [SerializeField] GameObject spawnParticle;
    [SerializeField] GameObject deathParticle;
    public List<string> sceneNames;
    int currentScene = 2;

    // This will tell us if a level was loaded from level select. If so, load back to level select, otherwise load to next level
    public bool levelLoadedFromLevelSelect;

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

    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.P))
        {
            print(checkLevelTime);
        }
	}


    public void FinishLevel()
    {
        timer.GetComponent<Timer>().timerActive = false;
        totalTime = timer.GetComponent<Timer>().time;

    }

    // Add one to the current scene and load the next level
    public void LevelFinished()
    {
        
        if(!levelLoadedFromLevelSelect)
        {
            checkLevelTime = (Level)System.Enum.Parse(typeof(Level), "Level" + (currentScene - 1).ToString());
            GetComponent<DataManager>().SubmitNewTime(timer.GetComponent<Timer>().levelTime, checkLevelTime);
            currentScene++;
            SceneManager.LoadScene(currentScene);
        }
        else
        {
            GetComponent<DataManager>().SubmitNewTime(timer.GetComponent<Timer>().time, checkLevelTime);
            SceneManager.LoadScene("LevelSelect");
        }
    }


    public void playerDeathAnimation(Vector2 pos)
    {
        if(!levelLoadedFromLevelSelect)
        {
            //When the player dies pause the timer, but also add it to the total time
            timer.GetComponent<Timer>().timerActive = false;
            totalTime = timer.GetComponent<Timer>().time;
        }
        GameObject newDeathParticle = Instantiate(deathParticle, pos, Quaternion.identity);
        newDeathParticle.GetComponent<ParticleSystem>().Play();
        StartCoroutine(ReloadLevel());
    }

    // If the scene needs to be reloaded, then reload the current scene
    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(currentScene);
    }

    public void StartLevel()
    {
        StartCoroutine(LevelStart());
    }

    IEnumerator LevelStart()
    {
        // When the level starts find the game objects we need to find
        player = GameObject.FindGameObjectWithTag("Player");
        timer = GameObject.FindGameObjectWithTag("Timer");
        // Store the starting pos of the player
        Vector3 pos = new Vector2(player.transform.position.x, player.transform.position.y);
        //Disable the player to start the spawn animation
        player.SetActive(false);
        GameObject newPlayerSpawn = Instantiate(playerSpawn, new Vector2(pos.x, pos.y - 2f), Quaternion.identity);
        GameObject newSpawnParticle = Instantiate(spawnParticle, new Vector2(pos.x, pos.y - 2f), Quaternion.identity);
        // Set the spawn particle to be the child of the player spawn so it moves with it
        newSpawnParticle.transform.parent = newPlayerSpawn.transform;
        while (newPlayerSpawn.transform.position != pos)
        {
            // The step size is equal to speed times frame time.
            float step = Time.deltaTime;

            // Move our position a step closer to the target.
            newPlayerSpawn.transform.position = Vector2.MoveTowards(newPlayerSpawn.transform.position, pos, step);
            yield return null;
        }
        Destroy(newPlayerSpawn);
        // Reactive the player, and set the timer to active
        player.SetActive(true);
        timer.GetComponent<Timer>().timerActive = true;
    }
}
