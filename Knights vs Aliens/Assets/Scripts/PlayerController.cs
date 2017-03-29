using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
	public float speed=30f;
	public float RotateSpeed=30f;
	private Rigidbody rb;

	void Start(){
		Cursor.visible = false;
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		Vector3 movement = transform.forward;
		Vector3 movement2 = transform.right;


		if (Input.GetKey (KeyCode.W)) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				rb.AddForce (movement * speed * 2);
			} else {
				rb.AddForce (movement * speed);
			}
		}
		if (Input.GetKey (KeyCode.S)) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				rb.AddForce (-movement * speed * 2);
			} else {
				rb.AddForce (-movement * speed);
			}
		}

		if (Input.GetKey (KeyCode.D)) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				rb.AddForce (movement2 * speed * 2);
			} else {
				rb.AddForce (movement2 * speed);
			}
		}
		if (Input.GetKey (KeyCode.A)) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				rb.AddForce (-movement2 * speed * 2);
			} else {
				rb.AddForce (-movement2 * speed);
			}
		}

		if (Input.GetKey("space") && rb.transform.position.y <= 1f) 
		{
			Vector3 jump = new Vector3 (0.0f, 60.0f, 0.0f);
			rb.AddForce (jump);
		}

		float h = Input.GetAxis ("Mouse X");
		transform.Rotate (0f, h*2f, 0f);
	}
		
}