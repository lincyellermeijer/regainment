using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite flippedSprite;

    internal bool isFlipped = false;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleSwitch() {
        isFlipped = !isFlipped;

        audioSource.Play();

        if (isFlipped) {
            spriteRenderer.sprite = flippedSprite;
        } else {
            spriteRenderer.sprite = normalSprite;
        }
    }
}
