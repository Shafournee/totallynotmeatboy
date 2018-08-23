using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

    [SerializeField] GameObject sausageBoy;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject bestTime;

	// Use this for initialization
	void Start () {
        StartCoroutine(Running());
        if(GameManager.instance != null)
        {
            GameManager.instance.gameObject.GetComponent<DataManager>().LoadTimes();
            bestTime.GetComponent<Text>().text = GameManager.instance.gameObject.GetComponent<DataManager>().GetFastestPlayerTime().ToString("F2");
            
        }

        // Tell the Game Manager they aren't on Level Select
	}
	
	// Update is called once per frame
	void Update () {
        sausageBoy.transform.Rotate(Vector3.forward * Time.deltaTime * 5f);
	}

    IEnumerator Running()
    {
        while(true)
        {
            for (int i = 0; i < 3; i++)
            {
                sausageBoy.GetComponent<Image>().sprite = sprites[i];


                yield return new WaitForSeconds(.1f);
            }
        }

    }

    public void MoveToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }

}
