using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIButtons : MonoBehaviour
{
	GameObject MenuMusic;
	void Start() {
		MenuMusic = GameObject.Find("MenuMusic");
		if(MenuMusic == null) {
			MenuMusic = (GameObject)Instantiate(Resources.Load("Menu"));
			MenuMusic.name = "MenuMusic";
			DontDestroyOnLoad(MenuMusic);
		}
	}
    // Start is called before the first frame update
    public void ChangeScene(string SceneName) {
	    if(SceneName == "Game") {
	    	Destroy(MenuMusic);
	    }
	    SceneManager.LoadSceneAsync(SceneName);
	}

}
