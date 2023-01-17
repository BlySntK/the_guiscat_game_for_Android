using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfiguracionUI : MonoBehaviour {

	GameObject configuracion, panelConf;
	Button volver, confBoton;
	public Image _configuracion;
	Slider _musica;
	Toggle sonidos, musicas;
	AudioSource _audio, _sonido;
	float alpha, veloalpha = 0.7f;
	bool activarFunciones, musicasPuestas = true, bloquear;


	void Awake () {

		configuracion = GameObject.Find ("Configuracion");
		confBoton = configuracion.GetComponent<Button> ();
		panelConf = GameObject.Find ("PanelConf");
		volver = GameObject.Find ("Volver").GetComponent<Button> ();
		_musica = GameObject.Find ("VolumenGeneral").GetComponent<Slider> ();
		panelConf.SetActive (false);
		_musica.value = 1;
	}

	public void Configuracion () {

		_configuracion = configuracion.GetComponent<Image> ();
		_configuracion.enabled = false;
		panelConf.SetActive (true);
		activarFunciones = true;
	}

	public void Volver () {

		_configuracion = configuracion.GetComponent<Image> ();
		panelConf.SetActive (false);
		_configuracion.enabled = true;
		activarFunciones = false;
	}

	void Update () {
	
		if (activarFunciones) Funcionalidades ();

		//En los siguientes casos...:
//		OcultarInterfaz siAparece = FindObjectOfType<OcultarInterfaz> ();
//		/* SI ES EL INICIO Y NO SE HA PULSADO EL BOTON DE INTERFAZ Y NO PASÓ EL TIEMPO: La interfaz se hará visible y el botón de Configuracion también */
//		if (!siAparece.apareceInterfaz && siAparece.autonomia && siAparece.tempoInterfaz > 0) {
//			Image transparencia = GetComponent<Image> ();
//			if (alpha < 1 && !bloquear) {
//				alpha += veloalpha * Time.deltaTime;
//				transparencia.color = new Color (transparencia.color.r, transparencia.color.g, transparencia.color.b, alpha);
//				confBoton.interactable = true;
//			} else if (alpha >= 1 && !bloquear)
//				bloquear = true;
//		
//		/* 	SI NO SE PULSÓ SOBRE EL BOTON DE LA INTERFAZ Y PASÓ EL TIEMPO: la interfaz se hara invisible así como el boton de configuración */
//		}
//		else if (!siAparece.apareceInterfaz && siAparece.autonomia && siAparece.tempoInterfaz <= 0) {
//			Image transparencia = GetComponent<Image> ();
//			if (alpha > 0 && bloquear) {
//				alpha -= veloalpha * Time.deltaTime;
//				transparencia.color = new Color (transparencia.color.r, transparencia.color.g, transparencia.color.b, alpha);
//				confBoton.interactable = false;
//			} else if (alpha <= 0 && bloquear)
//				bloquear = false;
//		}
		/* SI SE PULSÓ SOBRE EL BOTON DE INTERFAZ: La interfaz se hará visible al igual que el boton de configuración*/
//		else if (siAparece.apareceInterfaz && siAparece.autonomia && siAparece.tempoInterfaz > 0) {
//			Image transparencia = GetComponent<Image> ();
//			if (alpha < 1 && !bloquear) {
//				alpha += veloalpha * Time.deltaTime;
//				transparencia.color = new Color (transparencia.color.r, transparencia.color.g, transparencia.color.b, alpha);
//			} else if (alpha >= 1 && !bloquear)
//				bloquear = true;
//		}
	}

	void Funcionalidades () {

		_audio = GameObject.Find ("Musica").GetComponent<AudioSource> ();
		sonidos = GameObject.Find ("SonidosOn-Off").GetComponent<Toggle> ();
		musicas = GameObject.Find ("MusicasOn-Off").GetComponent<Toggle> ();

		//Si alguno de los checks está o no activado...
		if (!musicas.isOn) {
			_audio.Stop ();
			musicasPuestas = false;
		}else {
			if (!musicasPuestas) {
				_audio.Play ();
				musicasPuestas = true;
			}

			//Deslizamos la musica que se escucha al nivel optimo
			_audio.volume = _musica.value;
		}
	}
}
