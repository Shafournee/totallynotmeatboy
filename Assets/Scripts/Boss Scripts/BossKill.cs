using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossKill : MonoBehaviour {

    GameObject player;
    [SerializeField] GameObject playerStandIn;
    [SerializeField] GameObject EndScreen;
    [SerializeField] GameObject EndButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Player>() != null)
        {
            player = collider.gameObject;
        }
        if(collider.GetComponent<Boss>() != null)
        {
            StartCoroutine(BossKillScene(collider.gameObject));
        }
    }

    // This is the final level. Initiate final level stuff
    IEnumerator BossKillScene(GameObject Boss)
    {
        //Disable the player
        player.SetActive(false);
        // Make the boss unable to throw spears or move, and move the camera
        Boss.GetComponent<Boss>().canThrowSpears = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().finalLevel = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().finalCutscene = true;

        //Put the player standin where the player used to be
        GameObject newPlayer = Instantiate(playerStandIn, player.transform.position, Quaternion.identity);
        newPlayer.AddComponent<Rigidbody2D>();
        newPlayer.GetComponent<Rigidbody2D>().freezeRotation = true;
        newPlayer.AddComponent<BoxCollider2D>();
        GameManager.instance.FinishLevel();
        yield return new WaitForSeconds(2f);
        if (GameManager.instance.levelLoadedFromLevelSelect)
        {
            GameManager.instance.LevelFinished();
        }
        Boss.GetComponent<Rigidbody2D>().gravityScale = 1000f;
        yield return new WaitForSeconds(1f);
        EndScreen.SetActive(true);
        EndScreen.GetComponent<FinishScreen>().finishTime();
        EventSystem.current.SetSelectedGameObject(EndButton);
        // If the level was loaded from level select just go back to level select. Otherwise play the cutscene
    }

    IEnumerator EndingCutscene()
    {
        yield return null;
    }
}
