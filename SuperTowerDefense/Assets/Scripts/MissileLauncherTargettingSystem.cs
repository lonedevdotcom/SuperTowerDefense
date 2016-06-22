using UnityEngine;
using System.Collections;

public class MissileLauncherTargettingSystem : TurretTargettingSystem {
	float lastFired;
	public float fireDelay = 4f;
	public GameObject missileGameObject;

	override public void Start() {
		lastFired = Time.time;
		base.Start ();
	}

	override public void MaybeFire() {
		if (Time.time > lastFired + fireDelay) {
			// Instantiate Missile
			Vector3 instantionPosition = transform.position + (transform.forward * 0.4f);
			GameObject missile = Instantiate(missileGameObject, instantionPosition, Quaternion.identity) as GameObject;
			MissileControl missileControl = missile.GetComponent<MissileControl> ();
			missileControl.target = currentTarget;
			lastFired = Time.time;
		}
	}

	override public void EngagedTarget() {
	}

	override public void DisengagedTarget() {
	}
}
