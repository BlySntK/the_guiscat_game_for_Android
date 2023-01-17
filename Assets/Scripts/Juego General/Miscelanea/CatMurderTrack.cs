using UnityEngine;
using System.Collections;

public class CatMurderTrack : MonoBehaviour {

	float tAudio = 0, veloSubVol = 0.6f;
	float volumen = 0, tiempoCambioMusica = 0;
	int numero = -1;
	float duracion = -1, numeroDevuelto = -1;
	public AudioClip[] clips;
	public AudioSource clip;
	bool principal, putosGatos, temaPrincipal, 
		temaPuesto_1, temaPuesto_2, temaPuesto_3,
		temaPuesto_4, temaPuesto_5, temaPuesto_6, ningunTemaPuesto;

	
	void Awake () {

		clip = GetComponent<AudioSource> ();
		clip.volume = 0;
	}

	//Numero aleatorio con el que cambiar la musica
	int Randomizar () {

		numero = Random.Range (0, 30);
		return numero;
	}

	//Reproduciremos alguno de los temas disponibles
	public void LanzarMusica (int c) {

		if (c >= 20) {
			putosGatos = true;
			principal = false;
		}else if (c <= 0) {
			putosGatos = false;
			principal = true;
		}

		if (principal)
			TemaPrincipal ();
		else if (putosGatos) {
			if (tiempoCambioMusica > 0)
				tiempoCambioMusica -= Time.deltaTime;
			else {
				numeroDevuelto = -1;
				if (numeroDevuelto == -1)
					numeroDevuelto = Randomizar ();
			}

			if (numeroDevuelto >= 0 && numeroDevuelto < 5)
				PrimerTema ();
			else if (numeroDevuelto >= 5 && numeroDevuelto < 10)
				SegundoTema ();
			else if (numeroDevuelto >= 10 && numeroDevuelto < 15)
				TercerTema ();
			else if (numeroDevuelto >= 15 && numeroDevuelto < 20)
				CuartoTema ();
			else if (numeroDevuelto >= 20 && numeroDevuelto < 25)
				QuintoTema ();
			else if (numeroDevuelto >= 25 && numeroDevuelto < 30)
				SextoTema ();

			if (tiempoCambioMusica <= 0 && numeroDevuelto > -1)
				tiempoCambioMusica = 130;
		}
	}

	void TemaPrincipal () {

		if (volumen > 0 && ningunTemaPuesto) {
			volumen -= veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}else if (volumen <= 0 && ningunTemaPuesto) {
			clip.clip = clips [0];
			clip.loop = true;
			clip.Play ();
			temaPrincipal = true;
		}
	}

	void PrimerTema () {

		if (!temaPuesto_1 && ningunTemaPuesto) {
			if (volumen > 0) {
				temaPrincipal = false;
				volumen -= veloSubVol * Time.deltaTime;
				clip.volume = volumen;
			}else if (volumen <= 0 && ningunTemaPuesto) {
				clip.clip = clips [1];
				clip.loop = true;
				clip.Play ();
				temaPuesto_1 = true;
			}
		}
	}

	void SegundoTema () {

		if (!temaPuesto_2 && ningunTemaPuesto) {
			if (volumen > 0) {
				temaPrincipal = false;
				volumen -= veloSubVol * Time.deltaTime;
				clip.volume = volumen;
			}else if (volumen <= 0 && ningunTemaPuesto) {
				clip.clip = clips [2];
				clip.loop = false;
				clip.Play ();
				temaPuesto_2 = true;
			}

		}else if (temaPuesto_2 && !temaPuesto_3) {
			if (duracion == -1) duracion = clip.clip.length;
			if (tAudio < duracion)
				tAudio += Time.deltaTime;
			else{
				if (!temaPuesto_3) {
					clip.clip = clips [3];
					clip.loop = true;
					clip.Play ();
					temaPuesto_3 = true;
				}
			}
		}
	}

	void TercerTema () {

		if (!temaPuesto_3 && ningunTemaPuesto) {
			if (volumen > 0) {
				temaPrincipal = false;
				volumen -= veloSubVol * Time.deltaTime;
				clip.volume = volumen;
			}else if (volumen <= 0) {
				clip.clip = clips [3];
				clip.loop = true;
				clip.Play ();
				temaPuesto_3 = true;
			}
		}
	}

	void CuartoTema () {

		if (!temaPuesto_4 && ningunTemaPuesto) {
			if (volumen > 0) {
				temaPrincipal = false;
				volumen -= veloSubVol * Time.deltaTime;
				clip.volume = volumen;
			}else if (volumen <= 0) {
				clip.clip = clips [4];
				clip.loop = true;
				clip.Play ();
				temaPuesto_4 = true;
			}
		}
	}

	void QuintoTema () {

		if (!temaPuesto_5 && ningunTemaPuesto) {
			if (volumen > 0) {
				temaPrincipal = false;
				volumen -= veloSubVol * Time.deltaTime;
				clip.volume = volumen;
			}else if (volumen <= 0) {
				clip.clip = clips [5];
				clip.loop = true;
				clip.Play ();
				temaPuesto_5 = true;
			}
		}
	}

	void SextoTema () {

		if (!temaPuesto_6 && ningunTemaPuesto) {
			if (volumen > 0) {
				temaPrincipal = false;
				volumen -= veloSubVol * Time.deltaTime;
				clip.volume = volumen;
			}else if (volumen <= 0) {
				clip.clip = clips [6];
				clip.loop = true;
				clip.Play ();
				temaPuesto_6 = true;
			}
		}
	}

	void Update () {

		//Subir volumenes:

		/* TEMA INICIAL */
		if (temaPrincipal && volumen < 1 && ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}else if (temaPrincipal && volumen < 1 && !ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}

		/* PRIMER TEMA MACHACANTE */
		if (temaPuesto_1 && volumen < 1 && ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}else if (temaPuesto_1 && volumen < 1 && !ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}

		/* SEGUNDO TEMA MACHACANTE */
		if (temaPuesto_2 && volumen < 1 && ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}else if (temaPuesto_2 && volumen < 1 && !ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}

		/* TERCER TEMA MACHACANTE */
		if (temaPuesto_3 && volumen < 1 && ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}else if (temaPuesto_3 && volumen < 1 && !ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}

		/* CUARTO TEMA MACHACANTE */
		if (temaPuesto_4 && volumen < 1 && ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}else if (temaPuesto_4 && volumen < 1 && !ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}

		/* QUINTO TEMA MACHACANTE */
		if (temaPuesto_5 && volumen < 1 && ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}else if (temaPuesto_5 && volumen < 1 && !ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}

		/* SEXTO TEMA MACHACANTE */
		if (temaPuesto_6 && volumen < 1 && ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}else if (temaPuesto_6 && volumen < 1 && !ningunTemaPuesto) {
			volumen += veloSubVol * Time.deltaTime;
			clip.volume = volumen;
		}

		if (!temaPrincipal && !temaPuesto_1 && !temaPuesto_2 && !temaPuesto_3 &&
		    !temaPuesto_4 && !temaPuesto_5 && !temaPuesto_6)
			ningunTemaPuesto = true;
		else
			ningunTemaPuesto = false;
	}
}