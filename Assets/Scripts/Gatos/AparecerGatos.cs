using UnityEngine;
using System.Collections;

public class AparecerGatos : MonoBehaviour {
	
	public GameObject gatitoCamina, gatitoCorre, gatitoTuto;
	int randomGato; //Elección aleatoria de instancia de gatos
	bool accion;
	string nombreCamina; //Nuevo nombre para los gameObject instanciados que camiinen
	string nombreCorre; //Nuevo nombre para los gameObject instanciados que corran
	[HideInInspector]
	public bool comienzaLaHorda; //Daremos inicio a la intempestiva jauria gatuna cuando comencemos a llenar el comedero
	//[HideInInspector]
	public bool contarGato, instanciar; //Control(es) de salida de gatos en el tuto
	Vector3 posicion, posicionTuto;
	GatitoTutoScript gatito;
	EmpezarPartida variablesGlobales;

	//Coordenadas maestras de los gatos
	float coordenadaX;
	float coordenadaY = -3;
	float coordenadaZ;
	//*******************************//

	//Coordenadas tutorial
	const float coordenadaXTuto = 583;
	const float coordenadaYTuto = 15;
	const float coordenadaZTuto = -455;
	//*******************************//

	float temp; //Tiempo trancurrido entre gato y gato (respawn en nivel normal)
	float tiempoSalida = 1.5f; //Tiempo de salida de gatos en tutorial
	public int gatoTuto = 0;
	[HideInInspector]
	public int n_gatos_default = 500;
	int currentCat;


	void Start () {
	
		variablesGlobales = FindObjectOfType<EmpezarPartida> ();
		if (variablesGlobales.hacerTutorial) {
			contarGato = true;
			instanciar = true;
			GatosTutorial ();
		}
		temp = 2;
	}

	void Update () {

		randomGato = Random.Range (1, 120);
		PiensoDentro hayPienso = FindObjectOfType<PiensoDentro> ();
		if (comienzaLaHorda && hayPienso.cantidad > 0) {
			if (temp > 0)
				temp -= Time.deltaTime;
			else {
				EmpezarPartida frenada = FindObjectOfType<EmpezarPartida> ();
				if (currentCat < n_gatos_default && !frenada.frenarGatosUnaVez) {
					coordenadaX = Random.Range (-546, 1083);
					coordenadaZ = Random.Range (1858, 312);
					posicion = new Vector3 (coordenadaX, coordenadaY, coordenadaZ);

					//Eleccion felina:
					if (randomGato > 12 && randomGato < 19 && temp <= 0) {
						accion = true;
						temp = 10;
					}else if (temp <= 0){
						accion = false;
						temp = 2;
					}
					
					//Si la accion es true, correrá, si es false andará, se decide con este switch
					switch (accion) {

					case true:
						Instantiate (gatitoCorre, posicion, transform.rotation);
						currentCat++;
						variablesGlobales.n_gato = currentCat;

						//Aplicamos un cambio de nombre al GameObject para evitar confusion a la hora de matarlos
						GameObject gatoCorre = GameObject.Find ("GatitoCorre(Clone)");
						nombreCorre = "GatitoCorre_" + currentCat.ToString ("0");
						gatoCorre.gameObject.name = nombreCorre;
						break;
					
					case false:
						Instantiate (gatitoCamina, posicion, transform.rotation);
						currentCat++;
						variablesGlobales.n_gato = currentCat;

						//Aplicamos un cambio de nombre al GameObject para evitar confusion a la hora de matarlos
						GameObject gatoAnda = GameObject.Find ("GatitoCamina(Clone)");
						nombreCamina = "GatitoCamina_" + currentCat.ToString ("0");
						gatoAnda.gameObject.name = nombreCamina;
						break;
					}
					//***************************************************************************************

				}
			}
		}

		//Temporizador tutorial
		if (tiempoSalida > 0 && variablesGlobales.hacerTutorial && !variablesGlobales.tutorialHecho)
			tiempoSalida -= Time.deltaTime;
		else if (tiempoSalida < 0 && variablesGlobales.hacerTutorial && !variablesGlobales.tutorialHecho) {
			if (gatoTuto < 5)
				GatosTutorial ();
			else if (gatoTuto == 5)
				UltimoGato ();
		}
	}

	void GatosTutorial () {
		
		posicionTuto = new Vector3 (coordenadaXTuto, coordenadaYTuto, coordenadaZTuto);

		if (instanciar && contarGato) {
			instanciar = false;
			contarGato = false;
			Instantiate (gatitoTuto, posicionTuto, transform.rotation);
			gatoTuto++;
		}else if (instanciar && !contarGato) {
			instanciar = false;
			Instantiate (gatitoTuto, posicionTuto, transform.rotation);
			if (gatoTuto == 1)
				gatoTuto = 1;
			else if (gatoTuto == 2)
				gatoTuto = 2;
			else if (gatoTuto == 3)
				gatoTuto = 3;
			else if (gatoTuto == 4)
				gatoTuto = 4;
			else if (gatoTuto == 5)
				gatoTuto = 5;
		}
		tiempoSalida = 1.5f;
	}

	void UltimoGato () {

		if (instanciar && !contarGato) {
			instanciar = false;
			Instantiate (gatitoTuto, posicionTuto, transform.rotation);
			if (gatoTuto == 5)
				gatoTuto = 5;

			if (instanciar && contarGato) {
				instanciar = false;
				contarGato = false;
			}
		}
		tiempoSalida = 1.5f;
	}
}
