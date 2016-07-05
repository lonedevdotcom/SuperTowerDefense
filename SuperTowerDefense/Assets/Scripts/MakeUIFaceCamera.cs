using UnityEngine;
using System.Collections;

public class MakeUIFaceCamera : MonoBehaviour {
	Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
	}
	
	void Update () {
		transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
	}
}
