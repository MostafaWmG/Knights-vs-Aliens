using System.Collections;
using UnityEngine;

public class GameCellController : MonoBehaviour {

	void OnMouseDown(){
		GameManager.instance.mousePos = transform.position;
	}
}
