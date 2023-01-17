using UnityEngine;
using System.Collections;

public class MoverPienso : MonoBehaviour {


	void FixedUpdate () {

		Rigidbody moverPienso = GetComponent<Rigidbody> ();
		moverPienso.velocity = (new Vector3 (0, -80, 0));
	}
}
