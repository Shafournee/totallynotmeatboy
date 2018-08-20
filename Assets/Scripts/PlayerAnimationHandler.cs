using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates { idle, running, wallsliding, jumping }

public class PlayerAnimationHandler : MonoBehaviour {

    // TODO FIGURE OUT WHY ANIMATIONS BUG OUT SOMETIMES

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

    //TODO, Figure out why when jumping the animation sometimes jumps between the running and jumping anim
    //This isn't game breaking, but annoying to see.

    public void Animator()
    {
        if (currentState == PlayerStates.wallsliding)
            StartCoroutine(SlidingDownWall());
        else if (currentState == PlayerStates.jumping)
            playerSpriteRenderer.sprite = sprites[4];
        else if (currentState == PlayerStates.running && !runningCoroutine)
            StartCoroutine(Running());
        else if (currentState == PlayerStates.idle)
            playerSpriteRenderer.sprite = sprites[0];
        
    }


    IEnumerator SlidingDownWall()
    {
        while(currentState == PlayerStates.wallsliding)
        {
            playerSpriteRenderer.sprite = sprites[5];
            yield return null;
        }

    }

    IEnumerator Running()
    {

        runningCoroutine = true;
        while(currentState == PlayerStates.running)
        {
            for (int i = 1; i < 4; i++)
            {
                playerSpriteRenderer.sprite = sprites[i];

                // If they stop running break out of the coroutine
                if (currentState != PlayerStates.running)
                {
                    runningCoroutine = false;
                    yield break;
                }


                yield return new WaitForSeconds(.1f);
            }
        }


        runningCoroutine = false;
    }
}
