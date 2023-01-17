using UnityEngine;
using System.Collections;

public class CatState : MonoBehaviour {

	RagdollManager rM;

	void Update () {

		if (Input.GetKeyDown (KeyCode.K)) {
			if (GetComponent<RagdollManager> ()) {

				rM = GetComponent<RagdollManager> ();
				rM.RagdollStateOn ();
			}
		}
	}
}
