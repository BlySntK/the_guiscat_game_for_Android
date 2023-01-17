using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeOFFAviso : MonoBehaviour {
	
	public Texture2D pic;
	public float speed = 0.4f;
	int drawDepth = -1000;
	public float alpha = 1;
	float timeWait = 15;
	int fadeDir = -1;
	bool turn, respuesta;
	public bool continuar, load; //Ambos son avisos de carga: con interación de user y sin


	void OnGUI () {

		//Con esto logramas hacer un Fade In de forma automatica
		if (!continuar && alpha > 0 && !turn) {
			alpha += fadeDir * speed * Time.deltaTime;
			alpha = Mathf.Clamp01 (alpha);

			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), pic);
		} else if (!continuar && alpha < 1 && turn) {
			alpha += fadeDir * speed * Time.deltaTime;
			alpha = Mathf.Clamp01 (alpha);
			
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), pic);
		}else if (alpha <= 0 && !turn && !continuar)
			turn = true;
		else if (alpha >= 1 && turn && !continuar) {
			turn = false;
			load = true;
		}

		//Tiempo de espera hasta hacer Fade Out
		if (timeWait > 0 && turn && !continuar)
			timeWait -= Time.deltaTime;

		//Fade Out (Si esta comentado es que el script no esta disponible)
		if (turn && timeWait <= 0 && !continuar) {

			FadeOutAviso fade = GetComponent<FadeOutAviso> ();
			fade.Fade ();
		}
	}

	public float StartFade (int direction) {

		fadeDir = direction;
		return fadeDir;
	}

	//Con este metodo se salta la forma automática
	public void SiguienteIn () {

		continuar = true;
	}
}
