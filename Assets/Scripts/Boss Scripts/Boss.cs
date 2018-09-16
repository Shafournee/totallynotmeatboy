using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    [SerializeField] GameObject thrower;
    [SerializeField] GameObject arm;
    [SerializeField] GameObject spear;
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    public bool canThrowSpears;

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().finalLevel = true;
        canThrowSpears = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        Move();

    }

    void Move()
    {
        if (player != null && player.activeInHierarchy && canThrowSpears)
        {
            if (Vector3.Distance(new Vector3(player.transform.position.x, 0f), new Vector3(transform.position.x, 0f)) >= 7f)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(13f, 0f);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-12f, 0f);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void ShowSpears()
    {
        thrower.SetActive(true);
    }

    public IEnumerator ThrowSpears()
    {
        float waitTime = Random.Range(2f, 4f);
        yield return new WaitForSeconds(waitTime);

        if(!canThrowSpears)
        {
            yield break;
        }

        if(player != null)
        {
            GameObject newSpear = Instantiate(spear, thrower.transform.position, thrower.transform.rotation);
            Vector3 normalizedVector = (player.transform.position - thrower.transform.position).normalized;
            newSpear.GetComponent<Rigidbody2D>().velocity = normalizedVector * speed;
            thrower.SetActive(false);
            arm.GetComponent<BossArm>().spearThrown = true;
        }
    }

    public IEnumerator MoveForwards()
    {
        yield return null;
    }
}
