using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShootAtPlayer : MonoBehaviour
{
	GameObject Player;
	float Cooldown = 3f;
	float Delay = 3f;
	float BulletSpeed = 0.5f;
	// Start is called before the first frame update
	void Start()
	{
		Player = GameObject.Find("Ship");
		Delay = float.Parse(Util.Parse("Enemy_ShootAtPlayer")["Delay"].ToString());
		BulletSpeed = float.Parse(Util.Parse("Enemy_ShootAtPlayer")["BulletSpeed"].ToString());
	}

	// Update is called once per frame
	void Update()
	{
		if(Vector2.Distance(Player.transform.position, transform.position) > 20.0f) {
			Cooldown = Time.time + Delay;
			return;
		}

		if(Time.time > Cooldown) {
			Cooldown += Delay;
			Fire();
		}
	}

	void Fire() {
		Vector2 Target = (Player.transform.position - transform.position).normalized;
		

		GameObject Bullet = (GameObject)Instantiate(Resources.Load("EnemyBullet"), transform.position, Quaternion.identity);
		Bullet.GetComponent<Rigidbody2D>().velocity = Target * BulletSpeed;
		Destroy(Bullet, 10f);
	}
}
