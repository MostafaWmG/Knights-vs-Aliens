using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	void FixedUpdate(){

		float v = Input.GetAxis ("Mouse Y");
		transform.Rotate (-v * 0.2f,0f , 0f);
	}
}
