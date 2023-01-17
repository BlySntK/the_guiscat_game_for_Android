using UnityEngine;
using System.Collections;

public class Disparo : MonoBehaviour {

	float velo = 35;
	public GameObject gato;

	void Update () {

		transform.Translate (Vector3.right * velo);

		Destroy (gameObject, 10);
	}
}
