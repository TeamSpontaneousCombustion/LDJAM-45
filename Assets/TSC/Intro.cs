using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution(960, 600, false);
		StartCoroutine(ChangeScene());
	}
	
	IEnumerator ChangeScene() {
		yield return new WaitForSeconds(4.5f);
		SceneManager.LoadSceneAsync("Menu");
	}
}
