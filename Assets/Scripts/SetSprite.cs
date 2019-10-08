using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class SetSprite : MonoBehaviour
{
	#pragma warning disable 649
	[SerializeField]
	string Name;
	#pragma warning restore 649
	// Start is called before the first frame update
	void Start()
	{
		Texture2D texture = LoadPNG(Name);
		Sprite sprite = Sprite.Create(
			texture,
			new Rect(0.0f, 0.0f, texture.width, texture.height),
			new Vector2(0.5f, 0.5f),
			16.0f);
		GetComponent<SpriteRenderer>().sprite = sprite;

	    Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        BoxCollider2D BC = GetComponent<BoxCollider2D>();
        if(BC != null) {
	        BC.size = S;
    	    BC.offset = new Vector2 (0, 0);	
        }

	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public static Texture2D LoadPNG(string filePath) {
		filePath = Application.dataPath + "/../Data/" + filePath + ".png";
		Texture2D tex = null;
		byte[] fileData;

		if (File.Exists(filePath)) {
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
		 tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		} else {
			print("NOT FOUND D: " + filePath);
		}
		return tex;
	}
}
