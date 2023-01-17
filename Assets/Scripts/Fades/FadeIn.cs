using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {
	
	public Texture2D pic;
	public float speed = 0.05f;
	int drawDepth = -1000;
	public float alpha = 1;
	float timeWait = 6;
	int fadeDir = -1;
	bool turn;


	void OnGUI () {

		//Con esto logramas hacer un Fade In
		if (alpha > 0 && !turn) {
			alpha += fadeDir * speed * Time.deltaTime;
			alpha = Mathf.Clamp01 (alpha);

			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), pic);
		}
//		else if (alpha < 1 && turn) {
//			alpha += fadeDir * speed * Time.deltaTime;
//			alpha = Mathf.Clamp01 (alpha);
//			
//			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
//			GUI.depth = drawDepth;
//			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), pic);
//		}else if (alpha <= 0 && !turn) turn = true;
//		else if (alpha >= 1 && turn) turn = false;

		//Tiempo de espera hasta hacer Fade Out
//		if (timeWait > 0 && turn)
//			timeWait -= Time.deltaTime;

		//Fade Out (Si esta comentado es que el script no esta disponible)
//		if (turn && timeWait <= 0) {
//
//			FadeOut fade = GetComponent<FadeOut> ();
//			fade.Fade ();
//		}
	}

	public float StartFade (int direction) {

		fadeDir = direction;
		return fadeDir;
	}
}
