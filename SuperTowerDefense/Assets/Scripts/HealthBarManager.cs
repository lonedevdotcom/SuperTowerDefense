using UnityEngine;
using System.Collections;

public class HealthBarManager : MonoBehaviour {

	Camera mainCamera;
	RectTransform healthRectTransform;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		EnemyHealthManager ehm = gameObject.GetComponentInParent<EnemyHealthManager> ();
		ehm.OnHealthChangedAction += HandleHealthChangedAction;
		healthRectTransform = (RectTransform)gameObject.transform.Find ("Health").transform;
	}

	void HandleHealthChangedAction (float currentHealth, float maxHealth)
	{
		// Debug.Log ("Yep, I can see that the health is now " + currentHealth);
		healthRectTransform.localScale = new Vector3 (currentHealth / maxHealth, 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
	}

}
