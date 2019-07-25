using UnityEngine;
using System.Collections;

public class MirrorPlayer : PlayerController
{
    // Reverse gravity for mirrored player
    private void Start()
    {
        rb2d.gravityScale *= -1;
        jumpVelocity *= -1;
    }

    public override void FlipGravity()
    {
        // Reverse gravity
        transform.Rotate(new Vector3(180f, 0f, 0f));
        rb2d.gravityScale *= -1;
        jumpVelocity *= -1;

        base.FlipGravity();
    }


    public override void BetterJump()
    {
        // Calculate fallMultiplier when player is falling down from jump
        if (rb2d.velocity.y > 0)
        {
            rb2d.velocity -= Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        // Calculate lowJump (if you hold jump button you will jump higher)
        else if (rb2d.velocity.y < 0 && !Input.GetButton("Jump"))
        {
            rb2d.velocity -= Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        base.BetterJump();
    }

}