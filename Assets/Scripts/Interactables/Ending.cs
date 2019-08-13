using UnityEngine;


public class Ending : MonoBehaviour
{

    private bool playerEntered = false;
    private bool mirrorPlayerEntered = false;


    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            playerEntered = true;
        }
        if (target.tag == "MirrorPlayer")
        {
            mirrorPlayerEntered = true;
        }

        // When both players have triggered the exit, go to the next scene
        if (playerEntered && mirrorPlayerEntered)
        {
            LevelHandler.instance.LoadNextLevel();
        }
    }


}