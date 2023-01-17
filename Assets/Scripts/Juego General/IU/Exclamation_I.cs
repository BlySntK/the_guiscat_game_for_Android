using UnityEngine;
using System.Collections;

public class Exclamation_I : MonoBehaviour {

	float escalado = 0;
	const float speedScale = 29;
	const float ancho = 684;
	const float posX = 112.3f, posY = -116.2f;
	const float minScale = 5;
	const float maxScale = 20;
	bool bloqueo;


	public void Exclamacion () {

		//Posicionamos
		RectTransform thisRect = GetComponent<RectTransform> ();
		thisRect.localPosition = new Vector3 (posX, posY, thisRect.position.z);

		//Escalamos (efecto acercamiento)
		if (escalado == 0)
			escalado = minScale;
		else if (escalado < maxScale && !bloqueo) {
			escalado += speedScale * Time.deltaTime;
			RectTransform escalar = GetComponent<RectTransform> ();
			escalar.sizeDelta = new Vector2 (escalado, ancho);
		}else if (escalado >= maxScale && !bloqueo)
			bloqueo = true;
		else if (escalado > minScale && bloqueo) {
			escalado -= speedScale * Time.deltaTime;
			RectTransform escalar = GetComponent<RectTransform> ();
			escalar.sizeDelta = new Vector2 (escalado, ancho);
		}else if (escalado <= minScale && bloqueo)
			bloqueo = false;
	}
}
