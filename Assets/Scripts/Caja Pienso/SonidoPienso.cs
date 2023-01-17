using UnityEngine;
using System.Collections;

public class SonidoPienso : MonoBehaviour {
	
	public float lenClip, speedT = 5;


	void Update () {

		InstanciaGranos llenar = GetComponent<InstanciaGranos> ();
		AudioSource sonido = GetComponent<AudioSource> ();
		if (llenar.llenar) {
			if (lenClip == 0) lenClip = sonido.clip.length;
			if (lenClip > 0) lenClip -= speedT * Time.deltaTime;
			else{
				sonido.Play ();
				sonido.loop = false;
				lenClip = 0;
			}
		}
	}
}
