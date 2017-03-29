using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour {

	public LayerMask obstacle;
	public LayerMask plane;
	public GameObject ground;
	public Camera cameraMouse;
	public Controller[] playerController;
	public GameObject end;
	public Grid grid;
	public Movement movement;
	public Vector3 mousePos;
	public Vector3 hitPoint;

	public static GameManager instance;

	void Awake(){
		if (instance != null) {
			Debug.Log ("not singelton");
		} else {
			instance = this;	
		}

	}

	void Start(){
		StartCoroutine (Game ());
	}
	 
	IEnumerator Game(){

		while (true) {
			// r
			if (Input.GetButtonDown ("Request")) {
				for (int i = 0; i < playerController.Length; i++) {
					playerController [i].resest = false;
					playerController [i].request ();
				}
			}
				
			//a
			if( Input.GetButtonDown("Astar")){
				for (int i = 0; i < playerController.Length; i++) {
					playerController [i].approach = 0;
				}
			}

			//q
			if( Input.GetButtonDown("Reset")){
				for (int i = 0; i < playerController.Length; i++) {
					playerController [i].resest = true;
					RespawnRandom (playerController [i].transform);
				}
			}

//			if (Input.GetMouseButtonDown(0))
//			{
//				//Debug.Log("Click registered");
//
//				//Debug.Log(Input.mousePosition);
//				mousePos= Input.mousePosition;
//				Debug.Log ("sceen" + mousePos);
//				mousePos.z = Vector3.Distance (Camera.main.transform.localPosition, ground.transform.position);
////				mousePos.z = 5;
////				mousePos = cameraMouse.ScreenToWorldPoint(mousePos);
//				mousePos = Camera.main.ScreenToWorldPoint(mousePos);
//
//				RaycastHit hit;
////				mousePos.y = 20;
//				Physics.Raycast (mousePos, Vector3.down, out hit, 5000f,plane);
//				Debug.Log ("world" + mousePos);
////				float distance = Vector3.Distance (grid.transform.position, mousePos);
////				float x = Mathf.Sin(45f) * distance ;
////				float vatar = x / Mathf.Sin (67.5);
////				float subDistance = vatar * Mathf.Sin (22.5);
////				float mainDistance = distance - subDistance;
//				float z = 0;
////				hitPoint =  new Vector3(mousePos.x, 0f, mousePos.z + distance);
//				hitPoint = hit.point;
////				hitPoint.z = mousePos.z;
//				Debug.Log ("hit" + hitPoint);
//				for (int i = 0; i < playerController.Length; i++) {
//					playerController [i].resest = true;
//					yield return StartCoroutine (grid.UpdateGrid( grid.getNodeFromLocation(hitPoint)));
//					playerController [i].resest = false;
//					playerController [i].request ();
//				}
//
//			}

			if(Input.GetMouseButtonDown(0)){
				Debug.Log ("Escape");
				for (int i = 0; i < playerController.Length; i++) {
//					playerController [i].resest = true;
					yield return StartCoroutine (grid.UpdateGrid( grid.getNodeFromLocation(mousePos)));
					playerController [i].resest = false;
					playerController [i].request ();
				}
			}
				
			yield return null;
		}

	}
		
	void RespawnRandom(Transform player){
//		Debug.Log ("entered");
//		UnityEngine.Random.state = (int)Time.timeScale;
//		UnityEngine.Random.InitState ((int)Time.timeScale);

		int x = UnityEngine.Random.Range (-100,100);
		int z = UnityEngine.Random.Range (-100,100);

//		Debug.Log (x +" : " + z);
		Vector3 test = new Vector3 (x, 0f, z);
	
		validatePosition(x,z,test,player,player,RespawnRandom,RespawnRandomFinish); 

	}

	void RespawnRandomFinish(Transform player, Vector3 test){
		if (player == null) {
			end.transform.position = test;
		} else {
			player.transform.position = test;
			RespawnRandomEnd (player.transform);
		}

	}

	void RespawnRandomEnd(Transform player){
		Vector3 check = player.position;
		if (check.x >= -100 && check.x <= 0 && check.z >= -100 && check.z <= 0) {
			int x = UnityEngine.Random.Range (0, 100);
			int z = UnityEngine.Random.Range (0, 100);

			Vector3 test = new Vector3 (x, 0f, z);
			validatePosition (x, z,test,player,null,RespawnRandomEnd,RespawnRandomFinish);

		} else if (check.x >= 0 && check.x <= 100 && check.z >= -100 && check.z <= 0) {
			int x = UnityEngine.Random.Range (-100, 0);
			int z = UnityEngine.Random.Range (0, 100);

			Vector3 test = new Vector3 (x, 0f, z);
			validatePosition (x, z,test,player,null,RespawnRandomEnd,RespawnRandomFinish);

		}else if (check.x >= -100 && check.x <= 0 && check.z >= 0 && check.z <= 100) {
			int x = UnityEngine.Random.Range (0, 100);
			int z = UnityEngine.Random.Range (-100, 0);

			Vector3 test = new Vector3 (x, 0f, z);
			validatePosition (x, z,test,player,null,RespawnRandomEnd,RespawnRandomFinish);

		}else if (check.x >= 0 && check.x <= 100 && check.z >= 0 && check.z <= 100) {
			int x = UnityEngine.Random.Range (-100, 0);
			int z = UnityEngine.Random.Range (-100, 0);

			Vector3 test = new Vector3 (x, 0f, z);
			validatePosition (x, z,test,player,null,RespawnRandomEnd,RespawnRandomFinish);
		}
	}

	void validatePosition(int x , int z,Vector3 test,Transform  FirstCallback,Transform secondCallback,Action<Transform> callback,Action<Transform , Vector3> callbackFinish){

			if (Physics.CheckBox (test, new Vector3 (grid.NodeSide / 2, grid.NodeSide / 2, grid.NodeSide / 2), Quaternion.identity, obstacle)) {
			callback(FirstCallback);
		} else {
			callbackFinish(secondCallback,test);
		}
	}

	void OnDrawGizmos(){
		
		if (mousePos != null) {
			Gizmos.color = Color.black;
			Gizmos.DrawCube (mousePos,  Vector3.one * ((grid.NodeSide * 2)-.1f));
			Gizmos.DrawWireSphere (mousePos, 10f);
			Gizmos.color = Color.green;
			Gizmos.DrawCube (hitPoint,  Vector3.one * ((grid.NodeSide * 2)-.1f));
			Gizmos.DrawWireSphere (hitPoint, 10f);
		}
			

	}
}


