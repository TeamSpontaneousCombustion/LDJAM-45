using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipComponent : MonoBehaviour
{
	public int Health = 3;

	Vector2 deltaPos = Vector2.zero;

	bool IsBeingDragged = false;

	PlayerMovement pm;

	Rigidbody2D rb;
    // Start is called before the first frame update
	void Start()
	{
		pm = GameObject.Find("Ship").GetComponent<PlayerMovement>();
		rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
	void Update()
	{
    	//Draging Stuff:
		if(pm.EditMode) {
			if(!IsBeingDragged) {
				CheckDrag();
			} else {
				transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector2) deltaPos;
				if(Input.GetMouseButtonUp(0)) {
					IsBeingDragged = false;

					RaycastHit2D hit = Physics2D.Raycast(
						Camera.main.ScreenToWorldPoint(Input.mousePosition),
						Vector2.zero,
						Mathf.Infinity,
						~(1 << 12)
						);
					if(hit.collider != null) {
						if(hit.transform.gameObject == this.gameObject){
							bool AnyConnection = false;
							foreach (Transform Anchor in pm.gameObject.GetComponentsInChildren<Transform>()) {
								if(Anchor.tag != "Ghosts") {
									continue;
								}
								float dist = Vector2.Distance(transform.position, Anchor.transform.position);
								//Debug.Log(dist);
								if(dist < 0.7f) {
									//Attach the part to the main ship
									transform.parent = pm.transform;
									transform.rotation = pm.transform.rotation;
									transform.position = Anchor.transform.position;
									gameObject.layer = 11;
									AnyConnection = true;
									pm.gameObject.SendMessage("UpdateShip");									
								}
							}
							if(!AnyConnection) {
								//Detach the part from the main ship
								transform.parent = null;
								gameObject.layer = 0;
								pm.gameObject.SendMessage("UpdateShip");
							}
						}
					}
				}
			}
		}
		if(Health <= 0) {
			KillPart();
		}
	}
	void CheckDrag(){
		if(Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast(
				Camera.main.ScreenToWorldPoint(Input.mousePosition),
				Vector2.zero,
				Mathf.Infinity
				);
			if(hit.collider != null) {
				if(hit.transform.gameObject == this.gameObject){
					gameObject.SendMessage("DestroyGhosts", null, SendMessageOptions.DontRequireReceiver);
					IsBeingDragged = true;
					deltaPos = (Vector2) transform.position - (Vector2) hit.point;
				}
			}
		}
	}

	void KillPart() {
		Destroy(gameObject);
		pm.gameObject.SendMessage("UpdateShip");
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.layer == 10) {
			Health--;
			Destroy(col.gameObject);
			Instantiate(Resources.Load("Sounds/Hurt"));
		}
	}
}
