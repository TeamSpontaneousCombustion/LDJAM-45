using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{	
	PlayerMovement pm;
	bool CreateGhosts = true;
    // Start is called before the first frame update
	void Start()
	{
		pm = GameObject.Find("Ship").GetComponent<PlayerMovement>();
	}

    // Update is called once per frame
	void Update()
	{
		if(pm.EditMode) {
			if(CreateGhosts && (transform.parent != null || transform == pm.gameObject.transform)) {
				CreateAnchors();
				CreateGhosts = false;
			}
		} else {
			if(!CreateGhosts) {
				DestroyGhosts();
			}
			CreateGhosts = true;
		}
	}

	void CreateAnchors() {
		//Please Forgive me
		RaycastHit2D hit = Physics2D.BoxCast(
			transform.position + transform.up,
			new Vector2(0.1f, 0.1f),
			0f,
			transform.up,
			0.1f
			);
		if(hit.collider == null) {
			Instantiate(
				Resources.Load("Anchor"),
				transform.position + transform.up,
				transform.rotation,
				transform
				);
		}

		hit = Physics2D.BoxCast(
			transform.position - transform.right,
			new Vector2(0.1f, 0.1f),
			0f,
			-transform.right,
			0.1f
			);
		if(hit.collider == null) {
			Instantiate(
				Resources.Load("Anchor"),
				transform.position - transform.right,
				transform.rotation,
				transform
				);
		}

		hit = Physics2D.BoxCast(
			transform.position - transform.up,
			new Vector2(0.1f, 0.1f),
			0f,
			- transform.up,
			0.1f
			);
		if(hit.collider == null) {
			Instantiate(
				Resources.Load("Anchor"),
				transform.position - transform.up,
				transform.rotation,
				transform
				);
		}
		hit = Physics2D.BoxCast(
			transform.position + transform.right,
			new Vector2(0.1f, 0.1f),
			0f,
			transform.right,
			0.1f
			);
		if(hit.collider == null) {
			Instantiate(
				Resources.Load("Anchor"),
				transform.position + transform.right,
				transform.rotation,
				transform
				);
		}
	}

	void DestroyGhosts() {
		foreach (Transform child in transform) {
			if(child.tag == "Ghosts") {
				Destroy(child.gameObject);
			}
		}
	}
}
