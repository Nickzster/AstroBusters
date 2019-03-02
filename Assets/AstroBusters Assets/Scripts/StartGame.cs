using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	private Text text;

	// Use this for initialization
	void Start () 
	{
		text = GetComponent<Text>();
		#if UNITY_EDITOR || UNITY_STANDALONE
			text.text = "Press Space Bar to change directions.";
		#elif UNITY_IOS || UNITY_ANDROID
			text.text = "Tap the screen to change directions.";
		#else
			text.text = "Press Space Bar to change directions.";
		#endif
	}

	public void DestroyStartText()
	{
		text.text = "";
	}
	
	// Update is called once per frame

}
