using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;
using UnityEngine.UI;
using System;

public class ReticleColor : MonoBehaviour {

    private VREyeRaycaster vrEyeRaycaster;
    [SerializeField]
    private Image reticleImage;
    private Color originalReticleColor;
    private VRInteractiveItem vrInteractiveItem;


	// Use this for initialization
	void Awake () {
        vrEyeRaycaster = Camera.main.GetComponent<VREyeRaycaster>();
        originalReticleColor = reticleImage.color;
	}

    void OnEnable() {
        vrEyeRaycaster.OnRaycasthit += HandleRaycastHit;
    }

    void OnDisable() {
        vrEyeRaycaster.OnRaycasthit -= HandleRaycastHit;
    }


    private void HandleRaycastHit(RaycastHit obj) {
        if (vrInteractiveItem) {
            return;
        }
        vrInteractiveItem = obj.collider.GetComponent<VRInteractiveItem>();
        if (vrInteractiveItem) {
            reticleImage.color = Color.red;
            vrInteractiveItem.OnOut += HandleOut;
        }
    }

    private void HandleOut() {
        reticleImage.color = originalReticleColor;
        vrInteractiveItem.OnOut -= HandleOut;
        vrInteractiveItem = null;
    }

}
