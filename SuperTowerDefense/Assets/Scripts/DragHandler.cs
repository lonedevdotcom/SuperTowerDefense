using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public GameObject prefab;
	GameObject hoverPrefab;

	// Use this for initialization
	void Start () {
		hoverPrefab = Instantiate (prefab);
		RemoveScriptsFromPrefab ();
		AdjustPrefabAlpha ();
		hoverPrefab.SetActive (false);
	}

	void RemoveScriptsFromPrefab() {
		Component[] components = hoverPrefab.GetComponentsInChildren<TurretTargettingSystem>();
		foreach (Component component in components) {
			Destroy (component);
		}
	}

	void AdjustPrefabAlpha() {
		MeshRenderer[] meshRenderers = hoverPrefab.GetComponentsInChildren<MeshRenderer> ();
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
				hoverPrefab.transform.position = hits[terrainCollderQuadIndex].point;
				hoverPrefab.SetActive (true);
				// Debug.Log (hits [terrainCollderQuadIndex].point);
			} else {
				hoverPrefab.SetActive (false);
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
		// If the prefab instance is active after dragging stopped, it means
		// it's in the arena so (for now), just drop it in.
		if (hoverPrefab.activeSelf) {
			Instantiate (prefab, hoverPrefab.transform.position, Quaternion.identity);
		}

		// Then set it to inactive ready for the next drag!
		hoverPrefab.SetActive (false);
	}
}
