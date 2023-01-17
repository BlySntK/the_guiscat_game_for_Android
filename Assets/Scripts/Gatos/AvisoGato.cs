using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AvisoGato : MonoBehaviour {

	float aparecerCartel = 0, tiempoAparecer = 0.4f;
	const float width = 361, heigth = 162;
	bool bloquear, yes;
	Image info, exclamation;
	Exclamation_II _exclamation;


	void Start () {

		info = GetComponent<Image> ();
		RectTransform rect = GetComponent<RectTransform> ();
		rect.sizeDelta =  new Vector2 (width, heigth);
		info.fillAmount = 0;
		info.fillClockwise = false;
		exclamation = GameObject.Find ("Exclamacion_II").GetComponent<Image> ();
		_exclamation = FindObjectOfType<Exclamation_II> ();
		exclamation.enabled = false;
	}

	public void LanzarAviso () {

		if (aparecerCartel < 1 && !yes && !bloquear) {
			aparecerCartel += tiempoAparecer * Time.deltaTime;
			info.fillAmount = aparecerCartel;
			EmpezarPartida variableGlobal = FindObjectOfType<EmpezarPartida> ();
			variableGlobal.frenarGatosUnaVez = true;
		}else if (aparecerCartel >= 0.9f && !yes && !bloquear) {
			exclamation.enabled = true;
			Exclamation_II _exclamation = FindObjectOfType<Exclamation_II> ();
			_exclamation.Exclamacion ();
			bloquear = true;
		}

		if (aparecerCartel > 0 && yes && bloquear) {
			aparecerCartel -= tiempoAparecer * Time.deltaTime;
			info.fillAmount = aparecerCartel;
			exclamation.enabled = false;
			_exclamation.Exclamacion ();
		}else if (aparecerCartel <= 0 && yes && bloquear) {
			bloquear = false;
			yes = false;
			EmpezarPartida variableGlobal = FindObjectOfType<EmpezarPartida> ();
			variableGlobal.frenarGatosUnaVez = false;
			variableGlobal.noLanzarAviso = true;
		}

		if (!yes && bloquear) {
			_exclamation.Exclamacion ();
			CanvasGroup interacturar = GetComponent<CanvasGroup> ();
			interacturar.blocksRaycasts = true;
		}
	}

	public void Continuar () {

		if (aparecerCartel >= 0.9f && !yes && bloquear) {
			CanvasGroup interacturar = GetComponent<CanvasGroup> ();
			Destroy (interacturar);
			yes = true;
		}
	}
}
