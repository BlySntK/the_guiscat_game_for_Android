using UnityEngine;
using System.Collections;

public class Lanza : MonoBehaviour {

	public GameObject b;

	void Update () {

		if (Input.GetKeyDown (KeyCode.D))
			Instantiate (b, transform.position, transform.rotation);
	}
}
