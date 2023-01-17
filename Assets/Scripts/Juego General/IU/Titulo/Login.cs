using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Login : MonoBehaviour {

	//Strings de URL y acceso
	string addressURL, addressURLUser = "blaisantka.com/login/verifyuser.php?", addressURLPass = "blaisantka.com/login/verifypass.php?", 
			user, pass;

	//Canal alpha general
	float alpha = 0;

	//Esperar a hacer conexion con bases de datos
	public float waitForConnect = 2, speed = 2;

	//Booleanas de confirmacion y conexion
	bool userVerify, passAccess, descubrirPass, 
			acceder, noWait, finInsert, notWaitForConnect;

	//Variables de ubicacion y velocidad
	float currentPosYUser = 0, currentPosYPass = 0, velocidad = 18;

	//Posiciones Password Field
	const float posYInitialPass = 437.68f, posYFinalPass = 421.7f;

	//Posiciones User field
	const float posYInitialUser = 437.68f, posYFinalUser = 454.5f;

	//Variables varias
	Image passwordImg;
	GameObject pass_;
	RectTransform userRect, passRect;
	InputField usuario, contrasena;
	Text passwordText;
	WWW webObject;


	void Awake () {
	
		passwordImg = GameObject.Find ("Pass").GetComponent<Image> ();
		pass_ = GameObject.Find ("Pass");
		passwordText = GameObject.Find ("passText_").GetComponent<Text> ();
		usuario = GetComponent<InputField> ();
		contrasena = GameObject.Find ("Pass").GetComponent<InputField> ();
		userRect = GetComponent<RectTransform> ();
		passRect = GameObject.Find ("Pass").GetComponent<RectTransform> ();
		passwordImg.color = new Color (passwordImg.color.r, passwordImg.color.g, passwordImg.color.b, alpha);
		passwordText.color = new Color (passwordText.color.r, passwordText.color.g, passwordText.color.b, alpha);
		contrasena.interactable = false;
		pass_.SetActive (false);
	}

	void Update () {

		if (descubrirPass)
			DescubrirPassword ();
		else
			ContraerPass ();

		/* 
		 * Si no hay un usuario puesto en el campo user 
		 * volvemos a poner el campo tal cual estaba
		 */
		if (usuario.text == "" && userVerify) {
			descubrirPass = false;
			passAccess = false;
			userVerify = false;
			contrasena.interactable = false;
			pass_.SetActive (false);
			acceder = false;
			finInsert = false;
		}

		//Si se pulsa la tecla enter (version Windows) accedemos a verificar el user
		#if UNITY_STANDALONE_WIN
		if (Input.GetKeyDown (KeyCode.Return))
			acceder = true;
		#endif

		//Si se ha finalizado la inserción
		if (finInsert && !noWait)
			acceder = true;

		if (acceder && !noWait && waitForConnect > 0)
			waitForConnect -= speed * Time.deltaTime / 2;
		else if (acceder && !noWait && waitForConnect <= 0)
			AccesoUsuario ();
		else if (noWait)
			AccesoUsuario ();
	}

	public void AccesoUsuario () {

		/* 
		 * Damos un primer acceso, a la contraseña, despues de verificar que
		 * existe el usuario
		 */
		if (usuario.text != "" && waitForConnect <= 0 && !userVerify) {
			user = usuario.text;
			addressURL = addressURLUser + "name=" + user;
			StartCoroutine (VerificaUser ());
		}
		
		//Una vez verificado el user, pasamos a verificar la pass
		if (userVerify && usuario.text != "" && contrasena.interactable) {
			pass_.SetActive (true);
			descubrirPass = true;
		}

		//Cuando se pierda el foco:
		finInsert = true;
	}

	IEnumerator VerificaUser () {
	
		InfoSignAndLogin info = FindObjectOfType<InfoSignAndLogin> ();

		//Descargamos el contenido de la website Blaisantka (su base de datos)
		webObject = new WWW (addressURL);

		//Esperamos hasta que esté descargado
		yield return webObject;

		//Si fue bien la conexión, verificamos los datos obtenidos
		if (webObject.isDone && webObject.bytesDownloaded > 0) {
			//Si hay coincidencia de usuario:
			if (webObject.text.Equals ("1")) {
				userVerify = true;
				contrasena.interactable = true;
				acceder = false;
				noWait = true;
			
			//De lo contrario:
			}else if (webObject.text != "1" && usuario.text != "") {
				info.changeInfo = true;
				info.infoBadOrGood = false;
				info.empiezaContar = true;
				info.texto = "El usuario no éxiste";
				info.Information ();
			}

			//Si hubo algún error en la inserción
			if (webObject.error != null) {
				info.changeInfo = true;
				info.infoBadOrGood = false;
				info.empiezaContar = true;
				info.texto = "El usuario o la contraseña no son válidos...";
				info.Information ();
			}
		
		//Si no se pudo conectar a la base de datos:
		}else if (webObject.bytesDownloaded <= 0) {
			info.changeInfo = true;
			info.infoBadOrGood = false;
			info.empiezaContar = true;
			info.texto = "No se puede establecer conexión";
			info.Information ();
			waitForConnect = 2;
			acceder = false;
		}
	}

	void DescubrirPassword () {

		if (currentPosYUser == 0)
			currentPosYUser = posYInitialUser;
		if (currentPosYPass == 0)
			currentPosYPass = posYInitialPass;
		
		if (currentPosYUser < posYFinalUser) {
			currentPosYUser += velocidad * Time.deltaTime;
			userRect.localPosition = new Vector3 (userRect.localPosition.x, currentPosYUser, userRect.localPosition.z);
		}

		//Si el GameObject está activo podremos ejecutar las sentencias siguientes:
		if (pass_.activeSelf) {
			if (alpha < 1) {
				alpha += Time.deltaTime;
				passwordImg.color = new Color (passwordImg.color.r, passwordImg.color.g, passwordImg.color.b, alpha);
				passwordText.color = new Color (passwordText.color.r, passwordText.color.g, passwordText.color.b, alpha);
			}

			if (currentPosYPass > posYFinalPass) {
				currentPosYPass -= velocidad * Time.deltaTime;
				passRect.localPosition = new Vector3 (passRect.localPosition.x, currentPosYPass, passRect.localPosition.z);
			}

			//Una vez verificado el user y descubierto el campo pass, se restablece el primer acceso
			if (alpha >= 1 && currentPosYPass <= posYFinalPass && currentPosYUser >= posYFinalUser) {
				passAccess = true;
				noWait = false;
				finInsert = false;
				waitForConnect = 2;
			}
		}
	}

	void ContraerPass () {

		if (currentPosYPass == 0)
			currentPosYPass = posYInitialPass;

		if (currentPosYUser > posYInitialUser) {
			currentPosYUser -= velocidad * Time.deltaTime;
			userRect.localPosition = new Vector3 (userRect.localPosition.x, currentPosYUser, userRect.localPosition.z);
		}

		if (!pass_.activeSelf) {
			if (alpha > 0) {
				alpha -= Time.deltaTime;
				passwordImg.color = new Color (passwordImg.color.r, passwordImg.color.g, passwordImg.color.b, alpha);
				passwordText.color = new Color (passwordText.color.r, passwordText.color.g, passwordText.color.b, alpha);
			}

			if (currentPosYPass < posYInitialPass) {
				currentPosYPass += velocidad * Time.deltaTime;
				passRect.localPosition = new Vector3 (passRect.localPosition.x, currentPosYPass, passRect.localPosition.z);
			}
		}
	}
}
