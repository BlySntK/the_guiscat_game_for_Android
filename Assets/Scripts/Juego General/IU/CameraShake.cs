using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	/* Con este script se logra el efecto de vibracion de la camara al matar a un gatito */

	Vector3 originPosition;
	Vector3 originalPos;
	Quaternion originalRot;
	Quaternion originRotation;
	bool retornoPosicion;
	public float shake_decay;
	public float shake_intensity;
	


	void Start () {

		originalRot = transform.rotation;
		originalPos = transform.position;
	}

	void Update (){

		//Mientras la intensidad este por encima de 0, habra vibracion
		if (shake_intensity > 0){
			transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .01f,
				originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .1f,
				originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .1f);
			shake_intensity -= shake_decay;
		}else if (shake_intensity <= 0 && !retornoPosicion)
			retornoPosicion = true;

		//Una vez se calme, volvera a su posicion y rotacion original
		if (retornoPosicion) {
			transform.position = originalPos;
			transform.rotation = originalRot;
			retornoPosicion = false;
		}
	}
	
	public void Shaking (){

		//Metodo por el que se regenera la situacion normal de la camara
		originPosition = transform.position;
		originRotation = transform.rotation;
		shake_intensity = .1f;
		shake_decay = 0.0009f;
	}
}
