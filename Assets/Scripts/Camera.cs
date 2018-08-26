using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject player;
    [SerializeField] float biggestYValue;
    [SerializeField] float smallestYValue;
    [SerializeField] float biggestXValue;
    [SerializeField] float smallestXValue;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        }

	}
}
