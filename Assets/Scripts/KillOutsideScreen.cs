using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOutsideScreen : MonoBehaviour
{
	Renderer r;
	// Start is called before the first frame update
	void Start()
	{
		r = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log(gameObject.name + " " + r.isVisible);
		if(!r.isVisible) { 
			Destroy(gameObject);
		}
	}
}
