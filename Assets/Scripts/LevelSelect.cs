using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    [SerializeField] GameObject sausageBoy;
    [SerializeField] List<Sprite> sprites;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Running());
        StartCoroutine(RunBackAndForth());

        // Tell the game manager they're on level select
        if(GameManager.instance != null)
        {
            GameManager.instance.levelLoadedFromLevelSelect = true;
            GameManager.instance.gameObject.GetComponent<DataManager>().LoadTimes();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Running()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                sausageBoy.GetComponent<Image>().sprite = sprites[i];
                yield return new WaitForSeconds(.1f);
            }
        }

    }

    //Animation that runs the player back and forth
    IEnumerator RunBackAndForth()
    {
        Vector2 startPos = new Vector3(-1000f, -455f);
        Vector2 endPos = new Vector3(1010f, -455f);
        Vector2 target = endPos;
        while (true)
        {
            if (sausageBoy.GetComponent<RectTransform>().anchoredPosition == endPos)
            {
                target = startPos;
            }

            else if (sausageBoy.GetComponent<RectTransform>().anchoredPosition == startPos)
            {
                target = endPos;
            }

            if (target == endPos)
            {
                sausageBoy.GetComponent<RectTransform>().rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            else
            {
                sausageBoy.GetComponent<RectTransform>().rotation = new Quaternion(0f, 180f, 0f, 0f);
            }

            // The step size is equal to speed times frame time.
            float step = 700f * Time.deltaTime;

            // Move our position a step closer to the target.
            sausageBoy.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(sausageBoy.GetComponent<RectTransform>().anchoredPosition, target, step);
            yield return null;
        }
    }

    public void ReturnToStartScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
