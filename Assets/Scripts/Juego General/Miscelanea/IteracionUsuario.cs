using UnityEngine;
using System.Collections;

public class IteracionUsuario : MonoBehaviour {

	bool interactuado;

	public bool Interactuar () {

		if (Input.touchCount > 0) {
			if (Input.GetTouch(0).phase == TouchPhase.Began) {
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 10000)) {
					interactuado = true;
				}
			}
		}

		return interactuado;
	}
}
