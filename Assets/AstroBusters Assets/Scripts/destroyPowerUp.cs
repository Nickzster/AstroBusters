using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyPowerUp : MonoBehaviour {

	public int PowerUp;
	// Use this for initialization

	public AudioSource chargeUp;

	private Asteroids interfaceToAsteroidSpawner;
	private Controller interfaceToPlayer;
	void Start () 
	{	
		interfaceToAsteroidSpawner = GameObject.FindObjectOfType(typeof(Asteroids)) as Asteroids;
		interfaceToPlayer = GameObject.FindObjectOfType(typeof(Controller)) as Controller;
		Destroy(gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void Update ()  
	{
		
	}

	void OnTriggerEnter(Collider other) //need to set this up using the information below! LINK THE ASTEROIDS TO THIS FILE!!******************************
	{
		// Debug.Log("A powerup has a collision!"); //Need to change this from a "collision" to a "trigger collision".
		if(other.gameObject.CompareTag("Player"))
		{
			// Debug.Log("And it has hit a player!!!");
			switch(PowerUp)
			{
				case 0: //Easy
					Debug.Log("===EASY===");
					interfaceToAsteroidSpawner.resetDifficulty();
				break;
				case 1: //Hard
					Debug.Log("===HARD===");
					interfaceToAsteroidSpawner.changeAsteroidSpeedAndRate(0.0f, 100.0f);
				break;
				case 2: //Shield
					Debug.Log("===SHIELD===");
					interfaceToPlayer.spawnShield();
					interfaceToPlayer.chargeUp();

				break;
				case 3: //x2
					Debug.Log("===X2===");
					interfaceToAsteroidSpawner.changeAsteroidSpeedAndRate(1.0f, 0.0f);
				break;
			}
			Destroy(gameObject);
		}
	}
}
