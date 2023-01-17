using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Puntuar : MonoBehaviour {

	[HideInInspector]
	public float puntos; //Los puntos se modificaran en scripts como DescontarPienso por cada uno de los gatos cuando comen
	public Text _puntos; //La variable sera modificada en PiensoDentro, al escasear el pienso.
	[HideInInspector]
	public bool despuntuar, despuntuarCaida, auxCaida;
	public bool puntuando; //Booleana para matar a un gato
	bool continuar; //Booleana usada para proseguir con el GameOver aunque no estemos despuntuando
	float velocidad = 22;
	float velocidadDes = 18.8f;
	public float aux_p; //Puntos por matar a un gato
	public float aux_descon; //Descuento de puntos cuando comen los gatos
	float currentPoints, currentDespoint, currentFallen; //Las usamos como auxiliares para poder hacer un incremento/decremento
	ScriptGameOver gameOver;
	PiensoDentro advertencia;
	Llenar _gameOver;




	void Start () {

		puntos = 0;
		_puntos = GetComponent<Text> ();
		_puntos.text = puntos.ToString ("0");
		_gameOver = FindObjectOfType<Llenar> ();
		advertencia = FindObjectOfType<PiensoDentro> ();
	}


	void Update () {

		/* CAIDA DE PIENSO DE LA CAJA */
		//Continuaremos, una vez que hayamos accedido al metodo de descuento de puntos, hacia el GameOver
		if (auxCaida && advertencia.cantidad == 0 && _gameOver.tiempoAparecerGameOver < 0) {
			gameOver = (ScriptGameOver) FindObjectOfType<ScriptGameOver> ();
			gameOver.GameOver ();
		
		//De forma inversa tambien lo haremos para cancelar el propio GameOver
		}else if (auxCaida && advertencia.cantidad > 0 && _gameOver.tiempoAparecerGameOver > 0) {
			gameOver = (ScriptGameOver) FindObjectOfType<ScriptGameOver> ();
			gameOver.GameOver ();
		}
		//***********************************************************************************************

		/* GAME OVER */
		//Accedemos al metodo de despuntuacion y al game over si se acabó el tiempo y no hay pienso
		if (!auxCaida && despuntuar && advertencia.cantidad <= 0 && _gameOver.tiempoAparecerGameOver < 0) {
			Despuntuar ();
			gameOver = (ScriptGameOver) FindObjectOfType<ScriptGameOver> ();
			gameOver.GameOver ();

		//En el caso de deshacer el game over si se empieza a llenar el comedero
		}else if (!auxCaida && continuar && !despuntuar && 
			advertencia.cantidad <= 0 && _gameOver.tiempoAparecerGameOver > 0) {
				gameOver = (ScriptGameOver)FindObjectOfType<ScriptGameOver> ();
				gameOver.GameOver ();
		
		//Si hemos logrado tener más pienso, el Game Over se desvanecera...:
		}else if (advertencia.recuperar && advertencia.cantidad > 0 && _gameOver.tiempoAparecerGameOver > 0) {
			gameOver = FindObjectOfType<ScriptGameOver> ();
			gameOver.GameOver ();
		
		//Si llegamos un poco tarde pero aún hay posibilidades de frenar el game over, se frenará
		}else if (!advertencia.recuperar && advertencia.cantidad > 0 && _gameOver.tiempoAparecerGameOver > 0) {
			gameOver = FindObjectOfType<ScriptGameOver> ();
			gameOver.GameOver ();
		}
		//*****************************************************************************************

		/* PUNTUACION */

		//Accedemos al metodo de puntuacion a traves de la booleana
		if (puntuando)
			Puntuacion ();
		//***********************************************************

		if (puntos <= 0 && _puntos.text.Contains ("-"))
			_puntos.text = "0";
	}

	void Puntuacion () {

		if (currentPoints < aux_p) {
			//Usamos esta auxiliar para ayudar a que el recuento por 2 puntos se haga
			currentPoints += velocidad * Time.deltaTime;
			//***********************************************************************

			/* Aqui la puntuacion real */
			if (puntos < currentPoints) {
				puntos += velocidad * Time.deltaTime;
				_puntos.text = puntos.ToString ("0");
			}
		}else{
			currentPoints = 0;
			puntuando = false;
		}
	}

	void Despuntuar () {

		if (currentDespoint <= aux_descon) {
			//Usamos esta auxiliar para ayudar a que el descuento se haga
			currentDespoint += velocidadDes * Time.deltaTime;
			//***********************************************************************
			
			/* Aqui la puntuacion real */
			puntos -= velocidadDes * Time.deltaTime;
			if (puntos > 0)
				_puntos.text = puntos.ToString ("0");
			else
				_puntos.text = "0";

		}else{
			currentDespoint = 0;
			despuntuar = false;
			continuar = true;
		}
	}

	public void DespuntuarCaida (int cantidad) {

		if (currentFallen <= cantidad) {
			//Usamos esta auxiliar para ayudar a que el descuento se haga
			currentFallen += velocidadDes * Time.deltaTime;
			//***********************************************************************
			
			/* Aqui la puntuacion real */
			puntos -= velocidadDes * Time.deltaTime;

			if (puntos > 0) {
				_puntos.text = puntos.ToString ("0");
				auxCaida = true;
			}else
				_puntos.text = "0";

		}else if (currentFallen >= cantidad && despuntuarCaida) {
			currentFallen = 0;
			despuntuarCaida = false;
			auxCaida = false;
		}
	}
}
