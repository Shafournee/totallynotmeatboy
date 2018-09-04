using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject player;
    [SerializeField] float biggestYValue;
    [SerializeField] float smallestYValue;
    [SerializeField] float biggestXValue;
    [SerializeField] float smallestXValue;
    float trueYPos;
    float trueXPos;
    public bool finalLevel;
    public bool finalCutscene;
    bool CoroutineRunning;

    float fraction = 0;

    Vector3 startPos;
    Vector3 endPos;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if(player != null)
        {
            if(finalLevel)
            {
                transform.position = new Vector3(player.transform.position.x + 4f, player.transform.position.y + 3f, -10);
            }
            else if(finalCutscene)
            {
                startPos = transform.position;
                // Need to set this to the eventual final position, because this thing is broken for some reason lol
                endPos = new Vector3(138.4f, transform.position.y, -10);
                if (!CoroutineRunning)
                {
                    CoroutineRunning = true;
                    StartCoroutine(FinalScene());
                }


            }
            else
            {
                boundCamera();
            }

        }

	}

    void boundCamera()
    {
        if(player.transform.position.y > biggestYValue)
        {
            trueYPos = biggestYValue;
        }
        else if (player.transform.position.y < smallestYValue)
        {
            trueYPos = smallestYValue;
        }
        else
        {
            trueYPos = player.transform.position.y;
        }
        if(player.transform.position.x > biggestXValue)
        {
            trueXPos = biggestXValue;
        }
        else if (player.transform.position.x < smallestXValue)
        {
            trueXPos = smallestXValue;
        }
        else
        {
            trueXPos = player.transform.position.x;
        }
        transform.position = new Vector3(trueXPos, trueYPos, -10);
    }

    IEnumerator FinalScene()
    {
        while(true)
        {

            if (fraction < 1)
            {
                fraction += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, fraction);
            }

            yield return null;
        }
        CoroutineRunning = false;
    }
}
