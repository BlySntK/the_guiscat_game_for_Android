using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParpadearTexto : MonoBehaviour {
	
	float alph = 0;
	bool block;


	public void InicializarParpadeo () {

		alph = 0;
		Text texto = GetComponent<Text> ();
		texto.color = new Color (texto.color.r, texto.color.g, texto.color.b, alph);
	}

	public void Parpadear () {

		Text texto = GetComponent<Text> ();
		if (alph < 1 && !block) {
			alph += Time.deltaTime;
			texto.color = new Color (texto.color.r, texto.color.g, texto.color.b, alph);
		}else if (alph >= 1 && !block)
			block = true;
		else if (alph > 0 && block) {
			alph -= Time.deltaTime;
			texto.color = new Color (texto.color.r, texto.color.g, texto.color.b, alph);
		}else if (alph <= 0 && block)
			block = false;
	}

	public void DetenerParpadeo () {

		Text texto = GetComponent<Text> ();
		if (alph > 0) {
			alph -= Time.deltaTime;
			texto.color = new Color (texto.color.r, texto.color.g, texto.color.b, alph);
		}
	}
}
