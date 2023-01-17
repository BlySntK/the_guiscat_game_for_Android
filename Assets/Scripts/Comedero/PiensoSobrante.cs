using UnityEngine;
using System.Collections;

public class PiensoSobrante : MonoBehaviour {

	void OnCollisionStay (Collision grano) {

		PiensoDentro quitarGrano = (PiensoDentro) FindObjectOfType<PiensoDentro> ();
		if (quitarGrano.cantidad <= 0) {
			if (grano.gameObject.name.Contains ("Grano"))
				Destroy (grano.gameObject);
		}
	}
}
