using UnityEngine;
using System.Collections;

public class GatitoCaminaScript : MonoBehaviour {
	
	float andar = 25.8f;
	int distanciaDedo = 1000000;
	float speed = 50.5f; //Velocidad a la que se mueve la barra de Salud del gato
	float currentSalud, tiempo = .4f;

	//Variables para controlar los tiempos de disparo
	const float DISPARO_MIN = .2f;
	float tiempoDisparo = 0;
	public const int CANTIDAD_COMIDA = 4;
	public int currentCantidadComida = 0;
	Transform comedero, exterior;
	Animator paraAComer;
	SaludGatoCamina salud;
	Vector3 screenPoint;
	public bool llego, frenar, volverAtras /* Variable booleana que indicara al gato que se debe ir */;
	RaycastHit hit;




	void Start () {

		salud = FindObjectOfType<SaludGatoCamina> ();
		currentSalud = salud.Salud ();
	}

	void Update () {

		EmpezarPartida variableGlobal = FindObjectOfType<EmpezarPartida> ();
		if (variableGlobal.frenarGatosUnaVez)
			frenar = variableGlobal.frenarGatosUnaVez;
		else
			frenar = variableGlobal.frenarGatosUnaVez;

		//Estas instrucciones son para mover al gato con una limitada velocidad
		if (!llego && !frenar)
			transform.Translate (0, 0, andar * Time.deltaTime/0.1f);
		//*********************************************************************

		//El gato ira andando por defecto al comedero
		comedero = GameObject.Find ("Comedero").transform;
		exterior = GameObject.Find ("Exterior").transform;
		PiensoDentro granos = FindObjectOfType<PiensoDentro> ();
		if (granos.cantidad > 0 && !volverAtras)
			transform.LookAt (comedero);
		else if (granos.cantidad > 0 && volverAtras) {
			transform.LookAt (exterior);

			GatoDesaparece desaparece = gameObject.GetComponentInChildren<GatoDesaparece> ();
			desaparece.Desaparecer (gameObject);
		}else if (granos.cantidad <= 0 && volverAtras || !volverAtras) {
			transform.LookAt (exterior);
			GatoDesaparece desaparece = gameObject.GetComponentInChildren<GatoDesaparece> ();
			desaparece.Desaparecer (gameObject);
		}
		//****************************************************

		//Si lo tocamos, le haremos daño hasta reventarlo (Siempre que haya pienso que comer)
		if (granos.cantidad > 0) {

			#if UNITY_ANDROID
				if (Input.touchCount > 0) {
					if (Input.GetTouch(0).phase == TouchPhase.Began) {
						Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
						if (Physics.Raycast(ray, out hit, distanciaDedo)) {
							if (hit.collider.name == gameObject.name) {	
								//En cuanto le demos a un gato, deberá escucharse sonidos de disparos
								Metralladora metralla = FindObjectOfType<Metralladora> ();

								//Si estamos poco tiempo, sólo disparará una vez
								if (tiempoDisparo <= DISPARO_MIN)
									metralla.DispararUnaVez ();
								else if (tiempoDisparo > DISPARO_MIN)
									metralla.DispararLoop ();

								//Aplicamos daño al gato
								if (currentSalud > 0) {
									currentSalud -= speed * Time.deltaTime;
									salud.Salud (currentSalud);
								}else
									salud.Salud (currentSalud, gameObject, tiempo);
							}
						}
					}
				}
			#elif UNITY_EDITOR
				if (Input.GetButtonDown ("Fire1")) {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					if (Physics.Raycast(ray, out hit)) {
						if (hit.collider.name == gameObject.name) {	
							//En cuanto le demos a un gato, deberá escucharse sonidos de disparos
							Metralladora metralla = FindObjectOfType<Metralladora> ();

							//Si estamos poco tiempo, sólo disparará una vez
							if (tiempoDisparo <= DISPARO_MIN)
								metralla.DispararUnaVez ();
							else if (tiempoDisparo > DISPARO_MIN)
								metralla.DispararLoop ();

							//Aplicamos daño al gato
							if (currentSalud > 0) {
								currentSalud -= speed * Time.deltaTime;
								salud.Salud (currentSalud);
							}else
								salud.Salud (currentSalud, gameObject, tiempo);
						}
					}
				}
			#elif UNITY_STANDALONE_WIN
				if (Input.GetButtonDown ("Fire1")) {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					if (Physics.Raycast(ray, out hit)) {
						if (hit.collider.name == gameObject.name) {	
							//En cuanto le demos a un gato, deberá escucharse sonidos de disparos
							Metralladora metralla = FindObjectOfType<Metralladora> ();

							//Si estamos poco tiempo, sólo disparará una vez
							if (tiempoDisparo <= DISPARO_MIN)
								metralla.DispararUnaVez ();
							else if (tiempoDisparo > DISPARO_MIN)
								metralla.DispararLoop ();

							//Aplicamos daño al gato
							if (currentSalud > 0) {
								currentSalud -= speed * Time.deltaTime;
								salud.Salud (currentSalud);
							}else
								salud.Salud (currentSalud, gameObject, tiempo);
						}
					}
				}
			#endif
		}

		if (llego && variableGlobal.noLanzarAviso || !variableGlobal.noLanzarAviso) {
			paraAComer = GetComponent<Animator> ();
			paraAComer.SetBool ("Parar", true);
		}

		//En el caso de que no haya gatos corredores y esten parados los caminantes:
		GatitoCorreScript hayGatos = FindObjectOfType<GatitoCorreScript> ();
		if (hayGatos == null && !frenar && !llego) {
			Animator frenarGatoAnda = GetComponent<Animator> ();
			frenarGatoAnda.SetBool ("Parar", false);
		
		/* En los casos en los que hayan o no desaparecido los gatos, por diferentes motivos y 
		 * esten parados los caminantes, les permitiremos caminar
		 */
		}else if (hayGatos == null && frenar && variableGlobal.noLanzarAviso && !llego) {
			Animator frenarGatoAnda = GetComponent<Animator> ();
			frenarGatoAnda.SetBool ("Parar", false);
		}else if (frenar && hayGatos != null && variableGlobal.noLanzarAviso && !llego) {
			Animator frenarGatoAnda = GetComponent<Animator> ();
			frenarGatoAnda.SetBool ("Parar", false);
		}else if (hayGatos != null && !frenar && variableGlobal.noLanzarAviso && !llego) {
			Animator frenarGatoAnda = GetComponent<Animator> ();
			frenarGatoAnda.SetBool ("Parar", false);
		}
			
		//**********************************************************
	}

	public void FrenarGatoAnda () {

		if (frenar) {
			Animator frenarGatoAnda = GetComponent<Animator> ();
			frenarGatoAnda.SetBool ("Parar", true);
		}else{
			Animator frenarGatoAnda = GetComponent<Animator> ();
			frenarGatoAnda.SetBool ("Parar", false);
		}
	}
}
