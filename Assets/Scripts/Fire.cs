using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(FlipSprite());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FlipSprite()
    {

        while(true)
        {
            float wait = Random.Range(.1f, .3f);
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            yield return new WaitForSeconds(wait);
        }
        
    }
}
