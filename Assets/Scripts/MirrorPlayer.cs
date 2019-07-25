using UnityEngine;
using System.Collections;

public class MirrorPlayer : PlayerController
{

    // Reverse gravity for mirrored player
    private void Start()
    {
        rb2d.gravityScale *= -1;
        jumpForce *= -1;
    }


}