using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * Time.deltaTime * 600f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        transform.GetComponentInParent<Turret>().SpawnParticleCall(gameObject.transform.position);
        Destroy(gameObject);
    }

}
