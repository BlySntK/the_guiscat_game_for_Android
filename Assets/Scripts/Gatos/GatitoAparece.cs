using UnityEngine;
using System.Collections;

public class GatitoAparece : MonoBehaviour {

	SkinnedMeshRenderer skin;
	float alpha = 0;
	float speedAlpha = 0.5f;
	
	
	void Start () {
		
		skin = GetComponent<SkinnedMeshRenderer> ();
		skin.material.color = new Color (0, 0, 0, 0);
	}
	
	void Update () {
		if (alpha < 1) {
			alpha += speedAlpha * Time.deltaTime;
			skin.material.color = new Color (1, 1, 1, alpha);
		}
	}
}
