using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BladeDir { left, right };

public class Saw : MonoBehaviour {

    [SerializeField] GameObject sawBlade;
    [SerializeField] GameObject sawArm;
    [SerializeField] float speed;
    [SerializeField] BladeDir dir;
    Vector3 vec;

	// Use this for initialization
	void Start () {
		if(dir == BladeDir.left)
        {
            vec = Vector3.back;
        }
        else
        {
            vec = Vector3.forward;
        }
	}
	
	// Update is called once per frame
	void Update () {
        sawBlade.transform.RotateAround(transform.position, vec, speed * Time.deltaTime);
        sawArm.transform.RotateAround(transform.position, vec, speed * Time.deltaTime);
    }

}
