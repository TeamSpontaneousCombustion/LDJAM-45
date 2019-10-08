using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	PlayerMovement pm;
	float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        speed = float.Parse(Util.Parse("Player")["BulletSpeed"].ToString());
        pm = GameObject.Find("Ship").GetComponent<PlayerMovement>();
    }

    void Fire() {
        GameObject Bullet = (GameObject)Instantiate(
            Resources.Load("FriendlyBullet")
            );
        Destroy(Bullet, 10f);
        Bullet.transform.position = transform.position;
        Bullet.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }
}