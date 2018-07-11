using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public GameObject projectile;

    public enum direction { right, left, down, up };

    public direction facingDirection;

    public float shootingSpeed = 3f;

    Vector3 pos;
    Vector2 velocity;

	// Use this for initialization
	void Start () {

        if(facingDirection == direction.down)
        {
            pos = new Vector3(transform.position.x, transform.position.y - 1, 0);
            velocity = new Vector2(0f, -5f);
        }
        else if (facingDirection == direction.up)
        {
            pos = new Vector3(transform.position.x, transform.position.y + 1, 0);
            velocity = new Vector2(0f, 5f);
        }
        else if (facingDirection == direction.left)
        {
            pos = new Vector3(transform.position.x - 1, transform.position.y, 0);
            velocity = new Vector2(-5f, 0f);
        }
        else if (facingDirection == direction.right)
        {
            pos = new Vector3(transform.position.x + 1, transform.position.y, 0);
            velocity = new Vector2(5f, 0f);
        }

        StartCoroutine(SpawnProjectile());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnProjectile()
    {
        while(true)
        {
            GameObject newProjectile;

            newProjectile = Instantiate(projectile, pos, transform.rotation);
            newProjectile.GetComponent<Rigidbody2D>().velocity = velocity;
            yield return new WaitForSeconds(shootingSpeed);
        }

    }
}
