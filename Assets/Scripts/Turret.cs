using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [SerializeField] GameObject projectile;

    [SerializeField] enum direction { right, left, down, up };

    [SerializeField] direction facingDirection;

    [SerializeField] float shootingSpeed = 3f;

    [SerializeField] float projectileSpeed = 5f;

    [SerializeField] GameObject particles;

    Vector3 pos;
    Vector2 velocity;

	// Use this for initialization
	void Start () {

        if(facingDirection == direction.down)
        {
            pos = new Vector3(transform.position.x, transform.position.y - 1, 0);
            velocity = new Vector2(0f, -projectileSpeed);
        }
        else if (facingDirection == direction.up)
        {
            pos = new Vector3(transform.position.x, transform.position.y + 1, 0);
            velocity = new Vector2(0f, projectileSpeed);
        }
        else if (facingDirection == direction.left)
        {
            pos = new Vector3(transform.position.x - 1, transform.position.y, 0);
            velocity = new Vector2(-projectileSpeed, 0f);
        }
        else if (facingDirection == direction.right)
        {
            pos = new Vector3(transform.position.x + 1, transform.position.y, 0);
            velocity = new Vector2(projectileSpeed, 0f);
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

            newProjectile = Instantiate(projectile, pos, transform.rotation, gameObject.transform);
            newProjectile.GetComponent<Rigidbody2D>().velocity = velocity;
            yield return new WaitForSeconds(shootingSpeed);
        }

    }

    public IEnumerator SpawnParticles(Vector2 pos)
    {
        GameObject part = Instantiate(particles, pos, Quaternion.identity);
        yield return new WaitForSeconds(.01f);
        Destroy(part);
    }
}
