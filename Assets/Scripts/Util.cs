using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Util
{
    	public static JsonData Parse(string Variable) {
		string path = Application.dataPath + "/../Data/LDJAM45.tsc";
		string jsonString = File.ReadAllText(path);
		JsonData _JsonData = JsonMapper.ToObject(jsonString);
		return _JsonData[Variable];
	}
}
