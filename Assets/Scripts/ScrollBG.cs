using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScrollBG : MonoBehaviour
{	
	Rigidbody2D Player;
	Renderer R;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Ship").GetComponent<Rigidbody2D>();
        R = GetComponent<Renderer>();

        Texture2D texture = LoadPNG("BG");
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0.0f, 0.0f, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            16.0f);
        R.material.mainTexture= texture;
    }

    // Update is called once per frame
    void Update()
    {
        R.material.mainTextureOffset += (Vector2)Player.velocity * Time.deltaTime * 0.05f; 
    }

    Texture2D LoadPNG(string filePath) {
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
