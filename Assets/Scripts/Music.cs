using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    #pragma warning disable 649
	[SerializeField]
	AudioSource[] GameMusic;
    [SerializeField]
    AudioSource BossMusic;
    #pragma warning restore 649
    GameObject Player;
    float[] isPlaying;
    bool isBossMusicPlaying = false;

	/*
	0: Always Played
	1: N Parts
	2: N Parts
	3: Near Enemies
	4: 1 Boss Part
	5: 2 Boss Parts
	*/

	void Start() {
        isPlaying = new float[GameMusic.Length];
		foreach (AudioSource Track in GameMusic) {
			Track.volume = 0;
		}
        for(int i = 0; i < GameMusic.Length; i++) {
            isPlaying[i] = 0;
        }
        isPlaying[0] = 1;
        GameMusic[0].volume = 1;
        BossMusic.volume = 0;
        Player = GameObject.Find("Ship");
    }

    void Update() {
      GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
      bool ShouldPlayEnemyMusic = false;
      foreach (GameObject Enemy in Enemies) {
        if(Vector2.Distance(Enemy.transform.position, Player.transform.position) < 15f) {
            ShouldPlayEnemyMusic = true;
            break;
        }
    }
    GameMusic[3].volume = 1 * (ShouldPlayEnemyMusic ? 1 : 0);


}

void ChangeMusic(int[] arr) {
    if(!isBossMusicPlaying) {
        GameMusic[arr[0]].volume = (float)arr[1]/100;
    } else {
        isPlaying[arr[0]] = 1;
    }
}

void ChangeTheme(string Theme){
    Debug.Log(Theme);
    if(Theme == "Boss") {
        isBossMusicPlaying = true;
        BossMusic.volume = 0.5f;
        for(int i = 0; i < GameMusic.Length; i++) {
            isPlaying[i] = GameMusic[i].volume;
            GameMusic[i].volume = 0;
        }
    } else {
        isBossMusicPlaying = false;
        BossMusic.volume = 0;
        for(int i = 0; i < GameMusic.Length; i++) {
            GameMusic[i].volume = isPlaying[i];
        }
    }
}

IEnumerator FadeOut(AudioSource audioSource, float FadeTime) {
   float startVolume = audioSource.volume;

   while (audioSource.volume > 0) {
      audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

      yield return null;
  }
  audioSource.volume = 0f;
}

IEnumerator FadeIn(AudioSource audioSource, float FadeTime) {
   float startVolume = audioSource.volume;
   while (audioSource.volume <= 1) {
      audioSource.volume += startVolume * Time.deltaTime / FadeTime;

      yield return null;
  }
  audioSource.volume = 1f;
}
}
