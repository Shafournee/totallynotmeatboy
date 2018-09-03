using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject player;
    [SerializeField] float biggestYValue;
    [SerializeField] float smallestYValue;
    [SerializeField] float biggestXValue;
    [SerializeField] float smallestXValue;
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
                endPos = new Vector3(20.45f, transform.position.y, -10);
                if (!CoroutineRunning)
                {
                    CoroutineRunning = true;
                    StartCoroutine(FinalScene());
                }


            }
            else
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            }

        }

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
