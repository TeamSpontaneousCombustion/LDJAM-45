using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;



public class SetImage : MonoBehaviour
{
	#pragma warning disable 649
	[SerializeField]
	string Name;
	#pragma warning restore 649
	// Start is called before the first frame update
	void Start()
	{
		Texture2D texture = LoadPNG(Name);
		GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
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
