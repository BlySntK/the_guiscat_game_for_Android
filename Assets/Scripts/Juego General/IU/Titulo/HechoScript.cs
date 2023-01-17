using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HechoScript : MonoBehaviour {

	string addressURLParcial, addressURLFull, user, pass;
	WWW webObject;

	public void Hecho () {

		InputField inputUser = GameObject.Find ("Alias").GetComponent<InputField> ();
		InputField inputPass = GameObject.Find ("Password").GetComponent<InputField> ();
		RegistrarUsuario (inputUser, inputPass);
	}

	public void RegistrarUsuario (InputField iUser, InputField iPass) {

		user = iUser.text;
		pass = iPass.text;
		addressURLParcial = "blaisantka.com/signup/registro.php";
		addressURLFull = addressURLParcial + "?name=" + WWW.EscapeURL(user) + "&pass=" + WWW.EscapeURL(pass);
		BotonHabilitado vaciar = FindObjectOfType<BotonHabilitado> ();
		vaciar.currentChar = 0;
		StartCoroutine (Resultados());
	}

	IEnumerator Resultados () {

		InfoSignAndLogin info = FindObjectOfType<InfoSignAndLogin> ();
		webObject = new WWW (addressURLFull);

		yield return(webObject);

		if (webObject.error != null) {
			info.changeInfo = true;
			info.infoBadOrGood = false;
			info.empiezaContar = true;
			info.texto = "Registro fallido...";
			info.Information ();
		}else{
			info.changeInfo = true;
			info.infoBadOrGood = true;
			info.empiezaContar = true;
			info.texto = "Registro realizado con éxito!";
			info.Information ();
		}

		InputField user = GameObject.Find ("Alias").GetComponent<InputField> ();
		InputField pass = GameObject.Find ("Password").GetComponent<InputField> ();
		user.text = "";
		pass.text = "";
	}
}
