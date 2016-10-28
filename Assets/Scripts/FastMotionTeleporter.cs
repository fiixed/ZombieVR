using UnityEngine;
using System.Collections;
using System;

public class FastMotionTeleporter : MonoBehaviour {

    public LaserPointer laserPointer;
    public ControllerEvents controllerEvents;
    private Transform target;
    private RaycastHit hitInfo;
    public Transform cameraRig;

    private bool teleporting = false;

    [SerializeField]
    [Range(1f, 200f)]
    private float fastMotionTeleportSpeed = 10f;

    public void OnEnable() {
        laserPointer.PointerOn += HandlePointerOn;
        controllerEvents.TriggerReleased += HandleTriggerReleased;
    }

    public void OnDisable() {
        laserPointer.PointerOn -= HandlePointerOn;
        controllerEvents.TriggerReleased -= HandleTriggerReleased;
    }

    private void HandleTriggerReleased(object sender, ControllerEvents.ControllerInteractionEventArgs e) {
        if (!teleporting && target.GetComponent<Terrain>()) {
            teleporting = true;
            var terrainHeight = Terrain.activeTerrain.SampleHeight(hitInfo.point);
            float y = (terrainHeight > hitInfo.point.y) ? hitInfo.point.y : terrainHeight;
            Vector3 destPosition = new Vector3(hitInfo.point.x, y, hitInfo.point.z);
            StartCoroutine(FastMotionTeleporterCoroutine(cameraRig.position, destPosition, fastMotionTeleportSpeed));
        }
    }

    private IEnumerator FastMotionTeleporterCoroutine(Vector3 position, Vector3 destPosition, float speed) {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(position, destPosition);
        float distCovered;
        float fracJourney;

        while (cameraRig.position != destPosition) {
            distCovered = (Time.time - startTime) * speed;
            fracJourney = distCovered / journeyLength;
            cameraRig.position = Vector3.Lerp(position, destPosition, fracJourney);
            yield return null;
        }
        teleporting = false;
        yield return null;
    }

    private void HandlePointerOn(object sender, LaserPointer.PointerEventArgs e) {
        target = e.target;
        hitInfo = e.hitInfo;
    }
}
