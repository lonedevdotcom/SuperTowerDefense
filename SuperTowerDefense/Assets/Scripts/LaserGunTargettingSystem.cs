using UnityEngine;
using System.Collections;

public class LaserGunTargettingSystem : TurretTargettingSystem {
	LineRenderer lineRenderer;
	AudioSource laserSound;

	override public void Start() {
		// TODO (Issue 1) - Sort out the line renderer... It's a rubbish laser!
		lineRenderer = gameObject.GetComponent<LineRenderer> ();
		laserSound = gameObject.GetComponent<AudioSource> ();
		base.Start ();
	}

	override public void MaybeFire() {
		float distanceToTarget = Vector3.Distance (transform.position, currentTarget.transform.position);
		lineRenderer.SetPosition(1, new Vector3(0f, 0f, distanceToTarget + 0.6f));
		currentEnemyHealth.ReduceHealth (Time.deltaTime * damagePerSecond);
	}

	override public void EngagedTarget() {
		// Make Line Renderer 0 length... This stops it being really long initially when a new target is locked on.
		lineRenderer.SetPosition(1, Vector3.zero);
		lineRenderer.enabled = true;
		laserSound.enabled = true;
	}

	override public void DisengagedTarget() {
		lineRenderer.enabled = false;
		laserSound.enabled = false;
	}
}
