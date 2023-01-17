using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CambiarImgBoton : MonoBehaviour {

	Button bLlenar;
	Text texto;
	public bool available; //Variable manejada por Ocultar Interfaz, hace la excepcionpara que aparezca o no el prohibido


	void Start () {

		bLlenar = GetComponent<Button> ();
		texto = GameObject.Find ("TextoBoton").GetComponent<Text> ();
	}

	void Update () {

		Llenar bActivo = GameObject.Find ("BotonLlenar").GetComponent<Llenar> ();
		if (available) {
			if (bActivo.cont == 1)
				texto.text = "PARAR LLENADO";
			else if (bActivo.cont == 0)
				texto.text = "LLENAR CAJA";
		}
	}

	//Metodos solo disponibles para el objeto Llenar
	public void NoDisponible () {

		bLlenar.interactable = false;
		available = false;
		Image blocked = GameObject.Find ("BotonBlocked").GetComponent<Image> ();
		blocked.enabled = true;
	}

	public void Disponible () {

		bLlenar.interactable = true;
		available = true;
		Image blocked = GameObject.Find ("BotonBlocked").GetComponent<Image> ();
		blocked.enabled = false;
	}
	//***********************************************
}
