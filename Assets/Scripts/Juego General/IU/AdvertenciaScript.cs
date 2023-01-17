using UnityEngine;
using System.Collections;

public class AdvertenciaScript : MonoBehaviour {

	public float alpha = 0;
	bool blocked;
	public	float tiempoConsumo = 2;
	public float lanzarEnXSegundos = 5, contador = 0;
	SpriteRenderer sprite;


	void Start () {

		sprite = GetComponent<SpriteRenderer> ();
		sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, alpha);
	}

	public void LanzarAdvertencia () {

		//Visualizamos la advertencia
		if (lanzarEnXSegundos > 0)
			lanzarEnXSegundos -= Time.deltaTime;
		else{
			if (alpha < 1 && !blocked)
				alpha += Time.deltaTime;
			else
				blocked = true;

			if (alpha > 0 && blocked)
				alpha -= Time.deltaTime;
			else if (alpha <= 0 && blocked) {
				blocked = false;
				contador++;
			}

			sprite = GetComponent<SpriteRenderer> ();
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, alpha);

			if (contador > 5) {
				lanzarEnXSegundos = 5;
				contador = 0;
			}
		}
		//**********************************************************************************

		ConsumirPuntos ();
	}

	public void OcultarAdvertencia () {

		if (alpha > 0 || alpha <= 0) {
			alpha = 0;
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, alpha);
		}
	}

	void ConsumirPuntos () {

		PiensoDentro pienso = (PiensoDentro) FindObjectOfType<PiensoDentro> ();
		if (pienso.tiempoLimite <= 0) {
			if (tiempoConsumo > 0)
				tiempoConsumo -= Time.deltaTime;
			else{
				Puntuar puntos = (Puntuar) FindObjectOfType<Puntuar> ();

				puntos.puntos--;
				puntos._puntos.text = puntos.puntos.ToString ("0");
				tiempoConsumo = 2;
				pienso.tiempoLimite = 3;
			}
		}
	}
}
