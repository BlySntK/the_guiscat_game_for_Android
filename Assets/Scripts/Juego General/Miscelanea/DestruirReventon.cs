using UnityEngine;
using System.Collections;

public class DestruirReventon : MonoBehaviour {

	//Usamos el método Start para invocar al charco de sangre y para eliminar la explosión de vísceras
	void Start () {

		SalpicarSuelo salpica = GetComponent<SalpicarSuelo> ();
		salpica.CharcoSangre ();
		Destroy (gameObject, .63f);
	}
}
