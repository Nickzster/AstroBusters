using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class capsuleDetector : MonoBehaviour
{

    // Use this for initialization
    public Text counterText; //holds reference to countText in game.
    private int count;
    public Asteroids other;

    private void increaseDifficulty()
    {
        other.incrementValues(0.005f, 5.0f); //This increases the difficulty
    }

    private void increment()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            counterText.text = "Asteroids Dodged: " + count;
            increaseDifficulty();
        }
    }

	void start ()
    {
        count = 0;
        increment();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "asteroid")
        {
            Debug.Log("An asteroid has hit the capsule!");
            count++;
            increment();
        }
    }
}
