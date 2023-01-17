using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EmpezarPartida : MonoBehaviour {

	//Variables de esta clase no dependientes
	Image info, img;
	OcultarInterfaz ocultar;
	Puntuar p;
	Llenar llenarCaja;
	const float posXCartel_1 = -123.8f, posYCartel_1 = 1;
	const float posXCartel_2 = -3.81f, posYCartel_2 = 1.30f;
	const float width_1 = 325.9f, height_1 = 337.4f;
	const float width_resto = 361, height_resto = 337;
	float aparecerI = 0;
	float aparecerII = 0;
	float aparecerIII = 0;
	float time = 1.4f; //Tiempo aparecer informacion
	float _puntos;
	bool desaparecerPrimero;
	bool desaparecerSegundo;
	bool desaparecerTercero;
	bool desbloqueoI, desbloqueoII, desbloqueoIII;
	bool puntuar; 
	public bool avisoLlenar;
	Text puntos;
	[HideInInspector]
	public GameObject infoCatRun;
	RectTransform infoTransform;

	//Variables globales dependientes
	public bool frenarGatosUnaVez; 
	public bool hacerTutorial;
	public bool tutorialHecho;
	public bool noLanzarAviso; //Variable para que no se vuelva a visualizar el aviso de gatos corriendo
	public bool noMasTutorial; //Variable diseñada para que no aparezca más el tutorial del principio (cambiada en ScriptGameOver)
	public int contadorShakes = 0;
	public Sprite[] texture;
	public int n_gato; //Variable que determina si hay gatos en la escena
	public int numeroGatos = 0; //Variable de la cantidad total de gatos eliminados



	void Start () {

		ocultar = (OcultarInterfaz) FindObjectOfType<OcultarInterfaz> ();
		llenarCaja = (Llenar) FindObjectOfType<Llenar> ();
		llenarCaja.enabled = false;
		ocultar.Tutorial ();
		info = GameObject.Find ("Info").GetComponent<Image> ();
		infoTransform = GameObject.Find ("Info").GetComponent<RectTransform> ();
		infoTransform.sizeDelta = new Vector2 (width_1, height_1);
		p = (Puntuar) FindObjectOfType<Puntuar> ();
		info.enabled = true;
		desbloqueoI = true;
		info.sprite = texture[0];
		info.fillAmount = 0;
		info.fillClockwise = false;
		info.fillAmount = 0;
		info.fillClockwise = false;
		img = GameObject.Find ("Exclamacion_I").GetComponent<Image> ();
		img.enabled = false;
		infoCatRun = GameObject.Find ("AvisoGatoCorre");
		infoCatRun.SetActive (false);
		infoTransform.localPosition = new Vector3 (posXCartel_1, posYCartel_1);
	}

	void Update () {

		ocultar = FindObjectOfType<OcultarInterfaz> ();
		if (!avisoLlenar && info != null && hacerTutorial)
			CartelesInfo ();
		else if (!avisoLlenar && !hacerTutorial) {
			tutorialHecho = true;
			llenarCaja.enabled = true;
			ocultar.Tutorial ();
			avisoLlenar = true;
		}else if (info == null) {}
	}
	
	public void Comenzar () {

		if (aparecerI >= 0.9f && !desaparecerPrimero && desbloqueoI) {
			desaparecerPrimero = true;
			desbloqueoI = false;
			desbloqueoII = true;
		}else if (aparecerII >= 0.9f && !desaparecerSegundo && desbloqueoII) {
			desaparecerSegundo = true;
			desbloqueoII = false;
			desbloqueoIII = true;
		}else if (aparecerIII >= 0.9f && !desaparecerTercero && desbloqueoIII) {
			desaparecerTercero = true;
			desbloqueoIII = false;
		}
	}

	void CartelesInfo () {

		/**La primera instancia es las diferentes infos con nuestra misión, 
		 * cómo hacer pausa y la dosis de puntos con los que se empiezan (respectivamente):
		 **/

		//Aparece el primer cartel:
		Exclamation_I exclamacion = FindObjectOfType<Exclamation_I> ();
		if (info.enabled && !desaparecerPrimero && desbloqueoI) {
			if (aparecerI < 1) {
				aparecerI += time * Time.deltaTime;
				info.fillAmount = aparecerI;
			}

			if (info.fillAmount >= 1) {
				img.enabled = true;
				exclamacion.Exclamacion ();
			}
		
		//En cuanto el primer cartel desaparece, da paso al segundo cartel
		}else if (info.enabled && !desaparecerSegundo && desbloqueoII) {
			if (aparecerII < 1 && aparecerI <= 0) {
				aparecerII += time * Time.deltaTime;
				info.fillAmount = aparecerII;
			}else if (aparecerII >= 1 && aparecerI <= 0) {
				Pausa pause = FindObjectOfType<Pausa> ();
				pause.flechaVerde.enabled = true;
			}
		
		//Cuando lo hace el segundo, da paso al tercero y último
		}else if (info.enabled && !desaparecerTercero && desbloqueoIII) {
			if (aparecerIII < 1 && aparecerII <= 0) {
				aparecerIII += time * Time.deltaTime;
				info.fillAmount = aparecerIII;
			}
		}
		/****************************************************************************/
		
		//Con estos condicionales hacemos el barrido de los carteles para hacerlos desaparecer
		//y, ademas, cambiamos las imagenes de los carteles.
		if (desaparecerPrimero) {
			if (aparecerI > 0) {
				aparecerI -= time * Time.deltaTime;
				info.fillAmount = aparecerI;

				if (info.fillAmount < 1) {
					img.enabled = false;
					exclamacion.Exclamacion ();
				}
			}else if (aparecerI <= 0) {
				info.sprite = texture [1];
				desaparecerPrimero = false;
				infoTransform.localPosition = new Vector3 (posXCartel_2, posYCartel_2);
				infoTransform.sizeDelta = new Vector2 (width_resto, height_resto);
			}
		}else if (desaparecerSegundo) {
			if (aparecerII > 0) {
				aparecerII -= time * Time.deltaTime;
				info.fillAmount = aparecerII;
			}else if (aparecerII <= 0) {
				info.sprite = texture [2];
				desaparecerSegundo = false;

				/* Desaparecer flecha verde */
				Pausa pause = FindObjectOfType<Pausa> ();
				pause.flechaVerde.enabled = false;
			}
		}else if (desaparecerTercero) {
			if (aparecerIII > 0) {
				aparecerIII -= time * Time.deltaTime;
				info.fillAmount = aparecerIII;
			}else if (aparecerIII <= 0) {
				desaparecerTercero = false;
				llenarCaja.enabled = true;
				ocultar = FindObjectOfType<OcultarInterfaz> ();
				tutorialHecho = true;
				ocultar.Tutorial ();
				info.enabled = false;
				avisoLlenar = true;
			}
		}
	}
}
