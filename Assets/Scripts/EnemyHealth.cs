using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;
using System;

[RequireComponent(typeof(VRInteractiveItem))]
public class EnemyHealth : MonoBehaviour {

    public GameObject deathEffect;
    [SerializeField]
    [Range(25f, 200f)]
    private float health = 100f;
    private VRInteractiveItem vrInteractiveItem;
    private bool takingDamage = false;
    private float damagePerFrame = .25f;


	// Use this for initialization
	void Awake () {
        vrInteractiveItem = GetComponent<VRInteractiveItem>();
	}

    void OnEnable() {
        vrInteractiveItem.OnOver += HandleOver;
        vrInteractiveItem.OnOut += HandleOut;
    }

    void OnDisable() {
        vrInteractiveItem.OnOver -= HandleOver;
        vrInteractiveItem.OnOut -= HandleOut;
    }

    private void HandleOver() {
        takingDamage = true;
    }

    private void HandleOut() {
        takingDamage = false;
    } 

    // Update is called once per frame
    void Update () {
        if (takingDamage) {
            TakeDamage(damagePerFrame);
        }
        if (health <= 0) {
            Die();
            
        }
	}

    private void Die() {
        vrInteractiveItem.Out();
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage) {
        health -= damage;
    }
}
