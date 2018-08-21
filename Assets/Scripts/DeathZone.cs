using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Player>() != null)
        {
            GameManager.instance.GetComponent<GameManager>().playerDeathAnimation(collider.gameObject.transform.position);
            Destroy(collider.gameObject);
            
        }
    }
}
