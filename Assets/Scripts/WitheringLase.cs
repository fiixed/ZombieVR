using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// A holy laser weapon used to eradicate zombies
/// This class implements event listeners for Laser Pointer and Controller Events
/// </summary>

[RequireComponent(typeof(ControllerEvents))]
[RequireComponent(typeof(LaserPointer))]
public class WitheringLase : MonoBehaviour {

    private LaserPointer laserPointer;
    private ControllerEvents controllerEvents;

    private Color laserPointerDefaultColor;

    private float damagePerFrame = .25f;
    private bool dealingDamage = false;

    private EnemyHealth enemyTarget;

    // Unity lifecycle method
    void Awake() {
        laserPointer = GetComponent<LaserPointer>();
        controllerEvents = GetComponent<ControllerEvents>();

        laserPointerDefaultColor = laserPointer.color;
    }

    // Unity lifecycle method
    void OnEnable() {
        controllerEvents.TriggerPressed += HandleTriggerPressed;
        controllerEvents.TriggerReleased += HandleTriggerReleased;
    }

    // Unity lifecycle method
    void OnDisable() {
        controllerEvents.TriggerPressed -= HandleTriggerPressed;
        controllerEvents.TriggerReleased -= HandleTriggerReleased;
    }

    // Unity lifecycle method
    void Update() {
        if (dealingDamage) {
            enemyTarget.TakeDamage(damagePerFrame);
        }
    }

    // Event Handler
    private void HandleTriggerPressed(object sender, ControllerEvents.ControllerInteractionEventArgs e) {
        laserPointer.enabled = true;
        laserPointer.pointerModel.GetComponent<MeshRenderer>().material.color = laserPointerDefaultColor;
        laserPointer.PointerIn += HandlePointerIn;
        laserPointer.PointerOut += HandlePointerOut;
    }


    private void HandleTriggerReleased(object sender, ControllerEvents.ControllerInteractionEventArgs e) {
        laserPointer.enabled = false;
        laserPointer.PointerIn -= HandlePointerIn;
        laserPointer.PointerOut -= HandlePointerOut;
    }

    private void HandlePointerIn(object sender, LaserPointer.PointerEventArgs e) {
        laserPointer.pointerModel.GetComponent<MeshRenderer>().material.color = Color.red;
        enemyTarget = e.target.gameObject.GetComponent<EnemyHealth>();
        if (enemyTarget) {
            dealingDamage = true;
        }
    }

    private void HandlePointerOut(object sender, LaserPointer.PointerEventArgs e) {
        laserPointer.pointerModel.GetComponent<MeshRenderer>().material.color = laserPointerDefaultColor;
        enemyTarget = null;
        dealingDamage = false;
    }

    


}
