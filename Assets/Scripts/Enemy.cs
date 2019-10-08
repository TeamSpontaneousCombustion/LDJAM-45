using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{	
	[SerializeField]
	public int Health = 3;
	string[] Drops = new string[] {"Engine", "Gun", "Hull", "PowerCell"};
	// Start is called before the first frame update
	void Start()
	{
		switch (Random.Range(0,3)) 
		{
			case 0:
			gameObject.AddComponent<Enemy_FollowPlayer>();
			break;
			case 1:
			gameObject.AddComponent<Enemy_RunAway>();
			break;
			case 2:
			gameObject.AddComponent<Enemy_RandomDirection>();
			break;
		}
		switch (Random.Range(0,3)) 
		{
			case 0:
			gameObject.AddComponent<Enemy_ShootAtPlayer>();
			break;
			case 1:
			gameObject.AddComponent<Enemy_ShootCircle>();
			break;
			case 2:
			gameObject.AddComponent<Enemy_ShootPlus>();
			break;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(Health <= 0) {
			//Explote and throw some loot :D
			Instantiate(Resources.Load("Sounds/Ded"));
			Instantiate(Resources.Load(Drops[Random.Range(0, Drops.Length)]), transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		Instantiate(Resources.Load("Sounds/EnemyHurt"));
		if(col.gameObject.layer == 8) {
			Health--;
			Destroy(col.gameObject);
		}
	}
}
