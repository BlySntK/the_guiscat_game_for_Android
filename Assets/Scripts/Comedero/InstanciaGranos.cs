using UnityEngine;
using System.Collections;

public class InstanciaGranos : MonoBehaviour {

	public GameObject cockie;
	public bool llenar; //Variable manejada tambien por Sonido Pienso
	float tiempoLlenado = 0.24f;
	[HideInInspector]
	public int currentCantidad = 0; //Lo descontaremos en DescontarPienso


	void Update () {

		//Llenamos el comedero
		if (tiempoLlenado > 0)
			tiempoLlenado -= Time.deltaTime;
		else{
			if (llenar) {
				Instantiate (cockie, transform.position, transform.rotation);
				currentCantidad++;

				//Aplicamos un cambio de nombre al GameObject para evitar confusion a la hora de eliminarlos
				GameObject grano = GameObject.Find ("Pienso(Clone)");
				string nombre = "Grano_" + currentCantidad.ToString ("0");
				grano.gameObject.name = nombre;
				//***************************************************************************************
			}

			tiempoLlenado = 0.24f;
		}
	}
}
