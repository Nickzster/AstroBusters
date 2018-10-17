using UnityEngine;
using System.Collections;

public class Asteroids : MonoBehaviour
{
    float offset = 0;
    public Rigidbody Asteroid;
//  public Rigidbody counter;
    public Transform spawnAsteroid;
    public float netForce = 1.0f;
    bool canSpawn = false;
    public float time;
    bool waitActive = false;
    //Difficulty Variables
    public float maxNetForce; //This is the maxinum that the force can be.
    public float maxTime; //This is the maxinum the time can change
   // Use this for initialization

    IEnumerator Wait()
    {
        waitActive = true;
        yield return new WaitForSeconds(time);
        canSpawn = true;
        waitActive = false;
    }
	void Start()
    {

	}

    public void incrementValues(float newTimeFactor, float newForceFactor) //This changes the net force and the rate in which asteroids spawn.
    {
        if(netForce <= maxNetForce) //This is the defined hardest difficulty
        netForce += newForceFactor;
        float timeTemp = time - newTimeFactor;
        if(timeTemp > maxTime) //This makes sure the time does not equal zero. 
        time = timeTemp;
    }

    IEnumerator pause() //Used for spawning asteroids
    {
        yield return new WaitForSeconds(5);
        Time.timeScale = time;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitActive == false)
        {
            StartCoroutine(Wait());
        }
        if(canSpawn == true)
        {
            transform.Translate(0, 0 - offset, 0);
            offset = Random.Range(-7, 7);
            transform.Translate(0, 0 + offset, 0);
           // Asteroid = Instantiate(Asteroid, spawnAsteroid.position, spawnAsteroid.rotation) as Rigidbody;
			Asteroid = Instantiate(Asteroid, spawnAsteroid.position, Quaternion.identity) as Rigidbody;
            Asteroid.AddForce(spawnAsteroid.right * netForce);
            canSpawn = false;
        }
	}
}
