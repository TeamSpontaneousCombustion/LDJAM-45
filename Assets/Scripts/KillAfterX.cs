using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterX : MonoBehaviour
{
	[SerializeField]
	float t = 3;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, t);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
