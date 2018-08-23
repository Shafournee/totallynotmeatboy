using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection { right, left, none };

public class Player : MonoBehaviour {

    //TODO, make a buffer for wall jumping
    //TODO, add controller support

    KeyCode right = KeyCode.D;
    KeyCode left = KeyCode.A;
    KeyCode space = KeyCode.Space;
    KeyCode down = KeyCode.S;

    MoveDirection move;
    public MoveDirection animDirection;

    Rigidbody2D rigidBody;

    PlayerAnimationHandler anim;



    [SerializeField] float speed = 10f;
    [SerializeField] float airSpeed = 5f;
    [SerializeField] float trueSpeed;
    [SerializeField] float speedCap = 15f;
    [SerializeField] float jumpSpeed = 2000f;
    float vertJumpSpeed = 1000f;

    bool isJumping;

    bool slidingDownWall;

    bool CurrentlyJumping;

    bool CanWallJump;

    bool firstTimeOffWall;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimationHandler>();
        anim.currentState = PlayerStates.idle;
        GameManager.instance.StartLevel();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        WallJump();

	}

    private void FixedUpdate()
    {
        // Set the speed to be the airspeed or groundspeed depending if aired or grounded
        if(PlayerCanJump())
        {
            trueSpeed = speed;
        }
        else
        {
            trueSpeed = airSpeed;
        }

        // Cap the players movement speed at 20f
        if(rigidBody.velocity.y >= 20f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 20f);
        }

        // If the player goes from left to right immediately, set their velocity to zero first so they don't slide
        if(rigidBody.velocity.x > 0 && move == MoveDirection.left || rigidBody.velocity.x < 0 && move == MoveDirection.right)
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
        }
        // Moving left
        else if(move == MoveDirection.left && rigidBody.velocity.x > -speedCap)
        {
            rigidBody.AddForce(new Vector2(-trueSpeed, 0f));
        }
        // Moving right
        else if(move == MoveDirection.right && rigidBody.velocity.x < speedCap)
        {
            rigidBody.AddForce(new Vector2(trueSpeed, 0f));
        }
        // If the player is not moving, set their velocity to zero
        else if(move == MoveDirection.none)
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
        }

        if(slidingDownWall)
        {
            if(rigidBody.velocity.y > 5f)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 5f);
            }
            else if(rigidBody.velocity.y < -5f)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, -5f);
            }
            else
            {
                rigidBody.AddForce(new Vector2(0f, 5f));
            }
        }

        rigidBody.AddForce(new Vector2(0f, -4f));
    }

    void WallJump()
    {
        Vector2 playerRight = new Vector2(GetComponent<BoxCollider2D>().bounds.max.x + .01f, transform.position.y);
        Vector2 playerLeft = new Vector2(GetComponent<BoxCollider2D>().bounds.min.x - .01f, transform.position.y);
        if(Physics2D.Raycast(playerRight, Vector2.right, .01f))
        {

            if(!PlayerCanJump() && Input.GetKey(right))
            {
                // If you're on a wall, the player is sliding down the wall and can wall jump
                CanWallJump = true;
                slidingDownWall = true;
                // This is used to allow the player to have leniency with their jump. It gives them a few frames after leaving the wall to still do it 
                firstTimeOffWall = true;
                anim.currentState = PlayerStates.wallsliding;
                vertJumpSpeed = -1000f;

            }
        }
        else if(Physics2D.Raycast(playerLeft, Vector2.left, .01f))
        {
            if (!PlayerCanJump() && Input.GetKey(left))
            {
                CanWallJump = true;
                slidingDownWall = true;
                firstTimeOffWall = true;
                anim.currentState = PlayerStates.wallsliding;
                vertJumpSpeed = 1000f;
            }
        }
        else if (!PlayerCanJump())
        {
            // If it's the players first time off the wall they can wall jump
            if(firstTimeOffWall)
                StartCoroutine(WaitABitForWallJumps());
            slidingDownWall = false;
            anim.currentState = PlayerStates.jumping;
        }
        else
        {
            CanWallJump = false;
            slidingDownWall = false;
        }
    }

    void Movement()
    {
        if(Input.GetKeyDown(space) && (slidingDownWall || CanWallJump))
        {
            StartCoroutine(WallJumping());
            anim.currentState = PlayerStates.jumping;
        }
        else if(Input.GetKeyDown(space) && PlayerCanJump())
        {
            StartCoroutine(Jumping());
            anim.currentState = PlayerStates.jumping;
        }

        if(Input.GetKey(right))
        {
            move = MoveDirection.right;
            animDirection = MoveDirection.right;
            if(PlayerCanJump())
            {
                anim.currentState = PlayerStates.running;
            }
        }
        else if (Input.GetKey(left))
        {
            move = MoveDirection.left;
            animDirection = MoveDirection.left;
            if (PlayerCanJump())
            {
                anim.currentState = PlayerStates.running;
            }
        }
        else
        {
            move = MoveDirection.none;
            animDirection = MoveDirection.none;
            if (PlayerCanJump())
            {
                anim.currentState = PlayerStates.idle;
            }
        }



    }

    bool PlayerCanJump()
    {
        Vector2 playerBottomLeft = new Vector2(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.min.y - .01f);
        Vector2 playerBottomRight = new Vector2(GetComponent<BoxCollider2D>().bounds.max.x, GetComponent<BoxCollider2D>().bounds.min.y - .01f);
        if (Physics2D.Raycast(playerBottomLeft, Vector2.down, .01f) || Physics2D.Raycast(playerBottomRight, Vector2.down, .01f))
            return true;

        else
            return false;
    }

    IEnumerator Jumping()
    {
        // Keep track of the initial position, and if they're currently jumping
        // Wait until the next fixed update to apply force
        Vector2 initialJump = transform.position;
        yield return new WaitForFixedUpdate();
        rigidBody.AddForce(new Vector2(0f, jumpSpeed));
        CurrentlyJumping = true;

        while(CurrentlyJumping)
        {
            // If velocity in the Y direction becomes zero break out of the loop
            if (rigidBody.velocity.y < 0 || transform.position.y >= initialJump.y + 4f)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
                CurrentlyJumping = false;
            }

            // Otherwise, if they stop holding space set velocity to zero and break out
            else if(Input.GetKeyUp(space))
            {

                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
                CurrentlyJumping = false;
            }
            yield return null;
        }
        
        
    }

    IEnumerator WallJumping()
    {
        // Keep track of the initial position, and if they're currently jumping
        // Wait until the next fixed update to apply force
        Vector2 initialJump = transform.position;

        slidingDownWall = false;
        yield return new WaitForFixedUpdate();
        rigidBody.velocity = new Vector2(0f, 0f);
        StartCoroutine(VertJumpingForce());
        yield return new WaitForFixedUpdate();

        rigidBody.AddForce(new Vector2(0f, jumpSpeed));
        CurrentlyJumping = true;
        while (CurrentlyJumping)
        {
            // Otherwise, if they stop holding space set velocity to zero and break out
            if (Input.GetKeyUp(space))
            {

                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
                CurrentlyJumping = false;
            }
            else if (move == MoveDirection.right && rigidBody.velocity.x < 0 )
            {
                rigidBody.velocity = new Vector2(-20f, rigidBody.velocity.y);
            }
            else if(move == MoveDirection.left && rigidBody.velocity.x > 0)
            {
                rigidBody.velocity = new Vector2(20f, rigidBody.velocity.y);
            }

            // If velocity in the Y direction becomes zero break out of the loop
            else if (rigidBody.velocity.y < 0 || transform.position.y >= initialJump.y + 4f)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
                CurrentlyJumping = false;
            }

            
            yield return null;
        }
    }

    IEnumerator VertJumpingForce()
    {
        trueSpeed = 0;
        for (int i = 0; i < 10; i++)
        {
            rigidBody.AddForce(new Vector2(vertJumpSpeed/2, 0f));
            yield return new WaitForFixedUpdate();
            if (vertJumpSpeed > 0 && move == MoveDirection.right || vertJumpSpeed < 0 && move == MoveDirection.left || Input.GetKeyUp(space))
            {
                rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
                trueSpeed = airSpeed;
                yield break;
            }

        }
        rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
        trueSpeed = airSpeed;
    }

    IEnumerator WaitABitForWallJumps()
    {
        //If you're currently jumping you can't jump
        if(CurrentlyJumping)
        {
            CanWallJump = false;
        }
        else
        {
            CanWallJump = true;
        }
        firstTimeOffWall = false;

        yield return new WaitForSeconds(.2f);
        CanWallJump = false;
    }
}
