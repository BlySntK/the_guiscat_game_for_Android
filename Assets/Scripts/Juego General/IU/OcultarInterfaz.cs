using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OcultarInterfaz : MonoBehaviour {

	/*-----Este script solo se ejecutara cuando salgan las notificaciones---------*/
	public bool notificar;
	public bool informar;


	//Este metodo se ejecutara cuando tengamos que llenar el comedero, lo llamamos desde Llenar
	//Consiste en avisar por saturación de pienso en la caja
	public void Notificar () {

		if (notificar) {
			Image baseCantidad = GameObject.Find ("BaseCantidad").GetComponent<Image> ();
			Image medidor = GameObject.Find ("Medidor").GetComponent<Image> ();
			Text etiqueta = GameObject.Find ("EtiquetaCantidad").GetComponent<Text> ();
			Text cantidad = GameObject.Find ("Cantidad").GetComponent<Text> ();
			Image voltearIzq = GameObject.Find ("Voltear Izq").GetComponent<Image> ();
			Image voltearDch = GameObject.Find ("Voltear Dch").GetComponent<Image> ();
			Text puntuacion = GameObject.Find ("Puntuacion").GetComponent<Text> ();
			Text puntos = GameObject.Find ("Puntos").GetComponent<Text> ();

			baseCantidad.enabled = false;
			medidor.enabled = false;
			etiqueta.enabled = false;
			cantidad.enabled = false;
			voltearIzq.enabled = false;
			voltearDch.enabled = false;
			puntuacion.enabled = false;
			puntos.enabled = false;

		}else{
			Image baseCantidad = GameObject.Find ("BaseCantidad").GetComponent<Image> ();
			Image medidor = GameObject.Find ("Medidor").GetComponent<Image> ();
			Text etiqueta = GameObject.Find ("EtiquetaCantidad").GetComponent<Text> ();
			Text cantidad = GameObject.Find ("Cantidad").GetComponent<Text> ();
			Image voltearIzq = GameObject.Find ("Voltear Izq").GetComponent<Image> ();
			Image voltearDch = GameObject.Find ("Voltear Dch").GetComponent<Image> ();
			Text puntuacion = GameObject.Find ("Puntuacion").GetComponent<Text> ();
			Text puntos = GameObject.Find ("Puntos").GetComponent<Text> ();
			
			baseCantidad.enabled = true;
			medidor.enabled = true;
			etiqueta.enabled = true;
			cantidad.enabled = true;
			voltearIzq.enabled = true;
			voltearDch.enabled = true;
			puntuacion.enabled = true;
			puntos.enabled = true;
		}
	}

	//Este metodo entrara en funcionamiento cuando lo llame Empezar Partida
	public void Tutorial () {

		EmpezarPartida variablesGlobales = FindObjectOfType<EmpezarPartida> ();
		if (variablesGlobales.hacerTutorial) {
			Image botonLlenar = GameObject.Find ("BotonLlenar").GetComponent<Image> ();
			Text textoBoton = GameObject.Find ("TextoBoton").GetComponent<Text> ();
			Image baseCantidad = GameObject.Find ("BaseCantidad").GetComponent<Image> ();
			Image medidor = GameObject.Find ("Medidor").GetComponent<Image> ();
			Text etiqueta = GameObject.Find ("EtiquetaCantidad").GetComponent<Text> ();
			Text cantidad = GameObject.Find ("Cantidad").GetComponent<Text> ();
			Image voltearIzq = GameObject.Find ("Voltear Izq").GetComponent<Image> ();
			Image voltearDch = GameObject.Find ("Voltear Dch").GetComponent<Image> ();
			Image notificacion = GameObject.Find ("Notificacion").GetComponent<Image> ();
			Text puntuacion = GameObject.Find ("Puntuacion").GetComponent<Text> ();
			Text puntos = GameObject.Find ("Puntos").GetComponent<Text> ();
			
			botonLlenar.enabled = false;
			textoBoton.enabled = false;
			baseCantidad.enabled = false;
			medidor.enabled = false;
			etiqueta.enabled = false;
			cantidad.enabled = false;
			notificacion.enabled = false;
			voltearIzq.enabled = false;
			voltearDch.enabled = false;
			puntuacion.enabled = false;
			puntos.enabled = false;

		}else if (variablesGlobales.tutorialHecho) {
			Image botonLlenar = GameObject.Find ("BotonLlenar").GetComponent<Image> ();
			Text textoBoton = GameObject.Find ("TextoBoton").GetComponent<Text> ();
			Image baseCantidad = GameObject.Find ("BaseCantidad").GetComponent<Image> ();
			Image medidor = GameObject.Find ("Medidor").GetComponent<Image> ();
			Text etiqueta = GameObject.Find ("EtiquetaCantidad").GetComponent<Text> ();
			Image notificacion = GameObject.Find ("Notificacion").GetComponent<Image> ();
			Text cantidad = GameObject.Find ("Cantidad").GetComponent<Text> ();
			Image voltearIzq = GameObject.Find ("Voltear Izq").GetComponent<Image> ();
			Image voltearDch = GameObject.Find ("Voltear Dch").GetComponent<Image> ();
			Text puntuacion = GameObject.Find ("Puntuacion").GetComponent<Text> ();
			Text puntos = GameObject.Find ("Puntos").GetComponent<Text> ();
			
			botonLlenar.enabled = true;
			textoBoton.enabled = true;
			baseCantidad.enabled = true;
			medidor.enabled = true;
			etiqueta.enabled = true;
			cantidad.enabled = true;
			notificacion.enabled = true;
			voltearIzq.enabled = true;
			voltearDch.enabled = true;
			puntuacion.enabled = true;
			puntos.enabled = true;
		}
	}
}
