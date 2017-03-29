using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour {

	public void StartSceneButton(){
		
		SceneManager.LoadScene ("Scene1");

	}
	public void ExitScene(){
		Application.Quit ();
	}
}

