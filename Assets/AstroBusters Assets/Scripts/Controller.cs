using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float speed = 1.0f;
    public AudioSource explosionSound;
    public GameObject explosion;
    bool change; //tells the script that the direction needs to change.
    bool toggle = true; //tells the script which direction the ship needs to go.
    public GameObject player;
    public GameObject Counter; //Reference to the Capsule Counter
    public capsuleDetector counter; //Reference to the Capsule Counter script


    public GameObject Menu;
    //input controls


    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        toggle = true;
    }

    private void Save(int checkScore)
    {
        PlayerPrefs.SetInt("High_Score", checkScore); //Set the new high score.
        Debug.Log("The score " + checkScore + " was saved.");
    }

    public void changeDirection()
    {
        GetComponent<Rigidbody>().velocity = Vector3.down* speed; //Pushes the object in the desired direction
        Debug.Log("Vector Shifting");
	    speed *= -1.0f; //changes the direction
    }

    // Update is called once per frame
    void Update()
    {

        //WASD Version of Code
#if UNITY_STANDALONE_WIN

        if(Input.GetKeyDown("space") && toggle == true) //If the player presses the space bar, and the direction needs to change
		{
			change = true; //then change the direction
			Debug.Log("True");
			toggle = false; 
		}
		else //If the player is not pressing the keyboard
		{
			change = false; //Then we need to get ready for when the spacebar is pressed
//			Debug.Log("False");
			toggle = true;
		}
		if(change == true) 
		{
            changeDirection();
		}
#endif

#if UNITY_IOS
        //Mobile Version of Code. This has been tested on Android and iPhone

        if(Input.touchCount == 1 && toggle == true)
        {
            change = true;
            Debug.Log("True");
            toggle = false; 
        }
        else
        {
            change = false;
        }
        if(change == true)
        {
            changeDirection();
        }
        if(Input.touchCount == 0)
        {
            toggle = true;
        }
        else
        {
            toggle = false;
        } 
#endif

    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("A collision has occured!");
        if (col.gameObject.CompareTag("asteroid"))
        {
            Destroy(this);
            explosionSound.Play();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Menu.SetActive(true);
            int checkScore = PlayerPrefs.GetInt("High_Score");
            Debug.Log(checkScore);
            Debug.Log(counter.getScore());
            if (checkScore != 0)
            {
                if (checkScore < counter.getScore()) //If the score that was met in this round is higher than check score
                {
                    Debug.Log("We have a new high score!");
                    checkScore = counter.getScore(); //Set checkscore to this value. This is the newest high score.
                    Save(checkScore);
                }
            }
            else
            {
                Debug.Log("We have a new score.");
                Save(counter.getScore());
            }
        }
        if(col.gameObject.CompareTag("sides"))
        {
            changeDirection();
        }

        //SceneManager.LoadScene("astroidBusters");
    }

}
