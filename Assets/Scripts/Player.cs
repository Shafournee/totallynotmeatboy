using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection { right, left, none };

public class Player : MonoBehaviour {

    KeyCode right = KeyCode.D;
    KeyCode left = KeyCode.A;
    KeyCode space = KeyCode.Space;
    KeyCode down = KeyCode.S;

    MoveDirection move;
    public MoveDirection animDirection;

    Rigidbody2D rigidBody;

    PlayerAnimationHandler anim;

    [SerializeField] float speed = 10f;
    [SerializeField] float speedCap = 15f;
    [SerializeField] float jumpSpeed = 1000f;

    bool isJumping;

    bool isHoldingDown;

    bool slidingDownWall;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimationHandler>();
        anim.currentState = PlayerStates.idle;
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        WallJump();
        print(slidingDownWall);
	}

    private void FixedUpdate()
    {
        if(move == MoveDirection.left)
        {
            rigidBody.AddForce(new Vector2(-speed, 0f));
        }
        else if(move == MoveDirection.right)
        {
            rigidBody.AddForce(new Vector2(speed, 0f));
        }
        else if(move == MoveDirection.none)
        {
            if (rigidBody.velocity.x < 7f && rigidBody.velocity.x > -7f && PlayerCanJump())
            {
                rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);

            }
            else
            {
                rigidBody.AddForce(new Vector2(-rigidBody.velocity.x * 5f, 0f));
            }
        }

        if(isHoldingDown)
        {
            rigidBody.AddForce(new Vector2(0f, -20f));
        }

        if(slidingDownWall)
        {
            rigidBody.AddForce(new Vector2(0f, 8f));
        }

        rigidBody.AddForce(new Vector2(0f, -2f));
    }

    void WallJump()
    {
        Vector2 playerRight = new Vector2(GetComponent<BoxCollider2D>().bounds.max.x + .01f, transform.position.y);
        Vector2 playerLeft = new Vector2(GetComponent<BoxCollider2D>().bounds.min.x - .01f, transform.position.y);
        if(Physics2D.Raycast(playerRight, Vector2.right, .05f))
        {
            if(!PlayerCanJump() && Input.GetKey(right))
            {
                if(rigidBody.velocity.y < 0)
                {
                    slidingDownWall = true;
                    anim.currentState = PlayerStates.wallsliding;
                }
                if (Input.GetKeyDown(space))
                {
                    StartCoroutine(WallJumping(-600f));
                    anim.currentState = PlayerStates.jumping;
                }
            }
            else
            {
                slidingDownWall = false;
            }
        }
        else if(Physics2D.Raycast(playerLeft, Vector2.left, .05f))
        {
            if (!PlayerCanJump() && Input.GetKey(left))
            {
                if (rigidBody.velocity.y < 0)
                {
                    slidingDownWall = true;
                    anim.currentState = PlayerStates.wallsliding;
                }
                if (Input.GetKeyDown(space))
                {
                    StartCoroutine(WallJumping(600f));
                    anim.currentState = PlayerStates.jumping;
                }
            }
            else
            {
                slidingDownWall = false;
            }
        }
        else if (!PlayerCanJump())
        {
            slidingDownWall = false;
            anim.currentState = PlayerStates.jumping;
        }
        else
        {
            slidingDownWall = false;
        }
    }

    void Movement()
    {
        if(Input.GetKey(right) && rigidBody.velocity.x < speedCap)
        {
            move = MoveDirection.right;
            animDirection = MoveDirection.right;
            if(PlayerCanJump())
            {
                anim.currentState = PlayerStates.running;
            }
        }
        else if (Input.GetKey(left) && rigidBody.velocity.x > -speedCap)
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

        if(Input.GetKeyDown(space) && PlayerCanJump())
        {
            StartCoroutine(Jumping());
            anim.currentState = PlayerStates.jumping;
        }

        if(Input.GetKeyDown(down))
        {
            isHoldingDown = true;
        }
        else if(Input.GetKeyUp(down))
        {
            isHoldingDown = false;
        }
    }

    bool PlayerCanJump()
    {
        Vector2 playerBottom = new Vector2(0f, GetComponent<BoxCollider2D>().bounds.min.y);
        return Physics2D.Raycast(playerBottom, Vector2.down, .05f);
    }

    IEnumerator Jumping()
    {
        yield return new WaitForFixedUpdate();
        rigidBody.AddForce(new Vector2(0f, jumpSpeed));
    }

    IEnumerator WallJumping(float wallJumpVertSpeed)
    {
        slidingDownWall = false;
        yield return new WaitForFixedUpdate();
        rigidBody.velocity = new Vector2(0f, 0f);
        rigidBody.AddForce(new Vector2(wallJumpVertSpeed, jumpSpeed));
    }
}
