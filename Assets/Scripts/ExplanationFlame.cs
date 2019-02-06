using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationFlame : MonoBehaviour {

    public GameObject explanation;

    private void Start() {
        explanation.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            explanation.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Player") {
            explanation.SetActive(false);
        }
    }
}
