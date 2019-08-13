using UnityEngine;


public class GravitySwitch : MonoBehaviour
{

    // Load sprites in inspector
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite flippedSprite;


    internal bool isFlipped = false;


    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }


    // When gravity switch is activated, flip player sprite and play audio (gravity is handled in PlayerController)
    public void ToggleSwitch()
    {
        isFlipped = !isFlipped;

        audioSource.Play();

        if (isFlipped)
        {
            spriteRenderer.sprite = flippedSprite;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
        }
    }


}