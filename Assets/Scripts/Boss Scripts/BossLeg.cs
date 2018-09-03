using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLeg : MonoBehaviour {

    [SerializeField] GameObject pivot;
    [SerializeField] bool backLeg;

    // Use this for initialization
    void Start () {
        if (backLeg)
        {
            StartCoroutine(RotateBackLeg());
        }
        else
        {
            StartCoroutine(RotateLeg());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator RotateLeg()
    {
        for (int i = 0; i < 105; i++)
        {
            transform.RotateAround(pivot.transform.position, Vector3.back, 30f * Time.deltaTime);
            yield return null;
        }
        for (int i = 0; i < 125; i++)
        {
            transform.RotateAround(pivot.transform.position, Vector3.forward, 30f * Time.deltaTime);
            yield return null;
        }
        while (true)
        {
            for (int i = 0; i < 135; i++)
            {
                transform.RotateAround(pivot.transform.position, Vector3.back, 30f * Time.deltaTime);
                yield return null;
            }
            for (int i = 0; i < 135; i++)
            {
                transform.RotateAround(pivot.transform.position, Vector3.forward, 30f * Time.deltaTime);
                yield return null;
            }
        }

    }

    IEnumerator RotateBackLeg()
    {
        for (int i = 0; i < 105; i++)
        {
            transform.RotateAround(pivot.transform.position, Vector3.forward, 30f * Time.deltaTime);
            yield return null;
        }
        for (int i = 0; i < 125; i++)
        {
            transform.RotateAround(pivot.transform.position, Vector3.back, 30f * Time.deltaTime);
            yield return null;
        }
        while (true)
        {
            for (int i = 0; i < 135; i++)
            {
                transform.RotateAround(pivot.transform.position, Vector3.forward, 30f * Time.deltaTime);
                yield return null;
            }
            for (int i = 0; i < 135; i++)
            {
                transform.RotateAround(pivot.transform.position, Vector3.back, 30f * Time.deltaTime);
                yield return null;
            }
        }
    }
}
