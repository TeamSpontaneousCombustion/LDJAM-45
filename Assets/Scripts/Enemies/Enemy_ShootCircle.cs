using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShootCircle : MonoBehaviour
{
	GameObject Player;
	float Cooldown = 3f;
    float Delay = 1f;
    float BulletSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Ship");
        Delay = float.Parse(Util.Parse("Enemy_ShootCircle")["Delay"].ToString());
        BulletSpeed = float.Parse(Util.Parse("Enemy_ShootCircle")["BulletSpeed"].ToString());
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
    	
        Vector2 dir = new Vector2(Mathf.Cos(Time.time), Mathf.Sin(Time.time));
        GameObject Bullet = (GameObject)Instantiate(Resources.Load("EnemyBullet"), transform.position, Quaternion.identity);
        Bullet.GetComponent<Rigidbody2D>().velocity = dir * BulletSpeed;
        Destroy(Bullet, 10f);
    }
}
