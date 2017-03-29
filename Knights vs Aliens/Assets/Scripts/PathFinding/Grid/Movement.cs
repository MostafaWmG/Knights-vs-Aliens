using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour {

	Queue<MoveStruct> movementQueue = new Queue<MoveStruct> ();
	MoveStruct currentMove;

	static Movement movement;

	Algorithm algorithm;

	bool notFinish;

	void Awake(){
		movement = this;
		algorithm = GetComponent<Algorithm> ();
		notFinish = false;
	}

	public static void Move(Vector3 startPosition , Vector3 endPosition, Action<Vector3 [] ,bool, int > callBackFunction,int chooseApproach){
		MoveStruct newMoveStruct = new MoveStruct (startPosition, endPosition, callBackFunction,chooseApproach);
		movement.movementQueue.Enqueue (newMoveStruct);
		movement.Next ();
	}

	void Next(){
		if (!notFinish && movementQueue.Count > 0) {
			currentMove = movementQueue.Dequeue ();
			notFinish = true;
			algorithm.FindPatch (currentMove.startPosition, currentMove.endPosition,currentMove.chooseApproach);
		}
		
	}

	public void Finished(Vector3 [] patch, bool check,int approach){
		currentMove.callBackFunction (patch,check,approach);
		notFinish = false;
		Next ();
	}

	struct MoveStruct{
		public Vector3 startPosition;
		public Vector3 endPosition;
		public  Action<Vector3[],bool,int> callBackFunction;
		public int chooseApproach;

		public MoveStruct(Vector3 startPosition , Vector3 endPosition, Action<Vector3 [] ,bool ,int> callBackFunction,int chooseApproach){
			this.startPosition	 = startPosition;
			this.endPosition  = endPosition;
			this.callBackFunction = callBackFunction;
			this.chooseApproach = chooseApproach;
		}

	}
}
