using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOutBlai : MonoBehaviour {

	FadeOFFBlai fade;

	public void Fade () {

		fade = GetComponent<FadeOFFBlai> ();
		if (fade.alpha < 1)
			fade.StartFade (1);
	}

	void Update () {

		fade = GetComponent<FadeOFFBlai> ();
		if (fade.continuar)
			SceneManager.LoadScene ("Title");
		else if (fade.load)
			SceneManager.LoadScene ("Title");
	}
}
