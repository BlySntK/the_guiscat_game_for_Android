using UnityEngine;
using System.Collections;

public class CocinaVideo : MonoBehaviour {

	float alpha = 0, velocidadDeAparicion = 0.06f;

	void Start () {

		#if UNITY_EDITOR_WIN
			Renderer cocina = GetComponent<Renderer> ();
			Renderer render = GetComponent<Renderer> ();
			alpha = 0;
			cocina.material.color = new Color (cocina.material.color.r, cocina.material.color.g, cocina.material.color.b, alpha);
			MovieTexture cocinaVideo = (MovieTexture) render.material.mainTexture;
		#endif
	}

	void Update () {

		AlphaTitleGuiscat alphaTile = FindObjectOfType<AlphaTitleGuiscat> ();
		if (alpha < 1 && alphaTile.alpha >= 1) {
			alpha += velocidadDeAparicion * Time.deltaTime;

			#if UNITY_EDITOR_WIN
				Renderer cocina_ = GetComponent<Renderer> ();
				Renderer render_ = GetComponent<Renderer> ();
				cocina_.material.color = new Color (cocina_.material.color.r, cocina_.material.color.g, cocina_.material.color.b, alpha);
				MovieTexture cocinaVideo = (MovieTexture) render_.material.mainTexture;
				if (cocinaVideo.isPlaying) {
				}else{
					cocinaVideo.loop = true;
					cocinaVideo.Play ();
				}
			#endif
		}
	}
}
