using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

    [SerializeField] List<Sprite> sprites;

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
            collider.gameObject.SetActive(false);
            GameManager.instance.FinishLevel();
            StartCoroutine(GoalAnimation());
        }
    }

    IEnumerator GoalAnimation()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[i];
            yield return new WaitForSeconds(.1f);
        }
        GameManager.instance.LevelFinished();

    }
}
