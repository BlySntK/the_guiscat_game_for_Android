using UnityEngine;
using System.Collections;

public class Exclamation_II : MonoBehaviour {

	float escalado = 0;
	float speedScale = 29;
	float posicion = 684;
	const float minScale = 5;
	const float maxScale = 20;
	bool bloqueo;


	public void Exclamacion () {

		if (escalado == 0)
			escalado = minScale;
		else if (escalado < maxScale && !bloqueo) {
			escalado += speedScale * Time.deltaTime;
			RectTransform escalar = GetComponent<RectTransform> ();
			escalar.sizeDelta = new Vector2 (escalado, posicion);
		}else if (escalado >= maxScale && !bloqueo)
			bloqueo = true;
		else if (escalado > minScale && bloqueo) {
			escalado -= speedScale * Time.deltaTime;
			RectTransform escalar = GetComponent<RectTransform> ();
			escalar.sizeDelta = new Vector2 (escalado, posicion);
		}else if (escalado <= minScale && bloqueo)
			bloqueo = false;
	}
}
