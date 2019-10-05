using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipComponent : MonoBehaviour
{
	public int Health = 10;

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
						Mathf.Infinity
						);
					if(hit.collider != null) {
						if(hit.transform.gameObject == this.gameObject){
							bool AnyConnection = false;
							foreach (Transform Anchor in pm.gameObject.GetComponentsInChildren<Transform>()) {
								if(Anchor.tag != "Ghosts") {
									continue;
								}
								float dist = Vector2.Distance(transform.position, Anchor.transform.position);
								if(dist < 0.5f) {
									transform.parent = pm.transform;
									transform.rotation = pm.transform.rotation;
									transform.position = Anchor.transform.position;
									pm.gameObject.SendMessage("UpdateShip");
									AnyConnection = true;
								}
							}
							if(!AnyConnection) {
								transform.parent = null;
								pm.gameObject.SendMessage("UpdateShip");
							}
						}
					}
				}
			}
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
					gameObject.SendMessage("DestroyGhosts");
					IsBeingDragged = true;
					deltaPos = (Vector2) transform.position - (Vector2) hit.point;
				}
			}
		}
	}
}
