using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractTurret : MonoBehaviour {
	protected int turretLevel = 1; // Each increasing level improves lock-on speed, fire-rate, range etc.

	protected List<GameObject> enemiesInRange = new List<GameObject> ();

	public Transform target;
	protected enum TurretState { Disabled, Idle, LockingOn, Engaged }
	protected TurretState turretState;

	void Start() {
		turretState = TurretState.LockingOn;
	}

	// Update is called once per frame
	virtual protected void Update () {
		if (turretState == TurretState.LockingOn) {
			LockOn ();
		} else if (turretState == TurretState.Engaged) {
			RepositionTurret ();
			MaybeFire ();
		}
	}

	abstract protected void MaybeFire ();

	public void SetTurretLevel(int level) {
		turretLevel = level;
	}

	void LockOn() {
		if (target != null) {
			Vector3 targetDirection = target.position - transform.position;
			Vector3 newDir = Vector3.RotateTowards (transform.forward, new Vector3 (targetDirection.x, transform.position.y, targetDirection.z), Time.deltaTime*turretLevel, 0F);
			float remainingRotation = Mathf.Abs (Quaternion.LookRotation (transform.forward).eulerAngles.y - Quaternion.LookRotation (targetDirection).eulerAngles.y);
			// Debug.Log ("remainingRotation=" + remainingRotation);
			transform.rotation = Quaternion.LookRotation (newDir);
			if (remainingRotation < 5) {
				turretState = TurretState.Engaged;
			}
		} else {
			turretState = TurretState.Idle;
		}
	}

	void RepositionTurret() {
		if (target != null) {
			Vector3 targetDirection = target.position - transform.position;
			transform.rotation = Quaternion.LookRotation (new Vector3(targetDirection.x, transform.position.y, targetDirection.z));
		} else {
			turretState = TurretState.Idle;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy") {
			enemiesInRange.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other) {
		// I'm checking to see if it's an enemy because this might improve performance as it won't have to unnecessarily
		// try to remove non-enemies (eg turrets) from the list. 
		if (other.gameObject.tag == "Enemy") {
			enemiesInRange.Remove (other.gameObject); 
			// TODO - If the turrets current target is this one, we'll need to update the turret state.
		}
	}
}
