using UnityEngine;
using System.Collections;

public class GenerarReventonCaminan : MonoBehaviour {

	public GameObject reventon;

	public void Revienta () {

		//Se escuchará un grito de gato y explotará
		AudioSource grito = GetComponent<AudioSource> ();
		grito.loop = false;

		if (!grito.isPlaying)
			grito.Play ();

		Vector3 posicionPadre = new Vector3 (transform.localPosition.x, transform.localPosition.y + 70, transform.localPosition.z);
		Instantiate (reventon, posicionPadre, transform.rotation);
	}
}
