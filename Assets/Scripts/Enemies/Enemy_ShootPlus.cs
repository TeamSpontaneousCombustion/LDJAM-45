using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShootPlus : MonoBehaviour
{
	GameObject Player;
	float Cooldown = 3f;
    float Delay = 3f;
    float BulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Ship");
        Delay = float.Parse(Util.Parse("Enemy_ShootPlus")["Delay"].ToString());
        BulletSpeed = float.Parse(Util.Parse("Enemy_ShootPlus")["BulletSpeed"].ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(Player.transform.position, transform.position) > 15.0f) {
            Cooldown = Time.time + Delay;
            return;
        }

        if(Time.time > Cooldown) {
        	Cooldown += Delay;
        	Fire();
        }
    }

    void Fire() {
        Vector2[] dirs = new Vector2[] {transform.up, -transform.up, -transform.right, transform.right};
        for(int i = 0; i < dirs.Length; i++) {
            GameObject Bullet = (GameObject)Instantiate(Resources.Load("EnemyBullet"), transform.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody2D>().velocity = dirs[i] * BulletSpeed;
            Destroy(Bullet, 10f);
        }
    }
}
