using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool EditMode = false;
	float EnginePower = 1;

	Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        UpdateShip();
    }

    // Update is called once per frame
    void Update()
    {
    	if(!EditMode) {
    		Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    		Vector3 lookAt = mouseScreenPosition;
    		float AngleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
    		float AngleDeg = (180 / Mathf.PI) * AngleRad;
    		this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

    		rb.velocity = transform.right * Input.GetAxis("Vertical") * EnginePower;

    	}

    	if(Input.GetKeyUp("q")) {
    		EditMode = !EditMode;
    	}
    }

    void UpdateShip() {
    	EnginePower = 0;
    	foreach (Transform Child in transform) {
    		Engine engine = Child.GetComponent<Engine>();
    		if(engine != null) {
    			EnginePower += engine.EnginePower;
    		}
    	}
    }
}
