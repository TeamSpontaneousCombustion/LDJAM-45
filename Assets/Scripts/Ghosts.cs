using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{	
	PlayerMovement pm;
    // Start is called before the first frame update
	void Start()
	{
		pm = GameObject.Find("Ship").GetComponent<PlayerMovement>();
	}

	void CreateAnchors() {
		RaycastHit2D hit;
		Vector2[] dirs = new Vector2[] {transform.up, -transform.up, -transform.right, transform.right};
		for(int i = 0; i < dirs.Length; i++) {
			hit = Physics2D.BoxCast(
			(Vector2)transform.position + dirs[i],
			new Vector2(0.1f, 0.1f),
			0f,
			dirs[i],
			0.1f,
			1 << 11
			);
			if(hit.collider == null) {
				Instantiate(
				Resources.Load("Anchor"),
				(Vector2)transform.position + dirs[i],
				transform.rotation,
				transform
				);
			}
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
