using UnityEngine;
using System.Collections;

public class MissileControl : MonoBehaviour {
	public GameObject target;
	public float speed = 2f;
	public ParticleSystem explosionEffect;
	EnemyHealthManager enemyHealth;

	void Start() {
		enemyHealth = target.GetComponent<EnemyHealthManager> ();
	}

	// Update is called once per frame
	void Update () {
		transform.LookAt (target.transform.position);
		transform.position = Vector3.MoveTowards (transform.position, target.transform.position, Time.deltaTime * speed);

		if (Vector3.Distance (target.transform.position, transform.position) < 0.1f) {
			Instantiate (explosionEffect, transform.position, Quaternion.identity);
			enemyHealth.ReduceHealth (30f);
			Destroy (gameObject);
		}
	}
}
