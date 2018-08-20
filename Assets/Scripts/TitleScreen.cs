using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {

    [SerializeField] GameObject sausageBoy;
    [SerializeField] GameObject sausageBoy2;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] GameObject titleScreen;

	// Use this for initialization
	void Start () {
        StartCoroutine(Running());
        StartCoroutine(RunBackAndForth());
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
                sausageBoy2.GetComponent<Image>().sprite = sprites[i];


                yield return new WaitForSeconds(.1f);
            }
        }

    }

    IEnumerator RunBackAndForth()
    {
        Vector2 startPos = new Vector3(-1000f, 631f);
        Vector2 endPos = new Vector3(1010f, 631f);
        Vector2 target = endPos;
        while(true)
        {
            if (sausageBoy2.GetComponent<RectTransform>().anchoredPosition == endPos)
            {
                target = startPos;
            }

            else if (sausageBoy2.GetComponent<RectTransform>().anchoredPosition == startPos)
            {
                target = endPos;
            }

            if(target == endPos)
            {
                sausageBoy2.GetComponent<RectTransform>().rotation = new Quaternion (0f, 0f, 0f, 0f);
            }
            else
            {
                sausageBoy2.GetComponent<RectTransform>().rotation = new Quaternion(0f, 180f, 0f, 0f);
            }

            // The step size is equal to speed times frame time.
            float step = 700f * Time.deltaTime;

            // Move our position a step closer to the target.
            sausageBoy2.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(sausageBoy2.GetComponent<RectTransform>().anchoredPosition, target, step);
            yield return null;
        }
    }

    public void MoveToLevelSelect()
    {
        titleScreen.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -1100f);
    }

    public void ReturnToStartScreen()
    {
        titleScreen.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
    }
}
