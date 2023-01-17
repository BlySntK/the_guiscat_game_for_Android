using UnityEngine;
using System.Collections;

public class AjustarPanel : MonoBehaviour {

	const float width = 277, height = 168;
	const float posX = 0, posY = -7, posZ = 0;

	void Update () {

		RectTransform rect = GetComponent<RectTransform> ();
		rect.sizeDelta = new Vector2 (width, height);
		rect.localPosition = new Vector3 (posX, posY, posZ);
	}
}
