using UnityEngine;
using System.Collections;

public class SalpicarSuelo : MonoBehaviour {

	public GameObject charco;

	//Método invocado desde la destrucción del reventon
	public void CharcoSangre () {

		Instantiate (charco, transform.position, transform.rotation);
	}
}
