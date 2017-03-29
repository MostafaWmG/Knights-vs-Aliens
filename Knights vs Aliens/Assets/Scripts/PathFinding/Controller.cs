using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public int approach;
	public Transform target;
	public float speed = 50f;
	public bool resest = false;

	Vector3[] patch;
	int targetIndex;
//
//	void Start(){
//		Movement.Move (transform.position, target.position, patchCallBack,approach);
//	}
	public void request(){
		Movement.Move (transform.position, target.position, patchCallBack,approach);
	}

	public void patchCallBack(Vector3[] patch, bool success,int approach){
		if (success) {
			this.patch = patch;
			if (approach == 0) {
				StopCoroutine ("MoveAlongPatch");
				StartCoroutine ("MoveAlongPatch",approach);
			} else if (approach == 1) {
				StopCoroutine ("MoveToStart");
				StopCoroutine ("MoveAlongPatch");
				StartCoroutine ("MoveToStart");
			}
			
		}
	}

	IEnumerator MoveAlongPatch(int approach){

		Vector3 currentLocation = patch [0];
		targetIndex = 0;

		while (true && !resest) {
			if (transform.position == currentLocation) {
				targetIndex++;
				if (targetIndex >= patch.Length) {
					if (approach == 1) {
						StartCoroutine ("MoveToEnd");
					}
					yield break;	
				}
				currentLocation = patch [targetIndex];
			}

			transform.LookAt (currentLocation);
			transform.position = Vector3.MoveTowards (transform.position, currentLocation, speed * Time.deltaTime);
			yield return null;
		}



	}

	IEnumerator MoveToStart(){
		
		Vector3 currentLocation = patch [0];

		while (transform.position != currentLocation && !resest) {
			transform.LookAt (currentLocation);
			transform.position = Vector3.MoveTowards (transform.position, currentLocation, speed * Time.deltaTime);
			yield return null;
		}

		StartCoroutine ("MoveAlongPatch",1);
	}

	IEnumerator MoveToEnd(){
		
		while (transform.position != target.position && !resest) {
			transform.LookAt (target.position);
			transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
			yield return null;
		}
	}

	public void OnDrawGizmos(){
		
		if (patch != null && !resest) {
			for (int i = targetIndex; i < patch.Length; i++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube (patch [i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine (transform.position, patch [targetIndex]);
				} else {
					Gizmos.DrawLine (patch [i - 1], patch [i]);
				}
			} 
		} else if (resest){
			patch = null;
		}
	}
}
