using UnityEngine;
using System.Collections;

public class GatitoCorreScript : MonoBehaviour {

	float correr = 92.8f;
	int distanciaDedo = 1000000;
	float currentSalud, tiempo = .4f;
	public const int CANTIDAD_COMIDA = 10; //Constante que sirve para controlar la cantidad especifica de pienso que come
	public int currentCantidadComida = 0;
	Transform comedero, exterior;
	Animator pararAComer;
	PiensoDentro granos;
	Vector3 screenPoint;
	public bool llego, huir, frenar;
	RaycastHit hit;


	void Awake () {
	
		EmpezarPartida variablesGlobales = FindObjectOfType<EmpezarPartida> ();
		variablesGlobales.infoCatRun.SetActive (true);
	}

	void Update () {

		EmpezarPartida variableGlobal = FindObjectOfType<EmpezarPartida> ();
		if (variableGlobal.frenarGatosUnaVez)
			frenar = variableGlobal.frenarGatosUnaVez;
		else
			frenar = variableGlobal.frenarGatosUnaVez;

		//Mientras no estemos frenando el avance de los gatos:
		if (!frenar && !llego) {
			//Esta instrucción es para mover al gato con una limitada velocidad
			transform.Translate (0, 0, correr * Time.deltaTime / 0.1f);

			//El gato ira andando por defecto al comedero
			comedero = GameObject.Find ("Comedero").transform;
			exterior = GameObject.Find ("Exterior").transform;
			granos = FindObjectOfType<PiensoDentro> ();

			//Sin embargo, si llega al comedero NO huirá
			if (granos.cantidad > 0 && !huir)
				transform.LookAt (comedero);
			else if (huir && !llego) {
				transform.LookAt (exterior);
				GatoDesaparece desaparece = gameObject.GetComponentInChildren<GatoDesaparece> ();
				desaparece.Desaparecer (gameObject);
			}

			Animator frenarGatoCorre = GetComponent<Animator> ();
			frenarGatoCorre.SetBool ("Parar", false);
			GatitoCaminaScript frenarGato = FindObjectOfType<GatitoCaminaScript> ();

			//Nos aseguramos que existe al menos un gato andando
			if (frenarGato != null) {
				frenarGato.frenar = false;
				frenarGato.FrenarGatoAnda ();
			}
			//****************************************************
		}

		//Si lo tocamos, haremos que huya (siempre que no haya llegado al comedero)
		granos = FindObjectOfType<PiensoDentro> ();
		if (granos.cantidad > 0 && !llego) {

			#if UNITY_ANDROID
				if (Input.touchCount > 0) {
					if (Input.GetTouch (0).phase == TouchPhase.Began) {
						Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
						if (Physics.Raycast (ray, out hit, distanciaDedo)) {
							if (hit.collider.name == gameObject.name)
								huir = true;
						}
					}
				}
			#elif UNITY_EDITOR
				if (Input.GetButton("Fire1")) {
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray, out hit)) {
						if (hit.collider.name == gameObject.name)
							huir = true;
					}
				}
			#endif
		
		//Si no lo tocamos o sí lo hacemos pero ya es tarde, huirá
		}else if (granos.cantidad <= 0 && llego) {
			huir = true;
			Animator frenarGatoCorre = GetComponent<Animator> ();
			frenarGatoCorre.SetBool ("Parar", true);

			//Mmover al gato con una limitada velocidad
			transform.Translate (0, 0, correr * Time.deltaTime / 0.1f);
		
		//Si no lo tocamos y se termina el pienso, huirá
		}else if (granos.cantidad <= 0 && !llego) {
			huir = true;
			Animator frenarGatoCorre = GetComponent<Animator> ();
			frenarGatoCorre.SetBool ("Parar", true);

			//Mmover al gato con una limitada velocidad
			transform.Translate (0, 0, correr * Time.deltaTime / 0.1f);
		}

		//Si huye, despues de haber comido, desaparecerá
		if (huir && llego) {
			transform.LookAt (exterior);
			GatoDesaparece desaparece = gameObject.GetComponentInChildren<GatoDesaparece> ();
			desaparece.Desaparecer (gameObject);
		}

		//Si ya ha comido su ración, desaparecerá sin huir
		if (currentCantidadComida >= CANTIDAD_COMIDA) {
			GatoDesaparece desaparece = gameObject.GetComponentInChildren<GatoDesaparece> ();
			desaparece.Desaparecer (gameObject);
		}

		//En el caso de que aparezca el primer gato corriendo y deba pararse:
		if (frenar && !llego) {
			Animator frenarGatoCorre = GetComponent<Animator> ();
			frenarGatoCorre.SetBool ("Parar", true);
			GatitoCaminaScript frenarGato = FindObjectOfType<GatitoCaminaScript> ();

			//Nos aseguramos que existe al menos un gato andando
			if (frenarGato != null) {
				frenarGato.frenar = true;
				frenarGato.FrenarGatoAnda ();
			}
		}

		//Si ha llegado al comedero, parará
		if (!frenar && llego) {
			Animator frenarGatoCorre = GetComponent<Animator> ();
			frenarGatoCorre.SetBool ("Parar", true);
		}
			
		//**********************************************************

		//Instrucciones para frenar a todos los gatos, solo una vez
		variableGlobal = FindObjectOfType<EmpezarPartida> ();
		if (!variableGlobal.noLanzarAviso) {
			AvisoGato aviso = GameObject.Find ("AvisoGatoCorre").GetComponent<AvisoGato> ();

			//Método con el que se pondra a verdadero auxAviso
			aviso.LanzarAviso ();
		}
	}
}
