using UnityEngine;
using System.Collections;

public class GatoDesaparece : MonoBehaviour {

	SkinnedMeshRenderer skin;
	float alpha, speedAlpha = 0.5f;
	float tiempoParaDesaparecer = 5;
	public bool tutorial; //If ejecutado en caso de hacer el tutorial

	void Awake () {

		skin = GetComponent<SkinnedMeshRenderer> ();
		alpha = skin.material.color.a;
	}

	//No son necesarios los parametros a utilizar pero sí útiles en segun que momentos
	public void Desaparecer (GameObject padre = null) {

		if (alpha > 0) {
			alpha -= speedAlpha * Time.deltaTime;
			skin.material.color = new Color (1, 1, 1, alpha);
		}else{
			if (tutorial) {
				GatitoTutoScript gatito = padre.GetComponent<GatitoTutoScript> ();
				AparecerGatos aparece = FindObjectOfType<AparecerGatos> ();
				aparece.contarGato = false;
				aparece.instanciar = true;
				gatito.llegaPuntoFin = false;
			}
			Destroy (padre);
		}
	}
}
