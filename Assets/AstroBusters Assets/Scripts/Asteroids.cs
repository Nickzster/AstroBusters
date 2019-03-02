using UnityEngine;
using System.Collections;

public class Asteroids : MonoBehaviour
{
    float offset = 0;
    public Rigidbody[] spawnObjects;
//  public Rigidbody counter;
    public Transform spawnAsteroid;

    public StartGame startText;

    private bool gameHasStarted;
    public float currentAsteroidSpeed;
    bool canSpawn = false;
    public float currentAsteroidDelayTime;
    bool waitActive = false;
    //Difficulty Variables
    public float maxAsteroidSpeed; //This is the maxinum that the speed(force) can be.
    public float maxAsteroidDelayTime; //This is the maxinum the time can change
   // Use this for initialization

    IEnumerator Wait()
    {
        waitActive = true;
        yield return new WaitForSeconds(currentAsteroidDelayTime);
        canSpawn = true;
        waitActive = false;
    }
	void Start()
    {
        this.gameHasStarted = false;

	}

    public void startGame()
    {
        this.gameHasStarted = true;
        startText.DestroyStartText();
    }

    public void changeAsteroidSpeedAndRate(float newAsteroidDelayTimeFactor, float newAsteroidSpeedFactor) //This changes the net force and the rate in which asteroids spawn.
    {
        float newSpeed = currentAsteroidSpeed + newAsteroidSpeedFactor; //calculate the new netforce.
        Debug.Log("PARAMETERS: newTimeFactor(" + newAsteroidDelayTimeFactor + "), newForceFactor(" + newAsteroidSpeedFactor + ")");
        if(newSpeed < maxAsteroidSpeed) //If the new force factor is less than the max.
        {
            currentAsteroidSpeed = newSpeed; //then update it accordingly.
        }
        else //Otherwise, it is greater than or equal to.
        {
            currentAsteroidSpeed = maxAsteroidSpeed; //and we will set it to the max force.
        }

        float newTime = currentAsteroidDelayTime - newAsteroidDelayTimeFactor;
        if(newTime > maxAsteroidDelayTime) //If the new time is greater than the max time.
        {
            currentAsteroidDelayTime = newTime; //then set it to the new factor
        }
        else //Otherwise, it is equal to or less than, so set it to the max.
        {
            currentAsteroidDelayTime = maxAsteroidDelayTime;
        }
        Debug.Log("****CURRENT TIME: " + currentAsteroidDelayTime + " ****");
        Debug.Log("****CURRENT FORCE: " + currentAsteroidSpeed + " ****");
    }

    public void resetDifficulty()
    {
    Debug.Log(">>>>>RESET DIFFICULTY<<<<<");
        currentAsteroidDelayTime = 1.5f;
        currentAsteroidSpeed = 500f;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameHasStarted)
        {
            if(waitActive == false)
            {
                StartCoroutine(Wait());
            }
            if(canSpawn == true)
            {
                transform.Translate(0, 0 - offset, 0); //Move the spawner back to the middle
                offset = Random.Range(-7, 7); //Then pick a random direction from 7 units up to 7 units down
                transform.Translate(0, 0 + offset, 0); //and then move the spawner in that new position.
                //there is a 1 in 10 chance that a goodie will spawn
                int spawnAGoodie = Random.Range(0,9);
                int goodieToSpawn = 0;
                // Debug.Log(spawnAGoodie);
                if(spawnAGoodie == 0) //if it is zero,
                {
                    //then a goodie spawns.
                    // Debug.Log("Spawn a goodie!");
                    goodieToSpawn = Random.Range(0,4);
                    Rigidbody instance;
                    Debug.Log("Spawning goodie: " + goodieToSpawn); //0 = easy, 1 = hard, 2 = shield, 3 = x2
                    instance = Instantiate(spawnObjects[goodieToSpawn+1], spawnAsteroid.position, Quaternion.identity) as Rigidbody; //then spawn the asteroid
                    instance.AddForce(spawnAsteroid.right * currentAsteroidSpeed); //and shoot it at the player.
                    goodieToSpawn = 0;
    
                } 
                else
                {
                // Asteroid = Instantiate(Asteroid, spawnAsteroid.position, spawnAsteroid.rotation) as Rigidbody;
                    Rigidbody instance;
                    instance = Instantiate(spawnObjects[0], spawnAsteroid.position, Quaternion.identity) as Rigidbody; //then spawn the asteroid
                    instance.AddForce(spawnAsteroid.right * currentAsteroidSpeed); //and shoot it at the player.
                    
                }
                spawnAGoodie = 9;
                canSpawn = false; //then wait until we can spawn again.
            }
        }
	}
}
