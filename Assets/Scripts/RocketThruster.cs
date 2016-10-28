using System;
using UnityEngine;

[RequireComponent(typeof(ControllerEvents))]
public class RocketThruster : MonoBehaviour {

    public Rigidbody rb;
    public float thrustMagnitude;

    private ControllerEvents controllerEvents;
    private bool touchPadPressed = false;
   

    void Awake() {
        controllerEvents = GetComponent<ControllerEvents>();
    }

    void OnEnable() {
        controllerEvents.TouchpadPressed += HandleTouchpadPressed;
        controllerEvents.TouchpadReleased += HandleTouchpadReleased;
    }

    void OnDisable() {
        controllerEvents.TouchpadPressed -= HandleTouchpadPressed;
        controllerEvents.TouchpadReleased -= HandleTouchpadReleased;
    }

    private void HandleTouchpadPressed(object sender, ControllerEvents.ControllerInteractionEventArgs e) {
        touchPadPressed = true;

    }

    private void HandleTouchpadReleased(object sender, ControllerEvents.ControllerInteractionEventArgs e) {
        touchPadPressed = false;
    }

    // FixedUpdate is called once per Physics step
    void FixedUpdate() {
        // Thrust flight code here
        if (touchPadPressed) {
            rb.AddForce(transform.rotation * Vector3.forward * thrustMagnitude);
        }
    }

    

    

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
