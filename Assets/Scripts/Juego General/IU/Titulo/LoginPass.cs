using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginPass : MonoBehaviour {

	//Strings de URL y acceso
	string addressURL, addressURLPass = "blaisantka.com/login/verifypass.php?", 
			user, pass;

	//Canal alpha general
	float alpha = 0;

	//Esperar a hacer conexion con bases de datos
	public float waitForConnect = 2, speed = 2;

	//Booleanas de confirmacion y conexion
	bool noWait, notWaitForConnect, passVerify,
			accesoCompleto, finFullInsert;

	//Variables varias
	Image passwordImg;
	GameObject pass_;
	RectTransform userRect, passRect;
	InputField usuario, contrasena;
	Text passwordText;
	WWW webObject;



	void Update () {

		if (gameObject.activeSelf) {
			pass_ = GameObject.Find ("Pass");
			usuario = GetComponent<InputField> ();
			contrasena = GameObject.Find ("Pass").GetComponent<InputField> ();
		}

		//Si se ha finalizado la inserción
		if (finFullInsert)
			accesoCompleto = true;

		if (accesoCompleto && waitForConnect > 0)
			waitForConnect -= Time.deltaTime / 2;
		else if (accesoCompleto && !noWait && waitForConnect <= 0)
			FullAccess ();
		else if (noWait)
			FullAccess ();
	}

	public void FullAccess () {

		if (contrasena.text != "" && waitForConnect <= 0 && !passVerify) {
			user = usuario.text;
			pass = contrasena.text;
			addressURL = addressURLPass + "name=" + user + "&pass=" + pass;
			StartCoroutine (VerificaPass ());
		}

		finFullInsert = true;
	}

	IEnumerator VerificaPass () {

		InfoSignAndLogin info = FindObjectOfType<InfoSignAndLogin> ();
		webObject = new WWW (addressURL);

		yield return (webObject);
		if (webObject.isDone && webObject.bytesDownloaded > 0) {
			info.changeInfo = true;
			info.infoBadOrGood = true;
			info.empiezaContar = true;
			info.texto = "Bienvenido "+ user;
			info.Information ();
		}

		if (webObject.text.Equals ("1")) {
			passVerify = true;
			accesoCompleto = false;
			noWait = true;
		}
	}
}
