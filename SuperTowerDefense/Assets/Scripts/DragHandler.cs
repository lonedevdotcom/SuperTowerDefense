using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public GameObject prefab;
	GameObject prefabInstance;

	// Use this for initialization
	void Start () {
		prefabInstance = Instantiate (prefab);
		RemoveScriptsFromPrefab ();
		AdjustPrefAlpha ();
		prefabInstance.SetActive (false);
	}

	void RemoveScriptsFromPrefab() {
		Component[] components = prefabInstance.GetComponentsInChildren<TurretTargettingSystem>();
		foreach (Component component in components) {
			Destroy (component);
		}
	}

	void AdjustPrefAlpha() {
		MeshRenderer[] meshRenderers = prefabInstance.GetComponentsInChildren<MeshRenderer> ();
		for (int i = 0; i < meshRenderers.Length; i++) {
			Material mat = meshRenderers [i].material;
			meshRenderers [i].material.color = new Color (mat.color.r, mat.color.g, mat.color.b, 0.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) {
		// Debug.Log("Beginning drag");
	}

	public void OnDrag(PointerEventData eventData) {
		// Debug.Log(eventData);
		RaycastHit[] hits; 
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		hits = Physics.RaycastAll (ray, 50f);
		if ( hits != null && hits.Length > 0) {
			int terrainCollderQuadIndex = GetTerrainColliderQuadIndex (hits);
			if (terrainCollderQuadIndex != -1) {
				prefabInstance.transform.position = hits[terrainCollderQuadIndex].point;
				prefabInstance.SetActive (true);
				// Debug.Log (hits [terrainCollderQuadIndex].point);
			} else {
				prefabInstance.SetActive (false);
			}
		}
	}

	int GetTerrainColliderQuadIndex(RaycastHit[] hits) {
		for (int i = 0; i < hits.Length; i++) {
			if (hits [i].collider.gameObject.name.Equals ("TerrainColliderQuad")) {
				return i;
			}
		}

		return -1;
	}

	public void OnEndDrag(PointerEventData eventData) {
		// Debug.Log("Drag stopped");
		prefabInstance.SetActive (false);
	}
}
