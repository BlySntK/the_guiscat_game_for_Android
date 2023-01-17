using UnityEngine;
using System.Collections;

public class Llenado : MonoBehaviour {


	ParticleSystem pe;

	public void LlenarComedero () {

		pe = GetComponent<ParticleSystem> ();
		pe.Play ();
	}

	public void PararLlenado () {

		pe = GetComponent<ParticleSystem> ();
		pe.Stop ();
	}
}
