using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Llenar : MonoBehaviour {
	
	float tiempoApareceNotificacion = 0; //Segundos que durara la visualizacion antes de quitar alpha
	float tiempoQuitarNotificacion = 5; //Segundos que tardara en activarse el trigger
	//[HideInInspector]
	public float tiempoAparecerGameOver = 10; //Si no se llena el comedero en un máximo de 10 segundos
	float a = 0;
	Image img;
	[HideInInspector]
	public int cont = 0;
	Llenado pienso;



	void Awake () {

		//img = GameObject.Find ("Notificacion").GetComponent<Image> ();
		//img.color = new Color (img.color.r, img.color.g, img.color.b, a);
		pienso = FindObjectOfType<Llenado> ();
		pienso.PararLlenado ();
	}
	
	void Update () {

		/* Llenamos el comedero de pienso si pulsamos el boton indicado */

		//De manera automatica, si volteamos la caja con pienso cayendo, el pienso parara de caer
		VoltearDch volteoDch = FindObjectOfType<VoltearDch> ();
		VoltearIzq volteoIzq = FindObjectOfType<VoltearIzq> ();
		InstanciaGranos granos = FindObjectOfType<InstanciaGranos> ();
		if (volteoIzq.voltear) {
			pienso.PararLlenado ();
			granos.llenar = false;
			cont--;

			if (cont < 0)
				cont = 0;
		}

		//En cuanto la caja esté volteada hacia abajo, el pienso caerá
		if (volteoDch.cajaVolterada) {
			pienso.LlenarComedero ();
			granos.llenar = true;
		}
		/****************************************************************************************/
	}
}
