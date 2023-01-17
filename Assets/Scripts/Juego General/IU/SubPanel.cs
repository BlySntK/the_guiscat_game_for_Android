using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubPanel : MonoBehaviour {

	Image onOff;
	public Sprite[] onOff_;
	bool _onOff;
	float colorA = 0;
	const float yPosMax = -241, yPosMin = -110; 
	float xPos = 0, zPos = 0;
	const float speed = 150, speedColorBack = 3, speedColorOpen = 0.8f;
	float currentY = -110;
	RectTransform panel;
	Image panelColor, botonRebootLevel, botonRebootGame;
	Text botonLevel, botonGame;
	CanvasGroup canvasPanel, canvasButonLevel, canvasButonGame,
				canvasVolver;


	void Start () {
	
		onOff = GetComponent<Image> ();
		onOff.enabled = true;
		panel = GameObject.Find ("SubPanel").GetComponent<RectTransform> ();
		canvasPanel = GameObject.Find ("SubPanel").GetComponent<CanvasGroup> ();
		canvasButonLevel = GameObject.Find ("Reiniciar nivel").GetComponent<CanvasGroup> ();
		canvasButonGame = GameObject.Find ("Reiniciar juego").GetComponent<CanvasGroup> ();
		canvasVolver = GameObject.Find ("Volver").GetComponent<CanvasGroup> ();
		panelColor = GameObject.Find ("SubPanel").GetComponent<Image> ();
		botonRebootGame = GameObject.Find ("Reiniciar juego").GetComponent<Image> ();
		botonRebootLevel = GameObject.Find ("Reiniciar nivel").GetComponent<Image> ();
		botonLevel = GameObject.Find ("RebootLevel").GetComponent<Text> ();
		botonGame = GameObject.Find ("RebootGame").GetComponent<Text> ();
		canvasPanel.ignoreParentGroups = true;
		canvasPanel.blocksRaycasts = false;
		canvasButonLevel.blocksRaycasts = true;
		canvasButonLevel.ignoreParentGroups = false;
		canvasButonLevel.interactable = false;
		canvasButonGame.blocksRaycasts = true;
		canvasButonGame.interactable = false;
		canvasVolver.blocksRaycasts = true;
		canvasVolver.interactable = true;
		panelColor.color = new Color (panelColor.color.r, panelColor.color.g, panelColor.color.b, colorA);
		botonRebootLevel.color = new Color (botonRebootLevel.color.r, botonRebootLevel.color.g, botonRebootLevel.color.b, colorA);
		botonRebootGame.color = new Color (botonRebootGame.color.r, botonRebootGame.color.g, botonRebootGame.color.b, colorA);
		botonLevel.color = new Color (botonLevel.color.r, botonLevel.color.g, botonLevel.color.b, colorA);
		botonGame.color = new Color (botonGame.color.r, botonGame.color.g, botonGame.color.b, colorA);
		xPos = 1; zPos = 0;
		panel.localPosition = new Vector3 (xPos, yPosMin, zPos);
	}


	void Update () {

		if (_onOff) {
			onOff.sprite = onOff_ [1];
			if (currentY > yPosMax) {
				xPos = panel.localPosition.x;
				zPos = panel.localPosition.z;
				currentY -= speed * Time.deltaTime;
				panel.localPosition = new Vector3 (xPos, currentY, zPos);
			}else{
				canvasPanel.blocksRaycasts = true;
				canvasButonGame.ignoreParentGroups = true;
				canvasButonLevel.ignoreParentGroups = true;
				canvasButonGame.interactable = true;
				canvasButonLevel.interactable = true;
			}

			if (colorA < 1) {
				colorA += speedColorOpen * Time.deltaTime;
				panelColor.color = new Color (panelColor.color.r, panelColor.color.g, panelColor.color.b, colorA);
				botonRebootLevel.color = new Color (botonRebootLevel.color.r, botonRebootLevel.color.g, botonRebootLevel.color.b, colorA);
				botonRebootGame.color = new Color (botonRebootGame.color.r, botonRebootGame.color.g, botonRebootGame.color.b, colorA);
				botonLevel.color = new Color (botonLevel.color.r, botonLevel.color.g, botonLevel.color.b, colorA);
				botonGame.color = new Color (botonGame.color.r, botonGame.color.g, botonGame.color.b, colorA);
			}
		}else{
			onOff.sprite = onOff_ [0];
			xPos = panel.localPosition.x;
			zPos = panel.localPosition.z;

			if (currentY < yPosMin) {
				xPos = panel.localPosition.x;
				zPos = panel.localPosition.z;
				currentY += speed * Time.deltaTime;
				panel.localPosition = new Vector3 (xPos, currentY, zPos);
			}else{
				canvasPanel.blocksRaycasts = false;
				canvasButonGame.interactable = false;
				canvasButonLevel.interactable = false;
			}

			if (colorA > 0) {
				colorA -= speedColorBack * Time.deltaTime;
				panelColor.color = new Color (panelColor.color.r, panelColor.color.g, panelColor.color.b, colorA);
				botonRebootLevel.color = new Color (botonRebootLevel.color.r, botonRebootLevel.color.g, botonRebootLevel.color.b, colorA);
				botonRebootGame.color = new Color (botonRebootGame.color.r, botonRebootGame.color.g, botonRebootGame.color.b, colorA);
				botonLevel.color = new Color (botonLevel.color.r, botonLevel.color.g, botonLevel.color.b, colorA);
				botonGame.color = new Color (botonGame.color.r, botonGame.color.g, botonGame.color.b, colorA);
			}
		}
	}

	public void OnOff () {

		_onOff = !_onOff;
	}
}
