using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BotonHabilitado : MonoBehaviour {

	public int currentChar = 0;
	InputField password;
	bool backSpace;
	Button hecho;
	const int minCharacters = 5, maxCharacter = 10;


	void Awake () {
	
		password = GetComponent<InputField> ();
		hecho = GameObject.Find ("RegistroHecho").GetComponent<Button> ();
	}


	void Update () {

		if (password.text == "") {
			currentChar = 0;
			hecho.interactable = false;
		}

		if (password.text != "" && currentChar >= minCharacters && !backSpace)
			hecho.interactable = true;
		else if (password.text != "" && currentChar < minCharacters && !backSpace)
			hecho.interactable = false;

		if (currentChar == maxCharacter)
			password.characterLimit = currentChar;

		ActualizarBackSpace ();
	}


	public void Habilitar () {

		ActualizarBackSpace ();

		if (password.text != "" && currentChar < maxCharacter && !backSpace) {
			currentChar++;
			hecho.interactable = false;
		}else if (password.text != "" && currentChar < maxCharacter && backSpace)
			currentChar--;
		else if (password.text != "" && currentChar >= minCharacters || currentChar <= maxCharacter && backSpace) {
			currentChar--;
			hecho.interactable = false;
		}
	}

	void ActualizarBackSpace () {
	
		if (Input.GetKeyDown (KeyCode.Backspace)) {
			backSpace = true;
		} else
			backSpace = false;
	}
}
