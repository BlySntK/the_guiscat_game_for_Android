using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOutGuiscat : MonoBehaviour {

	public void Fade () {

		FadeOFFGuiscat fade = GetComponent<FadeOFFGuiscat> ();
		if (fade.alpha < 1)
			fade.StartFade (1);
	}

	void Update () {

		FadeOFFGuiscat fade = GetComponent<FadeOFFGuiscat> ();
		ThemeScript cambioMusica = FindObjectOfType<ThemeScript> ();
		if (fade.continuar /* && cambioMusica.clip.volume <= 0*/) {
			//SceneManager.LoadScene ("The_Guiscat");
			ParpadearTexto texto = GameObject.Find ("ParpadeaTexto").GetComponent<ParpadearTexto> ();
			MenuGuiscat menu = FindObjectOfType<MenuGuiscat> ();
			if (!menu.parpadearOtraVez)
				texto.DetenerParpadeo ();
		}
	}
}
