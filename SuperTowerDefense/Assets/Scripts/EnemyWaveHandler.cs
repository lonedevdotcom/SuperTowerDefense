using UnityEngine;
using System.Collections;

public class EnemyWaveHandler : MonoBehaviour {
	public Transform[] path;
	public EnemyWave[] enemyWaves;
	int currentWave = 0;
	float timeLastEnemyGameObjectSpawned;
	bool allWavesCompleted = false;
	public GameManager gameManager;

	// Use this for initialization
	void Start () {
		timeLastEnemyGameObjectSpawned = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (!allWavesCompleted) {
			float nextObjectDueDelay = enemyWaves [currentWave].delayBetweenEnemySpawns;
			if (Time.time - timeLastEnemyGameObjectSpawned > nextObjectDueDelay) {
				GameObject enemyGameGameObject = Instantiate (enemyWaves [currentWave].GetNextEnemy (), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
				EnemyPathRoute epr = enemyGameGameObject.AddComponent <EnemyPathRoute> () as EnemyPathRoute;
				epr.path = path;

				epr.OnReachedEndTarget += HandleReachedEndTargetAction;

				epr.moveSpeed = enemyWaves [currentWave].waveSpeed;
				timeLastEnemyGameObjectSpawned = Time.time;

				if (!enemyWaves [currentWave].ThereAreEnemiesRemaining ()) {
					Debug.Log ("Wave complete!!");
					currentWave++;

					if (currentWave == enemyWaves.Length) {
						Debug.Log ("All waves competed :-)");
						allWavesCompleted = true;
						Destroy (gameObject);
					}
				}
			}
		}
	}

	void HandleReachedEndTargetAction (GameObject go)
	{
		Debug.Log ("Reduce some health as enemy " + go + " has reached target!");	
		gameManager.SetHealth (gameManager.GetHealth () - 2);
	}
}
