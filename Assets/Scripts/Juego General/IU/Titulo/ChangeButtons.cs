using UnityEngine;
using System.Collections;

public class ChangeButtons : MonoBehaviour {

	[HideInInspector]
	public bool back, register;
	GameObject _back, _register;


	void Awake () {

		_back = GameObject.Find ("Back");
		_register = GameObject.Find ("Register");
	}

	void Update () {

		if (back && !register) {
			_back.SetActive (true);
			_register.SetActive (false);
		}else if (!back && register) {
			_back.SetActive (false);
			_register.SetActive (true);
		}
	}
}
