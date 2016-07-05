using UnityEngine;
using System.Collections;

public class MouseClickHandler : MonoBehaviour {
	void Update() {
		if (Input.GetMouseButtonDown (0)) { 
			RaycastHit[] hits; 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			hits = Physics.RaycastAll (ray, 50f);
			if (hits != null && hits.Length > 0) {
				for (int i = 0; i < hits.Length; i++) {
					CanvasGroup cg = hits [i].collider.gameObject.GetComponent<CanvasGroup>() as CanvasGroup;
					if (cg != null) {
						cg.alpha = 1;
					}
				}
			}
		}
	}
}
