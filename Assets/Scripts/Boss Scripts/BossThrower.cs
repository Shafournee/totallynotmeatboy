using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrower : MonoBehaviour {

    [SerializeField] GameObject target;
    [SerializeField] float rotSpeed;

    Quaternion lookRot;
    Vector3 dir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(target != null)
        {
            // Find the vector pointing from our position to the target
            dir = (target.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * rotSpeed);
        }
	}
}
