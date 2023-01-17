using UnityEngine;
using System.Collections;

public class Metralladora : MonoBehaviour {

	public AudioClip[] otherClip;

	public void DispararUnaVez () {

		AudioSource disparo = GetComponent<AudioSource> ();
		disparo.clip = otherClip[0];
		disparo.loop = false;
		disparo.Play ();
	}

	public void DispararLoop () {
	
		AudioSource disparo = GetComponent<AudioSource> ();
		disparo.clip = otherClip[1];
		disparo.loop = true;
		disparo.Play ();
	}
}
