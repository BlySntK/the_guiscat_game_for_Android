using UnityEngine;
using System.Collections;

public class RagdollManager : MonoBehaviour {

	public Collider[] cols;
	public Rigidbody[] rigs;
	Rigidbody notKinect;
	Collider notTrigger;
	bool ragdoll;

	Animator _anim;

	void Start () {

		_anim = GetComponent<Animator> ();

		rigs = GetComponentsInChildren <Rigidbody> ();
		cols = GetComponentsInChildren <Collider> ();
		notKinect = GetComponent<Rigidbody> ();
		notTrigger = GetComponent<Collider> ();

		foreach (Rigidbody r in rigs) {
			if (r.gameObject.layer == 9)
				r.isKinematic = true;
		}

		foreach (Collider cls in cols) {
			if (cls.gameObject.layer == 9)
				cls.isTrigger = true;
		}

		notKinect.isKinematic = false;
		notTrigger.isTrigger = false;
	}

	public void RagdollStateOn () {

		if (!ragdoll) {
			_anim.enabled = false;

			foreach (Rigidbody r in rigs) {
				if (r.gameObject.layer == 9)
					r.isKinematic = false;
			}

			foreach (Collider cls in cols) {
				if (cls.gameObject.layer == 9)
					cls.isTrigger = false;
			}

			ragdoll = true;
		}
	}
}
