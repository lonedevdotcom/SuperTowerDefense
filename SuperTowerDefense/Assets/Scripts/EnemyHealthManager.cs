using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {
	public float maxHealth = 100f;
	public float currentHealth;
	public bool isAlive = true;
	public GameObject explosionPrefab; 

	public delegate void HealthChangedAction(float currentHealth, float maxHealth);
	public event HealthChangedAction OnHealthChangedAction;

	public delegate void EnemyDestroyed(GameObject enemyGameObject);
	public event EnemyDestroyed OnEnemyDestroyedAction;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0f) {
			isAlive = false;
			Instantiate (explosionPrefab, transform.position, Quaternion.identity);

			if (OnEnemyDestroyedAction != null) {
				OnEnemyDestroyedAction (gameObject);
			}

			Destroy (gameObject);
		}
	}

	public void ReduceHealth(float amount) {
		currentHealth -= amount;

		if (OnHealthChangedAction != null) {
			OnHealthChangedAction (currentHealth, maxHealth);
		}

	}
}
