using UnityEngine;
using System.Collections;
using System;

public class Interactable : MonoBehaviour {

    public bool targeted = false;

    private Color startColor;
    private Material material;

	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
        startColor = material.color;
	}
	
	// Update is called once per frame
	void Update () {
        if (targeted) {
            Target();
            targeted = false;
        } else {
            Untargeted();
        }
	}

    public void Untargeted() {
        material.color = startColor;
    }

    public void Target() {
        material.color = Color.green;
    }
}
