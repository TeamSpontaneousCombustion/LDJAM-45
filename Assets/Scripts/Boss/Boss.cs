using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{	
	[SerializeField]
	public int Health = 25;
	string[] Drops = new string[] {"Trophy"};
	// Start is called before the first frame update
	void Start()
	{
			gameObject.AddComponent<Enemy_RandomDirection>();
			gameObject.AddComponent<Enemy_ShootAtPlayer>();
			gameObject.AddComponent<Enemy_ShootCircle>();
			gameObject.AddComponent<Enemy_ShootPlus>();
	}

	// Update is called once per frame
	void Update()
	{
		if(Health <= 0) {
			//Explote and throw some loot :D
			Instantiate(Resources.Load(Drops[Random.Range(0, Drops.Length)]), transform.position, Quaternion.identity);
			Instantiate(Resources.Load("Sounds/Ded"));
			Destroy(gameObject);
			GameObject Cam = GameObject.Find("Main Camera");
			Cam.SendMessage("ChangeTheme", "Game");
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.layer == 8) {
			Health--;			
			Instantiate(Resources.Load("Sounds/EnemyHurt"));
			Destroy(col.gameObject);
		}
	}
}
