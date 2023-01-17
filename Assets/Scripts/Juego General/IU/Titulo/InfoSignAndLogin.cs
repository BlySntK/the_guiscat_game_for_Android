using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoSignAndLogin : MonoBehaviour {

	Image thisObject, child_a;
	RectTransform childRect_a, childRect_b;
	Text child_b;
	public Sprite[] sprite;
	public string texto;
	public bool changeInfo, infoBadOrGood, empiezaContar;
	float alpha = 0, currentAlpha = 0; 
	public float tiempo = 2, speedTemp = 0.2f; 
	const float maxAlpha = 0.6f;
	const float widthChild_Gato = 38.3f, widthChild_Excla = 15.2f, 
				widthChild_b = 214.4f, height_a = 41.2f, height_b = 30;

	void Awake () {
	
		thisObject = GetComponent<Image> ();
		child_a = GameObject.Find ("IconInfo").GetComponent<Image> ();
		child_b = GameObject.Find ("TextInfo").GetComponent<Text> ();
		childRect_a = GameObject.Find ("IconInfo").GetComponent<RectTransform> ();
		childRect_b = GameObject.Find ("TextInfo").GetComponent<RectTransform> ();
		alpha = thisObject.color.a;
		thisObject.color = new Color (thisObject.color.r, thisObject.color.g, thisObject.color.b, currentAlpha);
		child_a.color = new Color (child_a.color.r, child_a.color.g, child_a.color.b, currentAlpha);
		child_b.color = new Color (child_b.color.r, child_b.color.g, child_b.color.b, currentAlpha);
	}

	void Update () {

		//Cambiamos el icono y el texto de la información de usuario y pass en función de resultados
		if (changeInfo) {
			if (currentAlpha < maxAlpha) {
				currentAlpha += Time.deltaTime;
				thisObject.color = new Color (thisObject.color.r, thisObject.color.g, thisObject.color.b, currentAlpha);
				child_a.color = new Color (child_a.color.r, child_a.color.g, child_a.color.b, currentAlpha);
				child_b.color = new Color (child_b.color.r, child_b.color.g, child_b.color.b, currentAlpha);
			}else
				changeInfo = false;
		}else{
			ChangeButtons modoRegistro = FindObjectOfType<ChangeButtons> ();
			//Se cumplirá este if en caso de no estar registrando a un user
			if (!modoRegistro.back) {
				InputField campoUser = GameObject.Find ("Usuario").GetComponent<InputField> ();
				if (tiempo > 0 && !changeInfo && empiezaContar)
					tiempo -= speedTemp * Time.deltaTime;
				else if (tiempo > 0 && campoUser.text != "") {
					tiempo = 2;
					empiezaContar = false;
					changeInfo = false;
					child_b.text = "";
				}else if (tiempo <= 0 && !changeInfo && empiezaContar) {
					if (currentAlpha > 0) {
						currentAlpha -= Time.deltaTime;
						thisObject.color = new Color (thisObject.color.r, thisObject.color.g, thisObject.color.b, currentAlpha);
						child_a.color = new Color (child_a.color.r, child_a.color.g, child_a.color.b, currentAlpha);
						child_b.color = new Color (child_b.color.r, child_b.color.g, child_b.color.b, currentAlpha);
					}else{
						empiezaContar = false;
						tiempo = 2;
						child_b.text = "";
					}
				}
			}
		}
	}

	public void Information () {

		//Si hay buena info será true, si no... false
		if (infoBadOrGood) {
			childRect_a.sizeDelta = new Vector2 (widthChild_Gato, height_a);
			childRect_b.sizeDelta = new Vector2 (widthChild_b, height_b);
			child_a.sprite = sprite[1];
			child_b.text = texto;
		}else{
			childRect_a.sizeDelta = new Vector2 (widthChild_Excla, height_a);
			childRect_b.sizeDelta = new Vector2 (widthChild_b, height_b);
			child_a.sprite = sprite[0];
			child_b.text = texto;
		}
	}
}
