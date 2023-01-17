using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOutAviso : MonoBehaviour {

	float tiempoEspera = 8;
	FadeOFFAviso fade;

	public void Fade () {

		fade = GetComponent<FadeOFFAviso> ();
		if (fade.alpha < 1)
			fade.StartFade (1);
	}

	void Update () {

		fade = GetComponent<FadeOFFAviso> ();
		if (fade.continuar)
			SceneManager.LoadScene ("BlaisantkaScene");
		else if (fade.load)
			SceneManager.LoadScene ("BlaisantkaScene");
	}
}
