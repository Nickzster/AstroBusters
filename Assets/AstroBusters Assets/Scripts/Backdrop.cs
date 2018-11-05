using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backdrop : MonoBehaviour {

	// Use this for initialization

	public float scrollSpeed = 0.5f;
	public float tileSizeZ;

	private Vector3 startPosition;
	Renderer rend;
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
