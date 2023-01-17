using UnityEngine;
using System.Collections;

public class ThemeScript : MonoBehaviour {
	
	void Start () {

		DontDestroyOnLoad (gameObject);
		clip = GetComponent<AudioSource> ();
		numeroClip = Random.Range (0, 10);
		if (numeroClip < 5) {
			clip.clip = clips [0];
			Reproducir ();
		}else if (numeroClip >= 5 && numeroClip <= 10) {
			clip.clip = clips [1];
			Reproducir ();
		}
	}

	float tAudio = 0, velo = 0.3f;
	float quitVolume = 1;
	[HideInInspector]
	public int numeroClip;
	public AudioClip[] clips;
	bool reproducido; 
	public bool quitarAudio;
	[HideInInspector]
	public AudioSource clip;
	MenuGuiscat opcion;

	void Update () {

		//Si está el tema secundario, nos aseguramos que cambia correctamente a su loop
		if (numeroClip >= 5 && numeroClip <= 10) {
			if (tAudio < clips [1].length)
				tAudio += Time.deltaTime;
			else {
				if (!reproducido) {
					clip.clip = clips [2];
					clip.loop = true;
					Reproducir ();
					reproducido = true;
				}
			}
		}else if (numeroClip < 5)
			clip.loop = true;
		//*************************************************************************

		/* 
		 * En estas líneas de abajo vamos a hacer transición de la pantalla inicial al menu.
		 * Tendremos que darle a la pantalla para acceder a él y después, una vez elegida la
		 * opción, se bajará la música.
		 */
		opcion = FindObjectOfType<MenuGuiscat> ();
		//Codigo de la opción elegida aquí:
		if (quitarAudio) {
			if (clip.volume > 0) {
				quitVolume -= velo * Time.deltaTime;
				clip.volume = quitVolume;
			}else
				Destroy (gameObject);
		}
	}

	void Reproducir () {

		clip = GetComponent<AudioSource> ();
		clip.Play ();
	}
}
