using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
		Player = GameObject.Find("Ship");        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + new Vector3(0,0,-10);
    }
}
