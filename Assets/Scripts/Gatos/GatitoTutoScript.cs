using UnityEngine;
using System.Collections;

public class GatitoTutoScript : MonoBehaviour {

	float andar = 25.8f;
	int distanciaDedo = 1000000;
	public int contCat = 0; //Controlamos que los gatos del tuto casquen para contarlos
	float speed = 50.5f; //Velocidad a la que se mueve la barra de Salud del gato
	float currentSalud, tiempo = .4f;

	//Variables para controlar los tiempos de disparo
	const float DISPARO_MIN = .2f;
	float tiempoDisparo = 0;
	public const int CANTIDAD_COMIDA = 4;
	public int currentCantidadComida = 0;
	Transform puntoFin;
	Animator paraAComer;
	SaludGatoCamina salud;
	Vector3 screenPoint;
	public bool llegaPuntoFin;
	AparecerGatos aparece;
	RaycastHit hit;




	void Start () {

		salud = FindObjectOfType<SaludGatoCamina> ();
		aparece = FindObjectOfType<AparecerGatos> ();
		currentSalud = salud.Salud ();
	}

	void Update () {

		//Estas instrucciones son para mover al gato con una limitada velocidad
		transform.Translate (0, 0, andar * Time.deltaTime/0.1f);
		//*********************************************************************

		//El gato ira andando por defecto al comedero
		puntoFin = GameObject.Find ("PuntoFin").transform;
		transform.LookAt (puntoFin);

		if (llegaPuntoFin) {
			GatoDesaparece desaparece = GetComponentInChildren<GatoDesaparece> ();
			desaparece.Desaparecer (gameObject);
		}
		//****************************************************

		//Si lo tocamos, le haremos daño hasta reventarlo (Siempre que haya pienso que comer)
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
		#endif
		//**********************************************************
	}

	void OnTriggerEnter (Collider col) {

		if (col.gameObject.name.Contains ("PuntoFin"))
			llegaPuntoFin = true;
	}
}
