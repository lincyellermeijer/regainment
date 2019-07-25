using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public float moveSpreed = 15f;
    public float maxSpeed = 7.5f;
    public float jumpVelocity = 7.5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public Transform groundCheck;

    internal Rigidbody2D rb2d;

    private bool facingRight = true;
    private bool isJumping = false;
    private bool isGrounded = false;

    private Animator anim;
    private int jumpHash;
    private int runStateHash;
    private float horizontalAxis;
    private bool isNearSwitch;


    public virtual void BetterJump()
    {
        // Method to calculate falling multiplier and add low jump
    }


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        // Check if player is grounded
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (LevelHandler.instance != null)
        {
            if (!LevelHandler.Fading)
            {
                PressJump();
                PressInteract();
                MoveHorizontal();
            }
            // When in load / fade freeze and reset movement of the player
            else
            {
                ResetMovement();
            }
        }
        else
        {
            PressJump();
            PressInteract();
            MoveHorizontal();
        }
    }


    private void ResetMovement()
    {
        horizontalAxis = 0f;
        rb2d.velocity = Vector2.zero;
        anim.SetFloat("Speed", 0f);
    }


    private void MoveHorizontal()
    {
        // Calculate velocity for movement
        horizontalAxis = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(moveSpreed * horizontalAxis, rb2d.velocity.y);
        SetAnimations(horizontalAxis);
    }


    private void PressInteract()
    {
        // Check if player pressed E for activating switch
        if (Input.GetButtonDown("Fire1") && isNearSwitch)
        {
            GameObject[] objs;
            objs = GameObject.FindGameObjectsWithTag("GravitySwitch");
            foreach (GameObject gravitySwitch in objs)
            {
                gravitySwitch.GetComponent<GravitySwitch>().ToggleSwitch();
            }
            FlipGravity();
        }
    }


    private void PressJump()
    {
        BetterJump();

        // Check if player is jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            rb2d.velocity = Vector2.up * jumpVelocity;
        }
    }


    private void FlipGravity()
    {
        GameObject player = GameObject.FindWithTag("Player");
        MainPlayer playerScript = player.GetComponent<MainPlayer>();
        
        GameObject mirrorPlayer = GameObject.FindWithTag("MirrorPlayer");
        MirrorPlayer mirrorPlayerScript = mirrorPlayer.GetComponent<MirrorPlayer>();

        // Reverse gravity
        ApplyGravity(player);
        ApplyGravity(mirrorPlayer);

        playerScript.isFlipped = playerScript.isFlipped == true ? false : true;
        playerScript.jumpVelocity *= -1;
        mirrorPlayerScript.isFlipped = mirrorPlayerScript.isFlipped == true ? false : true;
        mirrorPlayerScript.jumpVelocity *= -1;
    }


    private void ApplyGravity(GameObject thisPlayer)
    {
        thisPlayer.transform.Rotate(new Vector3(180f, 0f, 0f));
        thisPlayer.GetComponent<Rigidbody2D>().gravityScale *= -1;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "GravitySwitch")
        {
            isNearSwitch = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "GravitySwitch")
        {
            isNearSwitch = false;
        }
    }


    // Set the animations for moving, jumping and turning
    private void SetAnimations(float horizontalAxis)
    {
        anim.SetFloat("Speed", Mathf.Abs(horizontalAxis));

        if (isJumping)
        {
            anim.SetTrigger("Jump");
            isJumping = false;
        }

        if (horizontalAxis < 0 && facingRight || horizontalAxis > 0 && !facingRight)
        {
            if (isGrounded)
            {
                anim.SetTrigger("Turn");
            }
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

        }
    }


}