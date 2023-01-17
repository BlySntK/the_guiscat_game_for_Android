using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class RegisterDB : MonoBehaviour {

	string user, pass;
	GameObject register, login, hechoBoton;
	ChangeButtons changes;
	BackButton loginPanelState;
	Button hecho;

	void Awake () {
	
		register = GameObject.Find ("Registro");
		login = GameObject.Find ("Logueo");
		hechoBoton = GameObject.Find ("RegistroHecho");
		loginPanelState = FindObjectOfType<BackButton> ();
		loginPanelState.login = login;
		loginPanelState.hecho = hechoBoton;
		register.SetActive (false);
		changes = FindObjectOfType<ChangeButtons> ();
		changes.register = true;

		//Deshabilitamos el 'Hecho' hasta tener los campos rellenados antes de ocultarlo
		hecho = GameObject.Find ("RegistroHecho").GetComponent<Button> ();
		hecho.interactable = false;

		//Lo ocultamos
		hechoBoton.SetActive (false);
	}

	public void Registrar() {

		//Mostramos panel de registro
		register.SetActive (true);
		login.SetActive (false);
		hechoBoton.SetActive (true);

		//Cambiamos los botones
		changes.back = true;
		changes.register = false;
	}
}
