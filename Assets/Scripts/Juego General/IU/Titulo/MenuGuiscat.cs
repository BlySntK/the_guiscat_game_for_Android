using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuGuiscat : MonoBehaviour {

	FadeOFFGuiscat continuar;
	float escalar = 510;
	const float ancho = 255;

	//Constantes de posiciones finales; Guiscat y gato herido
	const float posYFinal = 110, posXFinal = 145, catInjuredPosXFinal = -46;

	//Constante posicion inicial gato herido
	const float PosInjuredCatIniX = -350;

	//Posiciones variables
	float currentPosInjuredCat = 0, currentPosX = 0, currentPosY = 0;
	float maxEscala = 190, minEscala = 510; 

	//Velocidades
	float velocidadEscala = 220, velocidadPosX = 90, velocidadPosY = 70, 
			veloPosCat = 155, veloIcons = 30;
	Image imgGuiscat;

	//Guiscat positions init:
	float posXIni = 0, posYIni = 27, posZIni = 0;

	//Alphas
	float alpha = 0, alphaImgCat_1 = 0, alphaImgCat_2 = 0, alphaCat = 0, alphaText = 0;
	bool turn; 
	public bool parpadearOtraVez;
	bool visualizarImg_1, visualizarImg_2;

	//Variables de acceso a las transform
	RectTransform catPos, back, 
					nP, rect;
	Image nuevaPartida, volver, catIcon_Nueva,
			catIcon_Volver, cat;
	Text nueva_, volver_;
	Button botonNueva, botonVolver;
	public bool volvemos;

	void Update () {

		/* 
		 * En estas líneas de abajo vamos a hacer transición de la pantalla inicial al menu.
		 * Tendremos que darle a la pantalla para acceder a él.
		 */
		continuar = FindObjectOfType<FadeOFFGuiscat> ();
		nuevaPartida = GameObject.Find ("NuevaPartida").GetComponent<Image> ();
		volver = GameObject.Find ("Volver").GetComponent<Image> ();
		catIcon_Nueva = GameObject.Find ("CatPic_1").GetComponent<Image> ();
		catIcon_Volver = GameObject.Find ("CatPic_2").GetComponent<Image> ();
		cat = GameObject.Find ("InjuredCat").GetComponent<Image> ();
		nueva_ = GameObject.Find ("NuevaText").GetComponent<Text> ();
		volver_ = GameObject.Find ("VolverText").GetComponent<Text> ();
		botonNueva = GameObject.Find ("NuevaPartida").GetComponent<Button> ();
		botonVolver = GameObject.Find ("Volver").GetComponent<Button> ();
		if (continuar != null) {
			if (!continuar.continuar) {
				imgGuiscat = GameObject.Find ("Title").GetComponent<Image> ();
				imgGuiscat.transform.localPosition = new Vector3 (posXIni, posYIni, posZIni);
				nuevaPartida.color = new Color (nuevaPartida.color.r, nuevaPartida.color.g, nuevaPartida.color.b, 0);
				nP = GameObject.Find ("NuevaPartida").GetComponent<RectTransform> ();
				back = GameObject.Find ("Volver").GetComponent<RectTransform> ();
				catPos = GameObject.Find ("InjuredCat").GetComponent<RectTransform> ();
				nP.localPosition = new Vector3 (142, -66, 0);
				back.localPosition = new Vector3 (142, -112, 0);
				catPos.localPosition = new Vector3 (PosInjuredCatIniX, 0, 0);
				volver.color = new Color (volver.color.r, volver.color.g, volver.color.b, 0);
				catIcon_Nueva.color = new Color (catIcon_Nueva.color.r, catIcon_Nueva.color.g, catIcon_Nueva.color.b, 0);
				catIcon_Volver.color = new Color (catIcon_Volver.color.r, catIcon_Volver.color.g, catIcon_Volver.color.b, 0);
				cat.color = new Color (cat.color.r, cat.color.g, cat.color.b, 0);
				nueva_.color = new Color (nueva_.color.r, nueva_.color.g, nueva_.color.b, 0);
				volver_.color = new Color (volver_.color.r, volver_.color.g, volver_.color.b, 0);
				botonNueva.enabled = false;
				botonVolver.enabled = false;

			}else{
				if (!volvemos) {
					botonNueva.enabled = true;
					botonVolver.enabled = true;
					imgGuiscat = GameObject.Find ("Title").GetComponent<Image> ();
					rect = imgGuiscat.GetComponent<RectTransform> ();

					if (escalar > maxEscala) {
						escalar -= velocidadEscala * Time.deltaTime;
						rect.sizeDelta = new Vector2 (escalar, ancho);
					}

					if (currentPosY == 0)
						currentPosY = posYIni;
					else{
						if (currentPosY < posYFinal) {
							currentPosY += velocidadPosY * Time.deltaTime;
							rect.localPosition = new Vector3 (currentPosX, currentPosY, posZIni);
						}
					}

					if (currentPosX < posXFinal) {
						currentPosX += velocidadPosX * Time.deltaTime;
						rect.localPosition = new Vector3 (currentPosX, currentPosY, posZIni);
					}

					if (currentPosInjuredCat == 0)
						currentPosInjuredCat = PosInjuredCatIniX;
					else{
						if (currentPosInjuredCat < catInjuredPosXFinal) {
							currentPosInjuredCat += veloPosCat * Time.deltaTime;
							catPos.localPosition = new Vector3 (currentPosInjuredCat, 0, 0);
						}
					}

					if (alphaCat < 1) {
						alphaCat += Time.deltaTime;
						cat.color = new Color (cat.color.r, cat.color.g, cat.color.b, alphaCat);
					}

					if (alphaText < 1) {
						alphaText += Time.deltaTime;
						nueva_.color = new Color (nueva_.color.r, nueva_.color.g, nueva_.color.b, alphaText);
						volver_.color = new Color (volver_.color.r, volver_.color.g, volver_.color.b, alphaText);
					}

					if (alpha < 1 && !turn) {
						alpha += Time.deltaTime;
						nuevaPartida.color = new Color (nuevaPartida.color.r, nuevaPartida.color.g, nuevaPartida.color.b, alpha);
						volver.color = new Color (volver.color.r, volver.color.g, volver.color.b, alpha);
					}else if (alpha >= 1 && !turn)
						turn = true;

					if (visualizarImg_1) {
						//Visualizamos y ocultamos los iconos del gato zombie de cada boton
						if (alphaImgCat_1 < 1) {
							alphaImgCat_1 += veloIcons * Time.deltaTime;
							catIcon_Nueva.color = new Color (catIcon_Nueva.color.r, catIcon_Nueva.color.g, catIcon_Nueva.color.b, alphaImgCat_1);
						}

						//Lanzamos las opciones: comenzar con o sin tutorial

					}else{
						if (alphaImgCat_1 > 0) {
							alphaImgCat_1 -= veloIcons * Time.deltaTime;
							catIcon_Nueva.color = new Color (catIcon_Nueva.color.r, catIcon_Nueva.color.g, catIcon_Nueva.color.b, alphaImgCat_1);
						}
					}

					if (visualizarImg_2) {
						//Visualizamos y ocultamos los iconos del gato zombie de cada boton
						if (alphaImgCat_2 < 1) {
							alphaImgCat_2 += veloIcons * Time.deltaTime;
							catIcon_Volver.color = new Color (catIcon_Volver.color.r, catIcon_Volver.color.g, catIcon_Volver.color.b, alphaImgCat_2);
						}
					}else{
						if (alphaImgCat_2 > 0) {
							alphaImgCat_2 -= veloIcons * Time.deltaTime;
							catIcon_Volver.color = new Color (catIcon_Volver.color.r, catIcon_Volver.color.g, catIcon_Volver.color.b, alphaImgCat_2);
						}
					}
				}else{
					//Volvemos a dejar todo como al principio
					if (escalar < minEscala) {
						escalar += velocidadEscala * Time.deltaTime;
						rect.sizeDelta = new Vector2 (escalar, ancho);
					}

					if (currentPosY > posYIni) {
						currentPosY -= velocidadPosY * Time.deltaTime;
						rect.localPosition = new Vector3 (currentPosX, currentPosY, posZIni);
					}

					if (currentPosX > posXIni) {
						currentPosX -= velocidadPosX * Time.deltaTime;
						rect.localPosition = new Vector3 (currentPosX, currentPosY, posZIni);
					}

					if (currentPosInjuredCat > PosInjuredCatIniX) {
						currentPosInjuredCat -= veloPosCat * Time.deltaTime;
						catPos.localPosition = new Vector3 (currentPosInjuredCat, 0, 0);
					}

					if (alphaCat > 0) {
						alphaCat -= Time.deltaTime;
						cat.color = new Color (cat.color.r, cat.color.g, cat.color.b, alphaCat);
					}

					if (alphaText > 0) {
						alphaText -= Time.deltaTime;
						nueva_.color = new Color (nueva_.color.r, nueva_.color.g, nueva_.color.b, alphaText);
						volver_.color = new Color (volver_.color.r, volver_.color.g, volver_.color.b, alphaText);
					}

					if (alpha > 0 && turn) {
						alpha -= Time.deltaTime;
						nuevaPartida.color = new Color (nuevaPartida.color.r, nuevaPartida.color.g, nuevaPartida.color.b, alpha);
						volver.color = new Color (volver.color.r, volver.color.g, volver.color.b, alpha);
					}else if (alpha <= 0 && turn) {
						turn = false;
						botonNueva.enabled = false;
						botonVolver.enabled = false;
					}

					if (alphaImgCat_1 > 0) {
						alphaImgCat_1 -= veloIcons * Time.deltaTime;
						catIcon_Nueva.color = new Color (catIcon_Nueva.color.r, catIcon_Nueva.color.g, catIcon_Nueva.color.b, alphaImgCat_1);
					}

					if (alphaImgCat_2 > 0) {
						alphaImgCat_2 -= veloIcons * Time.deltaTime;
						catIcon_Volver.color = new Color (catIcon_Nueva.color.r, catIcon_Nueva.color.g, catIcon_Nueva.color.b, alphaImgCat_2);
					}

					//Lo usamos para hacer parpadear el texto que surje
					ParpadearTexto parpadea = FindObjectOfType<ParpadearTexto> ();
					if (!parpadearOtraVez) {
						parpadea.InicializarParpadeo ();
						parpadearOtraVez = true;
					}else
						parpadea.Parpadear ();
				}
			}
		}
	}

	public void TransicionNuevaPartida () {

		volvemos = false;
		parpadearOtraVez = false;
	}

	public void AccionNuevaPartida () {

		ThemeScript audio = FindObjectOfType<ThemeScript> ();
		audio.quitarAudio = true;
		SceneManager.LoadScene ("The_Guiscat");
	}

	public void AccionVolver () {

		volvemos = true;
	}

	public void GatoNuevaPartidaMore () {

		visualizarImg_1 = true;
	}

	public void GatoNuevaPartidaLess () {

		visualizarImg_1 = false;
	}

	public void GatoVolverMore () {

		visualizarImg_2 = true;
	}

	public void GatoVolverLess () {

		visualizarImg_2 = false;
	}
}
