using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetArt : MonoBehaviour
{
	[SerializeField]
	Sprite[] sprites = new Sprite[16];
	int PlayerLayer = 11;
	SpriteRenderer SR;
	[SerializeField]
	bool[] Hits = new bool[4];
	enum Directions {
		Up, Down, Left, Right
	}
	void ChangeArt()
	{
		SR = GetComponent<SpriteRenderer>();
		RaycastHit2D hit;
		Vector2[] dirs = new Vector2[] {transform.up, -transform.up, -transform.right, transform.right};
		int TotalHits = 0;
		for(int i = 0; i < 4; i++) {
			Hits[i] = false;
		}
		for(int i = 0; i < dirs.Length; i++) {
			hit = Physics2D.Raycast(
			(Vector2)transform.position + (Vector2)dirs[i] * 0.7f,
			dirs[i],
			0.2f,
			1 << PlayerLayer
			);
			if(hit.collider != null && hit.collider.gameObject != gameObject) {
				Hits[i] = true;
				TotalHits++;
			}
		}
		if(TotalHits == 0) {
			SR.sprite = sprites[11];
		}
		if(TotalHits == 1) {
			if(Hits[(int)Directions.Up]) {
				SR.sprite = sprites[13];
			}
			if(Hits[(int)Directions.Down]) {
				SR.sprite = sprites[12];
			}
			if(Hits[(int)Directions.Left]) {
				SR.sprite = sprites[14];
			}
			if(Hits[(int)Directions.Right]) {
				SR.sprite = sprites[15];
			}
		}
		if(TotalHits == 2) {
			if(Hits[(int)Directions.Up] && Hits[(int)Directions.Down]) {
				SR.sprite = sprites[10];
			}
			if(Hits[(int)Directions.Left] && Hits[(int)Directions.Right]) {
				SR.sprite = sprites[9];
			}

			if(Hits[(int)Directions.Down] && Hits[(int)Directions.Right]) {
				SR.sprite = sprites[0];
			}
			if(Hits[(int)Directions.Down] && Hits[(int)Directions.Left]) {
				SR.sprite = sprites[2];
			}
			if(Hits[(int)Directions.Up] && Hits[(int)Directions.Right]) {
				SR.sprite = sprites[6];
			}
			if(Hits[(int)Directions.Up] && Hits[(int)Directions.Left]) {
				SR.sprite = sprites[8];
			}

		}
		if(TotalHits == 3) {
			if(!Hits[(int)Directions.Up]) {
				SR.sprite = sprites[1];
			}
			if(!Hits[(int)Directions.Down]) {
				SR.sprite = sprites[7];
			}
			if(!Hits[(int)Directions.Left]) {
				SR.sprite = sprites[3];
			}
			if(!Hits[(int)Directions.Right]) {
				SR.sprite = sprites[5];
			}
		}
		if(TotalHits == 4) {
			SR.sprite = sprites[4];
		}
	}
}
