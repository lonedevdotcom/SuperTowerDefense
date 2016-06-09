using UnityEngine;
using System.Collections;

public class MachineGunTargettingSystem : TurretTargettingSystem {
	GameObject muzzleFlashEffectsEmpty;
	AudioSource firingSound;

	override public void Start() {
		muzzleFlashEffectsEmpty = transform.FindChild("MuzzleFlashes").gameObject;
		firingSound = gameObject.GetComponent<AudioSource> ();
		base.Start ();
	}

	override public void MaybeFire() {
		currentEnemyHealth.ReduceHealth (Time.deltaTime * damagePerSecond);
	}

	override public void EngagedTarget() {
		muzzleFlashEffectsEmpty.SetActive (true);
		firingSound.loop = true;
		firingSound.Play ();
	}

	override public void DisengagedTarget() {
		muzzleFlashEffectsEmpty.SetActive (false);
		firingSound.loop = false;
	}
}
