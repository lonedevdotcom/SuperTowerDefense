using UnityEngine;
using System.Collections;

public class ParticleRemover : MonoBehaviour {
	ParticleSystem myParticleSystem;

	void Start() {
		myParticleSystem = gameObject.GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (! myParticleSystem.IsAlive ()) {
			Destroy (gameObject);
		}
	}
}
