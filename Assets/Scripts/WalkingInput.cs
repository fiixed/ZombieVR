using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(ControllerEvents))]
public class WalkingInput : MonoBehaviour {

    private ControllerEvents controllerEvents;

    private Vector3 walkingVector = Vector3.zero;

    public float speed = 1.0f;
    public Transform player;
    public Transform hmd;

    void Awake() {
        controllerEvents = GetComponent<ControllerEvents>();
    }

    void OnEnable() {
        controllerEvents.TouchpadAxisChanged += HandleTouchpadAxisChanged;
    }

    void OnDisable() {
        controllerEvents.TouchpadAxisChanged -= HandleTouchpadAxisChanged;
    }

    private void HandleTouchpadAxisChanged(object sender, ControllerEvents.ControllerInteractionEventArgs e) {
        walkingVector.x = e.touchpadAxis.x;
        walkingVector.z = e.touchpadAxis.y;
    }

    void FixedUpdate() {
        if (controllerEvents.touchpadTouched) {
            Vector3 forwardBackward = hmd.forward * walkingVector.z * speed * Time.deltaTime;
            Vector3 strafe = hmd.right * walkingVector.x * speed * Time.deltaTime;
            float playerY = player.position.y;
            player.position += (forwardBackward + strafe);
            player.position = new Vector3(player.position.x, playerY, player.position.z);
        }
    }
}
