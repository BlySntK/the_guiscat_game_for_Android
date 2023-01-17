using UnityEngine;
using System.Collections;

public class CabeceraScript : MonoBehaviour {

	void Start () {

		Renderer render = GetComponent<Renderer> ();
		#if UNITY_STANDALONE_WIN
//			MovieTexture cabecera = (MovieTexture)render.material.mainTexture;
//				
//			if (cabecera.isPlaying) {
//			}else
//				cabecera.Play ();
		#endif
	}
}
