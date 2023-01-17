using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RebootGame : MonoBehaviour {

	public void Reboot() {

		SceneManager.LoadScene ("BlaisantkaScene");
	}
}
