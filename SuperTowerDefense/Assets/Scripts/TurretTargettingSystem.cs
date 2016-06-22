using UnityEngine;
using System.Collections;

abstract public class TurretTargettingSystem : MonoBehaviour {
	protected ArrayList enemyGameObjects;
	protected GameObject currentTarget;
	protected EnemyHealthManager currentEnemyHealth;
	public int turretSpeed = 4;
	public float damagePerSecond = 40f;

	public enum TurretState { Disabled, Idle, LockingOn, Engaged }
	protected TurretState currentTurretState;

	// Use this for initialization
	public virtual void Start () {
		enemyGameObjects = new ArrayList();
		SetCurrentTurretState(TurretState.Idle);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTurretState == TurretState.Disabled) {
			// TODO - Work out disabled logic
		} else if (currentTurretState == TurretState.Idle) {
			CheckForEnemiesInRange ();
		} else if (currentTurretState == TurretState.LockingOn) {
			LockOn ();
		} else if (currentTurretState == TurretState.Engaged) {
			LookAtTarget ();
			MaybeFire ();
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Enemy") {
			// Debug.Log ("Enemy entered range. Adding to array");
			enemyGameObjects.Add (collider.gameObject);

			EnemyHealthManager ehm = collider.gameObject.GetComponent<EnemyHealthManager> ();
			ehm.OnEnemyDestroyedAction += EnemyDestroyed;
		}
	}


	void EnemyDestroyed(GameObject enemyGameObject) {
		if (currentTarget == enemyGameObject) {
			SetCurrentTurretState (TurretState.Idle);
		}

		enemyGameObjects.Remove (enemyGameObject);
	}

	void OnTriggerExit(Collider collider) {
		
		if (collider.gameObject.tag == "Enemy") {
			// Debug.Log ("Enemy went out of range. Removing from array");

			enemyGameObjects.Remove (collider.gameObject);

			EnemyHealthManager ehm = collider.gameObject.GetComponent<EnemyHealthManager> ();
			ehm.OnEnemyDestroyedAction -= EnemyDestroyed;

			if (currentTarget == collider.gameObject) {
				currentTarget = null;
				SetCurrentTurretState(TurretState.Idle);
			}
		}
	}

	void CheckForEnemiesInRange() {
		// Debug.Log ("Checking for enemies in Range");
		if (enemyGameObjects.Count > 0) {
			int nearestEnemyIndex = GetNearestEnemyIndex ();
			currentTarget = (GameObject)enemyGameObjects [nearestEnemyIndex];
			currentEnemyHealth = currentTarget.GetComponent<EnemyHealthManager> ();
			SetCurrentTurretState(TurretState.LockingOn);
		} else {
			// Nothing to do... Stay idle!
		}
	}

	int GetNearestEnemyIndex() {
		float nearestDistance = 9999f;
		int nearestEnemyIndex = 0;
		for (int i = 0; i < enemyGameObjects.Count; i++) {
			float distanceToObject = Vector3.Distance (transform.position, ((GameObject)enemyGameObjects [i]).transform.position);
			if (distanceToObject < nearestDistance) {
				nearestEnemyIndex = i;
				nearestDistance = distanceToObject;
			}
		}
		return nearestEnemyIndex;
	}

	private void LockOn() {
		// Debug.Log ("Locking On");
		Vector3 currentTargetPosition = currentTarget.transform.position;
		currentTargetPosition.y = transform.position.y; // This is to ensure the turret doesn't angle up or down.
		Vector3 targetDirection = currentTargetPosition - transform.position;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDirection, Time.deltaTime*turretSpeed, 0F);
		transform.rotation = Quaternion.LookRotation (newDir);

		float remainingRotation = Mathf.Abs (Quaternion.LookRotation (transform.forward).eulerAngles.y - Quaternion.LookRotation (targetDirection).eulerAngles.y);
		if (remainingRotation < 2.5) {
			SetCurrentTurretState(TurretState.Engaged);
		}
	}

	void LookAtTarget() {
			Vector3 targetPosition = currentTarget.transform.position;
			targetPosition.y = transform.position.y;
			transform.LookAt (targetPosition);
	}

	public void SetCurrentTurretState(TurretState state) {
		currentTurretState = state;

		if (currentTurretState == TurretState.Engaged) {
			EngagedTarget ();
		} else {
			DisengagedTarget ();
		}
	}

	abstract public void EngagedTarget();
	abstract public void DisengagedTarget();
	abstract public void MaybeFire();
}
