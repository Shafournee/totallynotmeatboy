using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates { idle, running, wallsliding, jumping }

public class PlayerAnimationHandler : MonoBehaviour {

    public List<Sprite> sprites;
    SpriteRenderer playerSpriteRenderer;
    public PlayerStates currentState;
    MoveDirection dir;
    bool runningCoroutine;

    // Use this for initialization
    void Start () {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Direction();
        Animator();

    }

    public void Direction()
    {
        dir = GetComponent<Player>().animDirection;
        if (dir == MoveDirection.left && currentState != PlayerStates.wallsliding)
        {
            if (playerSpriteRenderer.flipX == false)
            {
                playerSpriteRenderer.flipX = true;
            }
        }
        else if (dir == MoveDirection.right && currentState != PlayerStates.wallsliding)
        {
            if (playerSpriteRenderer.flipX == true)
            {
                playerSpriteRenderer.flipX = false;
            }
        }
        else if (dir == MoveDirection.left)
        {
            if (playerSpriteRenderer.flipX == true)
            {
                playerSpriteRenderer.flipX = false;
            }
        }

        else if (dir == MoveDirection.right)
        {
            if (playerSpriteRenderer.flipX == false)
            {
                playerSpriteRenderer.flipX = true;
            }
        }
    }

    public void Animator()
    {
        if (currentState == PlayerStates.idle)
            playerSpriteRenderer.sprite = sprites[0];
        else if (currentState == PlayerStates.running && !runningCoroutine)
            StartCoroutine(Running());
        else if (currentState == PlayerStates.wallsliding)
            playerSpriteRenderer.sprite = sprites[5];
        else if (currentState == PlayerStates.jumping)
            playerSpriteRenderer.sprite = sprites[4];
    }


    public void SlidingDownWall()
    {
        playerSpriteRenderer.sprite = sprites[5];
        playerSpriteRenderer.flipX = true;
    }

    public void Jumping()
    {
        playerSpriteRenderer.sprite = sprites[4];
    }

    IEnumerator Running()
    {

        runningCoroutine = true;

        for (int i = 1; i < 4; i++)
        {
            playerSpriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(.1f);
        }

        runningCoroutine = false;
    }
}
