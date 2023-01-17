using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptGameOver : MonoBehaviour {

	Image spriteGOB,
			spriteText,
			spriteSText;
	bool aparecerText,
			aparecerSubText;
	float alphaB, alphaText, alphaSubT;
	float tempo = 0.05f, tempoText = 2,
			tempoSubText = 8, tempoFin = 10;


	void Start () {

		spriteGOB = GetComponent<Image> ();
		spriteText = GameObject.Find ("GOText").GetComponent<Image> ();
		spriteSText = GameObject.Find ("GOSubtext").GetComponent<Image> ();
		alphaB = 0;
		alphaText = 0;
		alphaSubT = 0;
		spriteGOB.color = new Color (spriteGOB.color.r, spriteGOB.color.g, spriteGOB.color.b, alphaB);
		spriteText.color = new Color (spriteText.color.r, spriteText.color.g, spriteText.color.b, alphaText);
		spriteSText.color = new Color (spriteSText.color.r, spriteSText.color.g, spriteSText.color.b, alphaSubT);
	}

	public void GameOver () {

		AparecerBase ();
		if (aparecerText)
			AparecerTexto ();

		if (aparecerSubText)
			AparecerSubTexto ();

		PiensoDentro quitarAviso = (PiensoDentro) FindObjectOfType<PiensoDentro> ();
		//Si aparece el Game Over, en 10 segundos volveremos a empezar la partida
		if (quitarAviso.gameOver) {
			if (tempoFin > 0)
				tempoFin -= Time.deltaTime;
			else{
				EmpezarPartida saltartuto = FindObjectOfType<EmpezarPartida> ();
				saltartuto.noMasTutorial = true;
				SceneManager.LoadScene ("BlaisantkaScene");
			}
		}
	}

	void AparecerBase () {

		Llenar tiempo = FindObjectOfType<Llenar> ();
		PiensoDentro pienso = (PiensoDentro) FindObjectOfType<PiensoDentro> ();
		if (alphaB < 1 && tiempo.tiempoAparecerGameOver <= 0) {
			alphaB += tempo * Time.deltaTime;
			spriteGOB.color = new Color (spriteGOB.color.r, spriteGOB.color.g, spriteGOB.color.b, alphaB);
		}else if (alphaB >= 1 && tiempo.tiempoAparecerGameOver <= 0)
			aparecerText = true;
		else if (alphaB > 0 && !pienso.recuperar && tiempo.tiempoAparecerGameOver > 0) {
			alphaB -= tempo * Time.deltaTime;
			spriteGOB.color = new Color (spriteGOB.color.r, spriteGOB.color.g, spriteGOB.color.b, alphaB);
		}else if (alphaB > 0 && pienso.recuperar && tiempo.tiempoAparecerGameOver <= 0) {
			alphaB -= tempo * Time.deltaTime;
			spriteGOB.color = new Color (spriteGOB.color.r, spriteGOB.color.g, spriteGOB.color.b, alphaB);
		}else if (alphaB > 0 && pienso.recuperar && tiempo.tiempoAparecerGameOver > 0)
			pienso.recuperar = false;

	}

	void AparecerTexto () {

		if (alphaText < 1) {
			alphaText += tempoText * Time.deltaTime;
			spriteText.color = new Color (spriteText.color.r, spriteText.color.g, spriteText.color.b, alphaText);
		}else
			aparecerSubText = true;
	}

	void AparecerSubTexto () {

		if (alphaSubT < 1) {
			alphaSubT += tempoSubText * Time.deltaTime;
			spriteSText.color = new Color (spriteSText.color.r, spriteSText.color.g, spriteSText.color.b, alphaSubT);
		}else{
			PiensoDentro quitarAviso = (PiensoDentro)FindObjectOfType<PiensoDentro> ();
			quitarAviso.gameOver = true;
		}
	}
}
