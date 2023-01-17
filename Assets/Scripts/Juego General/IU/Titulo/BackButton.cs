using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackButton : MonoBehaviour {

	ChangeButtons changes;
	GameObject register;
	[HideInInspector]
	public GameObject login, hecho;

	public void Back () {

		register = GameObject.Find ("Registro");
		register.SetActive (false);
		login.SetActive (true);
		hecho.SetActive (false);
		changes = FindObjectOfType<ChangeButtons> ();
		changes.back = false;
		changes.register = true;
	}
}
