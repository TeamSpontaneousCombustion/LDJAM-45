using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
	[SerializeField]
	public float EnginePower = 3;
	void Start() {
		EnginePower = float.Parse(Util.Parse("Player")["EnginePower"].ToString());
	}
}
