using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour {

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Player") {
            audioSource.Play();
            LevelHandler.instance.LoadEnding();
        }
    }
}
