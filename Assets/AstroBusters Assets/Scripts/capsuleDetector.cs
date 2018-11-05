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

    private void changeDifficulty(float time, float force)
    {
        other.changeAsteroidSpeedAndRate(time, force); //This increases the difficulty
    }

    public int getScore()
    {
        return count;
    }

    private void increment()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null) //if the player dodges an asteroid
        {
            counterText.text = "Asteroids Dodged: " + count; //increase the dodge count
            changeDifficulty(0.005f, 5.0f); //and make it harder
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
        if(other.gameObject.tag == "asteroid") //if the asteroid is dodged
        {
            
            //  Debug.Log("An asteroid has hit the capsule!(1)");
            count++; //then increment the dodge count
            increment(); //and update player stats.
        }
    }
}
