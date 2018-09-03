using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArm : MonoBehaviour {

    public bool spearThrown;
    float topRot = -35f;
    float botRot = -120f;
    [SerializeField] GameObject pivot;
    [SerializeField] bool backArm;

	// Use this for initialization
	void Start () {
        if(backArm)
        {
            StartCoroutine(RotateBackArm());
        }
        else
        {
            StartCoroutine(RotateArm());
        }
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    IEnumerator RotateArm()
    {
        while(!spearThrown)
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
        StartCoroutine(GetNewSpear());
    }

    IEnumerator GetNewSpear()
    {
        for (int i = 0; i < 150; i++)
        {
            transform.RotateAround(pivot.transform.position, Vector3.forward, 60f * Time.deltaTime);
            yield return null;
        }
        //Show the spear in his hand again
        transform.GetComponentInParent<Boss>().ShowSpears();
        for (int i = 0; i < 150; i++)
        {
            transform.RotateAround(pivot.transform.position, Vector3.back, 60f * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(transform.GetComponentInParent<Boss>().ThrowSpears());
        spearThrown = false;
        StartCoroutine(RotateArm());
    }

    IEnumerator RotateBackArm()
    {
        while (true)
        {
            for (int i = 0; i < 180; i++)
            {
                transform.RotateAround(pivot.transform.position, Vector3.back, 30f * Time.deltaTime);
                yield return null;
            }
            for (int i = 0; i < 180; i++)
            {
                transform.RotateAround(pivot.transform.position, Vector3.forward, 30f * Time.deltaTime);
                yield return null;
            }
        }
    }
}
