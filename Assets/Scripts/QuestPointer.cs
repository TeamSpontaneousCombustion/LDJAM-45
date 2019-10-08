using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPointer : MonoBehaviour
{
	[SerializeField]
	GameObject Target;
    GameObject[] Enemies;
    GameObject[] Bosses;

    GameObject Player;
    GameObject Cam;
    RectTransform PointerRectTransform;
    // Start is called before the first frame update
    Vector2 RandomPos() {
        Vector2 Pos = new Vector2(
            Random.Range(-100f, 100f),
            Random.Range(-100f, 100f)
            );
        return Pos;
    }
    void Start()
    {
        Cam = GameObject.Find("Main Camera");
        Player = GameObject.Find("Ship");
        for(int i = 0; i < 50; i++) {
            Vector2 Pos;
            do {
                Pos = RandomPos();
            } while(Vector2.Distance(Vector2.zero, Pos) < 20);
            Instantiate(Resources.Load("Enemy"),
                Pos,
                Quaternion.identity
                );
        }
        for(int i = 0; i < 3; i++) {
            Vector2 Pos;
            do {
                Pos = RandomPos();
            } while(Vector2.Distance(Vector2.zero, Pos) < 30);
            Instantiate(Resources.Load("Boss"),
                Pos,
                Quaternion.identity
                );
        }
        Bosses = GameObject.FindGameObjectsWithTag("Boss");
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        PointerRectTransform = GetComponent<RectTransform>();
    }

    bool IsPlaying = false;
    // Update is called once per frame
    void Update()
    {
        if(Target == null) {
            if(Bosses.Length > 0) {
                Bosses = GameObject.FindGameObjectsWithTag("Boss");
                Target = Bosses[0];


            }
            return;
        }

        bool ShouldPlay = false;
        foreach (GameObject Boss in Bosses) {
            if(Vector2.Distance(Boss.transform.position, Player.transform.position) < 20) {
                ShouldPlay = true;
            }
        }
        if(ShouldPlay && !IsPlaying) {
            Cam.SendMessage("ChangeTheme", "Boss");
            IsPlaying = true;
        }
        if(!ShouldPlay && IsPlaying) {
            Cam.SendMessage("ChangeTheme", "Game");
            IsPlaying = false;
        }

        Vector3 ToPos = Target.transform.position;
        Vector3 FromPos = Camera.main.transform.position;
        FromPos.z = 0f;
        Vector3 Dir = (ToPos - FromPos).normalized;
        float AngleTo = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg - 90;
        PointerRectTransform.localEulerAngles = new Vector3(0,0,AngleTo);
    }
}
