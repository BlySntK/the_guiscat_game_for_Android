using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GPServices : MonoBehaviour {

	public int puntuacion = 0;
	public GameObject panel1;
	public GameObject panel2;
	int contador = 0;

	void Start () {

		PlayGamesPlatform.Activate ();
	}

	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}

	public void Loggin () {
	
		Social.localUser.Authenticate ((bool success) =>{});
	}

	public void VerMarcadores () {
	
		Social.Active.ShowLeaderboardUI ();
	}

	public void SubirPuntuaciones () {

		Social.ReportScore (puntuacion, GooglePlayServices.leaderboard_puntuacion, (bool success) =>{});
	}

	public void NuevoMenu () {

		panel1.SetActive (false);
		panel2.SetActive (true);
	}

	public void CompletarLogro1 () {

		Social.ReportProgress (GooglePlayServices.achievement_eres_muy_malo, 100, (bool success) =>{});
	}

	public void CuantosLlevo () {

		//contador = PlayGamesPlatform.Instance.GetAchievement (GooglePlayServices.achievement_eres_muy_malo).CurrentSteps;
		//textoContador.text = "" + contador;
	}

	public void IncrementarLogro () {
	
		PlayGamesPlatform.Instance.IncrementAchievement (GooglePlayServices.achievement_eres_muy_malo, 1, (bool success) =>{});
		contador++;
		PlayerPrefs.SetInt ("cont", contador);
	}
}
