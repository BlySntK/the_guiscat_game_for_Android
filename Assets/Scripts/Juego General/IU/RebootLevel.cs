using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RebootLevel : MonoBehaviour {

	public void Reboot (string level) {

		SceneManager.LoadScene (level);
	}
}
