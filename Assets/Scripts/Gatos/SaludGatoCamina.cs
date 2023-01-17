using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaludGatoCamina : MonoBehaviour {

	public Sprite[] textura;
	SpriteRenderer barra;
	EmpezarPartida variablesGlobales;
	float puntos = 2;
	int countShake = 0; 
	public int numeroGatos;
	const float SALUD = 8.81f;

	public float Salud () {

		barra = GetComponent<SpriteRenderer> ();
		barra.transform.localScale = new Vector3 (SALUD, barra.transform.localScale.y, barra.transform.localScale.z);
		return SALUD;
	}

	public void Salud (float health, GameObject padre, float time) {

		variablesGlobales = FindObjectOfType<EmpezarPartida> ();
		if (health > 6) {
			barra = GetComponent<SpriteRenderer> ();
			barra.sprite = textura [0];
			barra.transform.localScale = new Vector3 (health, barra.transform.localScale.y, barra.transform.localScale.z);
		}else if (health < 6 && health > 3) {
			barra = GetComponent<SpriteRenderer> ();
			barra.sprite = textura [1];
			barra.transform.localScale = new Vector3 (health, barra.transform.localScale.y, barra.transform.localScale.z);
		}else if (health < 3 && health > 0) {
			barra = GetComponent<SpriteRenderer> ();
			barra.sprite = textura [2];
			barra.transform.localScale = new Vector3 (health, barra.transform.localScale.y, barra.transform.localScale.z);
		}else if (health <= 0) {
			barra = GetComponent<SpriteRenderer> ();
			barra.sprite = null;
			GenerarReventonCaminan revienta = padre.GetComponent<GenerarReventonCaminan> ();
			revienta.Revienta ();
			barra.transform.localScale = new Vector3 (health, barra.transform.localScale.y, barra.transform.localScale.z);
			Puntuar puntuar = FindObjectOfType<Puntuar> ();
			puntuar.puntuando = true;
			puntuar.aux_p = puntos;
			Text gatos = GameObject.Find ("NumeroGatos").GetComponent<Text> ();
			if (numeroGatos < 1) {
				numeroGatos++;
				variablesGlobales.numeroGatos += numeroGatos;
				gatos.text = variablesGlobales.numeroGatos.ToString ("0");
			}

			/* Sector de la condición en la que agitamos la cámara un límite de veces */
			CameraShake camShack = FindObjectOfType<CameraShake> ();
			variablesGlobales.contadorShakes++;

			//Comprobamos cuantas sacudidas hay en curso o han habido
			if (countShake <= 0)
				countShake = variablesGlobales.contadorShakes;
			else if (countShake > 2) {
				countShake = 0;
				variablesGlobales.contadorShakes = 0;
			}

			//Dependiendo de las que haya [sacudidas], se ejecuta una nueva (o no)
			if (camShack.IsInvoking ("Shaking") && countShake < 2) {
				camShack.Shaking ();
				countShake++;
			}else if (camShack.IsInvoking ("Shaking") && countShake >= 2) {}
			else if (!camShack.IsInvoking ("Shaking") && countShake <= 0) {
				camShack.Shaking ();
				countShake++;
			}
			/***************************************************************/

			AparecerGatos aparecer = FindObjectOfType<AparecerGatos> ();
			if (aparecer.gatoTuto <= 5 && variablesGlobales.hacerTutorial) {
				if (!variablesGlobales.tutorialHecho) {
					aparecer.contarGato = true;
					aparecer.instanciar = true;
				}
			}

			Destroy (padre, time);
		}
	}

	public void Salud (float health) {

		if (health > 6) {
			barra = GetComponent<SpriteRenderer> ();
			barra.sprite = textura [0];
			barra.transform.localScale = new Vector3 (health, barra.transform.localScale.y, barra.transform.localScale.z);
		}else if (health < 6 && health > 3) {
			barra = GetComponent<SpriteRenderer> ();
			barra.sprite = textura [1];
			barra.transform.localScale = new Vector3 (health, barra.transform.localScale.y, barra.transform.localScale.z);
		}else if (health < 3 && health > 0) {
			barra = GetComponent<SpriteRenderer> ();
			barra.sprite = textura [2];
			barra.transform.localScale = new Vector3 (health, barra.transform.localScale.y, barra.transform.localScale.z);
		}
	}
}
