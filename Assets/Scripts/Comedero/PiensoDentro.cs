using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PiensoDentro : MonoBehaviour {

	GameObject pienso;
	[HideInInspector]
	public int cantidad; //Variable para controlar el pienso que tenemos
	const int CANTIDAD = 163; //Cantidad total de pienso que se puede llenar
	string nombreGato;
	float extraSaludGato = 5;
	bool descontarPorEscased;
	[HideInInspector]
	public bool gameOver;
	[HideInInspector]
	public bool lanzarAdvertencia; //Esta booleana sera clave para el GameOver en una de sus condiciones en Puntuar
	[HideInInspector]
	public bool recuperar; //Esta otra booleana tambien es clave para restablecer el GameOver en Puntuar
	public float tiempoLimite = 8;
	Image medidor;
	Puntuar puntos;
	EmpezarPartida hayGatos;
	float pointsWalking = 5, pointsRunners = 10;
	float tiempoComer = 1.5f;
	Text txt;


	void Start () {

		medidor = GameObject.Find ("Medidor").GetComponent<Image> ();
		txt = GameObject.Find ("Cantidad").GetComponent<Text> ();
		medidor.rectTransform.sizeDelta = new Vector2 (0, 19.7f);
		cantidad = 0;
		txt.text = cantidad.ToString ("0");
	}

	void Update () {

		//En cuanto pasemos el tutorial se activara el riesgo de escased la primera vez
		EmpezarPartida empezar = FindObjectOfType<EmpezarPartida> ();
		if (empezar.avisoLlenar) {
			puntos = FindObjectOfType<Puntuar> ();
			PiensoDentro granosDentro = FindObjectOfType<PiensoDentro> ();

			//Habra riesgo de escased si no llegamos al minimo de pienso...
			if (granosDentro.cantidad < 10)
				descontarPorEscased = true;

			//...Será cuando descontemos puntos
			if (descontarPorEscased)
				Escased ();

			//Mientras haya la cantidad de pienso adecuada, la musica sera vibrante
			CatMurderTrack musica = FindObjectOfType<CatMurderTrack> ();			
			if (cantidad <= 0)
				musica.LanzarMusica (cantidad);
			else if (cantidad >= 0)
				musica.LanzarMusica (cantidad);
		}
	}

	void OnTriggerEnter (Collider col) {

		if (col.gameObject.name.Contains ("Grano")) {
			//Detectamos el pienso y lo contabilizamos
			txt = GameObject.Find ("Cantidad").GetComponent<Text> ();
			medidor = GameObject.Find ("Medidor").GetComponent<Image> ();
			cantidad++;
			txt.text = cantidad.ToString ("0");
			medidor.rectTransform.sizeDelta = new Vector2 (cantidad, 19.7f);

			//Luz verde a la salida felina (si ya hicimos el tutorial)
			AparecerGatos comenzar = FindObjectOfType<AparecerGatos> ();
			EmpezarPartida tutorial = FindObjectOfType<EmpezarPartida> ();
			if (tutorial.tutorialHecho)
				comenzar.comienzaLaHorda = true;

			//Sonido del grano
			ReproducirCaida ();

			//Si nos excedemos, aplicamos la penalizacion
			Exceso ();
		}

		if (col.gameObject.name.Contains ("GatitoCamina") && cantidad > 0) {
			//El gato se parara en el comedero
			GatitoCaminaScript gatito = col.GetComponent<GatitoCaminaScript> ();
			gatito.llego = true;
		}
		
		if (col.gameObject.name.Contains ("GatitoCorre") && cantidad > 0) {
			//El gato se parara en el comedero
			GatitoCorreScript gatito = col.GetComponent<GatitoCorreScript> ();
			gatito.llego = true;
		}
	}

	public void ReproducirCaida () {

		AudioSource sonido = GameObject.Find ("Comedero").GetComponent<AudioSource> ();
		sonido.Play ();
	}

	void OnTriggerStay (Collider other) {

		//Y empezara a comer el pienso
		if (other.gameObject.name.Contains ("GatitoCamina")) {
			GatitoCaminaScript gatito = other.GetComponent<GatitoCaminaScript> ();

			//Cuando llegue, pare y haya comida
			if (cantidad > 0 && gatito.llego) {
				if (tiempoComer > 0)
					tiempoComer -= Time.deltaTime;
				else {
					cantidad--;
					if (gatito.currentCantidadComida < GatitoCaminaScript.CANTIDAD_COMIDA)
						gatito.currentCantidadComida = cantidad;
					else{
						gatito.llego = false;
						gatito.volverAtras = true;
					}
					
					txt.text = cantidad.ToString ("0");
					medidor.rectTransform.sizeDelta = new Vector2 (cantidad, 19.7f);
					puntos.despuntuar = true;
					puntos.aux_descon = pointsWalking;
					tiempoComer = 1.5f;
					pienso = GameObject.Find ("Grano_" + cantidad.ToString ("0"));
					Destroy (pienso);
				}
			} else if (cantidad <= 0 && gatito.llego)
				gatito.llego = false;
		}else if (other.gameObject.name.Contains ("GatitoCorre")) {
			GatitoCorreScript gatito = other.GetComponent<GatitoCorreScript> ();

			//Cuando llegue, pare y haya comida
			if (cantidad > 0 && gatito.llego) {
				if (tiempoComer > 0)
					tiempoComer -= Time.deltaTime;
				else {
					cantidad--;
					if (gatito.currentCantidadComida < GatitoCorreScript.CANTIDAD_COMIDA)
						gatito.currentCantidadComida = cantidad;
					
					txt.text = cantidad.ToString ("0");
					medidor.rectTransform.sizeDelta = new Vector2 (cantidad, 19.7f);
					puntos.despuntuar = true;
					puntos.aux_descon = pointsRunners;
					tiempoComer = 1.5f;
					pienso = GameObject.Find ("Grano_" + cantidad.ToString ("0"));
					Destroy (pienso);
				}
			}
		}
	}

	void Exceso () {

		if (cantidad > CANTIDAD) {
			//Perdida de pienso por el camino: Mas vitalidad para el gato (si lo hay)
			medidor = GameObject.Find ("Medidor").GetComponent<Image> ();
			cantidad = CANTIDAD;
			medidor.rectTransform.sizeDelta = new Vector2 (cantidad, 19.7f);

			//Mayor vitalidad gatuna:
			hayGatos = FindObjectOfType<EmpezarPartida> ();
			if (hayGatos.n_gato > 0) {
				SaludGatoCamina saludCamina = FindObjectOfType<SaludGatoCamina> ();
				saludCamina.Salud (extraSaludGato);
			}
		}
	}

	void Escased () {

		if (!gameOver) {
			if (cantidad <= 20) {
				// Perdida de puntos en...
				/* Lanzamos aviso a los 10 segundos */
				if (tiempoLimite > 0)
					tiempoLimite -= Time.deltaTime;
				else{
					lanzarAdvertencia = true;
					recuperar = false;

					//Visualizar la barra de medicion
					medidor = GameObject.Find ("Medidor").GetComponent<Image> ();
					medidor.rectTransform.sizeDelta = new Vector2 (cantidad, 19.7f);

				}
			}else if (cantidad >= 10) {
				lanzarAdvertencia = false;
				descontarPorEscased = false;
				recuperar = true;
				tiempoLimite = 8;
			}
		}else{
			lanzarAdvertencia = false;
			descontarPorEscased = false;
			recuperar = false;
		}

		if (lanzarAdvertencia) {
			AdvertenciaScript advertencia = (AdvertenciaScript) FindObjectOfType<AdvertenciaScript> ();
			advertencia.LanzarAdvertencia ();
		}else{
			AdvertenciaScript advertencia = (AdvertenciaScript) FindObjectOfType<AdvertenciaScript> ();
			advertencia.OcultarAdvertencia ();
		}
	}
}
