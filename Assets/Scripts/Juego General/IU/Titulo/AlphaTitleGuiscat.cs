using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlphaTitleGuiscat : MonoBehaviour {

	[HideInInspector]
	public float alpha = 0; 
	float velocidadDeAparicion;
	ThemeScript theme;

	void Update () {

		if (theme == null)
			theme = FindObjectOfType<ThemeScript> ();
		else {
			if (theme.numeroClip < 5) velocidadDeAparicion = 0.08f;
			else if (theme.numeroClip >= 5 && theme.numeroClip <= 10) velocidadDeAparicion = 0.1f;

			if (alpha < 1) {
				alpha += velocidadDeAparicion * Time.deltaTime;
				Image title = GetComponent<Image> ();
				title.color = new Color (title.color.r, title.color.g, title.color.b, alpha);
			}else{
				//Lo usamos para hacer parpadear el texto que surje
				ParpadearTexto parpadea = FindObjectOfType<ParpadearTexto> ();
				FadeOFFGuiscat fade = FindObjectOfType<FadeOFFGuiscat> ();
				if (alpha >= 1 && !fade.turn && !fade.continuar)
					parpadea.Parpadear ();
			}
		}
	}
}
