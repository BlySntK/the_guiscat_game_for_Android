using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pausa : MonoBehaviour {

	float desapareceBoton = 3, alfa = 0, alfaPulsado = 0.5f;
	bool hacerPausa, apareceBoton;
	ColorBlock colores,colorAlpha;
	Color colorVerde, colorPulsado, colorBlanco, colorGris, colorAlpha_ /* Es el color que debe tener el boton SectorPausa, ser transparente */;
	Button botonPausa; 
	Image botonActivo; 
	public Image flechaVerde;
	Text texto;


	void Start () {
		 //Configurando BotonActivo
		botonActivo = GameObject.Find ("SectorPausa").GetComponent<Image> ();
		colorAlpha_ = new Color (0, 0, 0, 0);
		botonActivo.color = colorAlpha_;

		//Configurando flecha verde
		flechaVerde = GameObject.Find ("FlechaVerde").GetComponent<Image> ();
		flechaVerde.enabled = false;

		//Configurando BotonPausa
		botonPausa = gameObject.GetComponent<Button> ();
		texto = botonPausa.GetComponentInChildren<Text> ();
		colorVerde = new Color (0, 1, 0, 0);
		texto.enabled = false;
		colores.normalColor = colorVerde;
		botonPausa.colors = colores;
		alfa = botonPausa.colors.normalColor.a;

	}

	public void ActivarBoton () {

		OcultarInterfaz noMostrarPausa = FindObjectOfType<OcultarInterfaz> ();

		#if UNITY_ANDROID_API
			if (Input.touchCount > 0 && flechaVerde.enabled) {
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
					RaycastHit hit;
					if (Physics.Raycast (ray, out hit)) {
						if (botonActivo.name == "SectorPausa")
							apareceBoton = true;
					}
				}
			}
		#endif

		#if UNITY_EDITOR
			if (Input.GetButton("Fire1")) {
				if (botonActivo.name == "SectorPausa")
					apareceBoton = true;
			}
		#endif
	}

	void Update () {

		if (apareceBoton) {
			if (alfa < 1) {
				alfa += Time.deltaTime;
				colorVerde = new Color (0, 1, 0, alfa);
				colores.normalColor = colorVerde;
				colores.highlightedColor = Color.white;
				colores.pressedColor = Color.white;
				colores.disabledColor = Color.white;
				colores.colorMultiplier = 1;
				botonPausa.colors = colores;
			}else{
				texto.enabled = true;
				if (desapareceBoton > 0)
					desapareceBoton -= Time.deltaTime;
				else{
					apareceBoton = false;
					desapareceBoton = 3;
				}
			}
		}else{
			if (botonPausa.colors.pressedColor.a > 0 && alfa == 0) alfa = botonPausa.colors.pressedColor.a;
			if (alfa > 0) {
				alfa -= Time.deltaTime;
				colorVerde = new Color (0, 1, 0, alfa);

				if (alfaPulsado > 0)
					alfaPulsado -= Time.deltaTime;

				colorPulsado = new Color (0.5f, 0.5f, 0.5f, alfaPulsado);
				colorBlanco = new Color (1, 1, 1, alfa);
				colores.normalColor = colorVerde;
				colores.pressedColor = colorPulsado;
				colores.highlightedColor = colorBlanco;
				colores.pressedColor = colorBlanco;
				colores.disabledColor = colorBlanco;
				colores.colorMultiplier = 1;
				botonPausa.colors = colores;
			}else
				texto.enabled = false;
		}
	}

	public void HacerPausa () {

		if (apareceBoton) {
			hacerPausa = !hacerPausa;

			colorGris = Color.grey;
			colores.pressedColor = colorGris;
			botonPausa.colors = colores;

			if (hacerPausa)
				Time.timeScale = 0;
			else
				Time.timeScale = 1;
		}
	}
}
