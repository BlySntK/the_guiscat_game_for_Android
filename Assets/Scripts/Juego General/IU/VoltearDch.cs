using UnityEngine;
using System.Collections;

public class VoltearDch : MonoBehaviour {

	Transform caja;
	float speed = 100f;
	float began = 0;
	public bool voltear; //La usamos para llenar o frenar la caida de pienso
	public bool cajaVolterada;
	
	public void Volteo () {
		
		if (!voltear)
			voltear = true;
	}
	
	public void Frenar () {
		
		if (voltear)
			voltear = false;
	}
	
	void Update () {
		
		if (voltear) {
			if (began < 1)
				began++;
		}else if (!voltear) {
			if (began > 0)
				began--;
		}
		
		if (began > 0) {
			caja = GameObject.Find ("Caja_Pienso").transform;
			if (caja.localRotation.z > -0.87f) {
				caja.Rotate (new Vector3 (0, 0, -speed * Time.deltaTime / 2));
			}else if (caja.localRotation.z <= -0.87f)
				cajaVolterada = true;
		}
	}
}
