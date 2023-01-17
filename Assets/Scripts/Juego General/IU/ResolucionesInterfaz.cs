using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResolucionesInterfaz : MonoBehaviour {

	public RectTransform Contenedor;
	Camera cam;

	void Update () {

		cam = GetComponent<Camera> ();
		float x = cam.aspect; //Obtiene el aspecto actual de la camara
		Contenedor.localScale = new Vector3(x, 1f, 1f); //Re-escala el contenedor
	}
}
