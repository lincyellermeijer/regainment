using UnityEngine;
using System.Collections;

public class MainPlayer : PlayerController
{

    internal bool isFlipped = false;


    public override void BetterJump()
    {
        if (!isFlipped)
        {
            // Calculate fallMultiplier when player is falling down from jump
            if (rb2d.velocity.y < 0)
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            // Calculate lowJump (if you hold jump button you will jump higher)
            else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
        else if (isFlipped)
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
        }

        base.BetterJump();
    }


}