using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RandomDirection : MonoBehaviour
{
	GameObject Player;
	float MoveSpeed = 2f;
	Vector2 Target;
	// Start is called before the first frame update
	void Start()
	{
		MoveSpeed = float.Parse(Util.Parse("Enemy_RandomDirection")["MoveSpeed"].ToString());
		Player = GameObject.Find("Ship");
		Target = (Vector2)Player.transform.position + new Vector2(Random.Range(-5f,5f), Random.Range(-5f,5f));
	}

    // Update is called once per frame
    void Update()
	{
		if(Vector2.Distance(transform.position, Player.transform.position) > 15) {
			return;
		}
		if(Vector2.Distance(transform.position, Target) < 2) {
			Target = (Vector2)Player.transform.position + new Vector2(Random.Range(-5f,5f), Random.Range(-5f,5f));
		}
		Vector2 Dir = (Target - (Vector2)transform.position).normalized;
		transform.Translate(MoveSpeed * Time.deltaTime * Dir);

    }
}
