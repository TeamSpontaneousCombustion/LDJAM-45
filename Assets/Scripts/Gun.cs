using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	PlayerMovement pm;
	int speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Ship").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
    	Vector2[] dirs = new Vector2[] {transform.up, transform.right, -transform.up, -transform.right};
        if(transform.parent == pm.gameObject.transform) {
        	if(Input.GetKeyUp("space")) {
        		foreach (Vector2 dir in dirs) {
        			GameObject Bullet = (GameObject)Instantiate(
        				Resources.Load("FriendlyBullet")
        				);
        			Destroy(Bullet, 10f);
        			Bullet.transform.position = transform.position;
        			Bullet.GetComponent<Rigidbody2D>().velocity = dir * speed;
        		}
        	}
        }
    }
}