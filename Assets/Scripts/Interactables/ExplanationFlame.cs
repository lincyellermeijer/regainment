using UnityEngine;


public class ExplanationFlame : MonoBehaviour
{

    // Load explanation object in inspector
    [SerializeField] private GameObject explanation;


    // Don't show on start
    private void Start()
    {
        explanation.SetActive(false);
    }


    // Show when player enters trigger
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            explanation.SetActive(true);
        }
    }


    // Hide when player exits trigger
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            explanation.SetActive(false);
        }
    }


}