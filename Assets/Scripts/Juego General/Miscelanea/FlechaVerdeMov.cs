using UnityEngine;
using System.Collections;

public class FlechaVerdeMov : MonoBehaviour {

	const float xIda = 230, xVuelta = 242, posY = -32;
	float currentX = 242, speedIda = 20, speedVuelta = 10;
	bool turn;


	void Update () {

		if (currentX > xIda && !turn) {
			currentX -= speedIda * Time.deltaTime;
			RectTransform recI = GetComponent<RectTransform> ();
			recI.localPosition = new Vector3 (currentX, posY, 0);
		}else if (currentX <= xIda && !turn)
			turn = true;

		if (currentX < xVuelta && turn) {
			currentX += speedVuelta * Time.deltaTime;
			RectTransform recV = GetComponent<RectTransform> ();
			recV.localPosition = new Vector3 (currentX, posY, 0);
		} else if (currentX >= xVuelta && turn)
			turn = false;
	}
}
