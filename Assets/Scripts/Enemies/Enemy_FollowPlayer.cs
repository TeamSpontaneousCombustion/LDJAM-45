using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FollowPlayer : MonoBehaviour
{	
	GameObject Player;
	float MoveSpeed = 2f;
	// Start is called before the first frame update
	void Start()
	{
		MoveSpeed = float.Parse(Util.Parse("Enemy_FollowPlayer")["MoveSpeed"].ToString());
		Player = GameObject.Find("Ship");
	}

	// Update is called once per frame
	void Update()
	{
		if(Vector2.Distance(transform.position, Player.transform.position) > 15) {
			return;
		}

		if(Vector2.Distance(transform.position, Player.transform.position) > 1) {
			Vector2 dir = (Player.transform.position - transform.position).normalized;
			transform.Translate(MoveSpeed * Time.deltaTime * dir);
		}
		
	}
}
