using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelect : MonoBehaviour
{

    [SerializeField] GameObject sausageBoy;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] GameObject selectArrow;
    [SerializeField] GameObject playButton;

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
        SelectionArrow();
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
        Vector2 startPos = new Vector3(-1000f, -372f);
        Vector2 endPos = new Vector3(1010f, -372f);
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

    void SelectionArrow()
    {

        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>() != null)
        {
            Vector2 pos = Vector2.zero;
            if (EventSystem.current.currentSelectedGameObject == playButton)
            {
                pos = new Vector2(280f, -142f);
                selectArrow.GetComponent<RectTransform>().rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            else if (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.x < -240f)
            {
                pos = new Vector2(EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.x - 250f, EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.y - 20f);
                selectArrow.GetComponent<RectTransform>().rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            else if(EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.x < 10f)
            {
                pos = new Vector2(EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.x + 250f, EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.y - 20f);
                selectArrow.GetComponent<RectTransform>().rotation = new Quaternion(0f, 0f, 180f, 0f);
            }
            
            else
            {
                pos = new Vector2(EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.x - 250f, EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.y - 20f);
                selectArrow.GetComponent<RectTransform>().rotation = new Quaternion(0f, 0f, 0f, 0f);
            }  
            selectArrow.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }

    public void ReturnToStartScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
