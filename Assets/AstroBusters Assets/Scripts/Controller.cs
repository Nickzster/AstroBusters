﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

	private bool gameStart;
    public float speed = 1.0f;
    public AudioSource explosionSound;

	public AudioSource discharge;

	public AudioSource charge;
    public GameObject explosion;
    ParticleSystem explosionFx;
    bool change; //tells the script that the direction needs to change.
    bool toggle = true; //tells the script which direction the ship needs to go.
    public GameObject player;
	public GameObject[] ships; //refers to the selectible ships
	public GameObject[] shields; //directly refers to the selected ships. Used to active or deactivate a ship's shield.

	private int selectedShip; //used by shield, to determine which shield to activate.

	private bool shielded = false; //determines whether the player is shielded or not
	private bool toggleShield = false; //determines whether the shield needs to be toggled.

	public Asteroids other;

	private CapsuleCollider shipCollision;

	private Transform playerTransform;

    public GameObject Counter; //Reference to the Capsule Counter
    public capsuleDetector counter; //Reference to the Capsule Counter script
	public int[] scoreThresholds; //50,100,200,500,1000


    public GameObject Menu;
    //input controls

	public void chargeUp()
	{
		charge.Play();
	}
	void chargeDown()
	{
		discharge.Play();
	}
	void setCollisionValues(CapsuleCollider shipCollision, float newRadius, float newHeight)
	{
		shipCollision.radius = newRadius;
		shipCollision.height = newHeight;
	}

	public bool Shielded()
	{
		return this.shielded;
	}
	private void shieldToggle()
	{
		this.toggleShield = true;
	}
	public void spawnShield()
	{
		this.shielded = true;
		shieldToggle();
	}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }

	public void destroyShield()
	{
		shieldToggle();
		this.shielded = false;
	}
    // Use this for initialization
    void Start()
    {
		gameStart = false;
		Application.targetFrameRate = 144;
        GetComponent<Rigidbody>().useGravity = false;
        toggle = true;
		//Initalize ship
		int SelectShip = PlayerPrefs.GetInt("selected_ship");
		this.selectedShip = SelectShip;
		shipCollision = GetComponent<CapsuleCollider>();
//		Sprite currentShip = GetComponent<SpriteRenderer> ();
		//Set the sprite for the ship
		ships[SelectShip].SetActive (true);
		//Then set its capsule collider properties
		switch (SelectShip) 
		{
		case 0:
			setCollisionValues (shipCollision, 1.01f, 2.71f);
			break;
		case 1:
			setCollisionValues (shipCollision, 1.06f, 3.31f);
			break;
		case 2:
			setCollisionValues (shipCollision, 0.85f, 3.27f);
			break;
		case 3:
			setCollisionValues (shipCollision, 1.49f, 2.58f);
			break;
		case 4:
			setCollisionValues (shipCollision, 1.09f, 2.58f);
			break;
		case 5:
			setCollisionValues (shipCollision, 1.09f, 2.58f);
			break;
		default:
			break;
		}
		//Initalize the explosion
		 this.explosionFx = explosion.GetComponent<ParticleSystem>();
		 playerTransform = player.GetComponent<Transform>();
    }

    private void Save(int checkScore)
    {
        PlayerPrefs.SetInt("High_Score", checkScore); //Set the new high score.
        Debug.Log("The score " + checkScore + " was saved.");
    }

    public void changeDirection()
    {
        GetComponent<Rigidbody>().velocity = Vector3.down* speed; //Pushes the object in the desired direction
        // Debug.Log("Vector Shifting");
	    speed *= -1.0f; //changes the direction
    }

    // Update is called once per frame
	void mobileInput()
	{
		if(Input.touchCount == 1 && toggle == true)
		{
			if(!gameStart){ other.startGame(); this.gameStart = false;}
			change = true;
			// Debug.Log("True");
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
	}
    void Update()
    {

        //WASD Version of Code
#if UNITY_STANDALONE_WIN || UNITY_WEBGL
		// Debug.Log("Stand Alone WINDOWS");
        if(Input.GetKeyDown("space") && toggle == true) //If the player presses the space bar, and the direction needs to change
		{
			if(!gameStart){ other.startGame(); this.gameStart = false;}
			change = true; //then change the direction
			// Debug.Log("True");
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
		mobileInput();
		Debug.Log("Stand Alone IOS");
#endif

#if UNITY_ANDROID
		mobileInput();
		Debug.Log("Stand Alone ANDROID");
#endif
		if(shielded)
		{
			if(toggleShield)
			{
				//activate shield
				shields[selectedShip].SetActive(true);
				// shields[selectedShip].fadeObjectIn();
				toggleShield = false;
			}

		}
		else
		{
			if(toggleShield)
			{
				shields[selectedShip].SetActive(false);
				//deactivate shield
				toggleShield = false;
			}

		}
    }

	void unlockShips(int score)
	{
		Debug.Log ("UNLOCK SHIPS CALLED");
		if (score >= scoreThresholds [0] && PlayerPrefs.GetInt("ship1") == 0)
		{
			PlayerPrefs.SetInt ("ship1", 1);
			Debug.Log ("Player hit " + scoreThresholds[0]);
		} 
		if (score >= scoreThresholds [1] && PlayerPrefs.GetInt("ship2") == 0) 
		{
			PlayerPrefs.SetInt ("ship2", 1);
			Debug.Log ("Player hit " + scoreThresholds[1]);
		} 
		if (score >= scoreThresholds [2] && PlayerPrefs.GetInt("ship3") == 0) 
		{ 
			PlayerPrefs.SetInt ("ship3", 1);
			Debug.Log ("Player hit " + scoreThresholds[2]);
		} 
		if (score >= scoreThresholds [3] && PlayerPrefs.GetInt("ship4") == 0) 
		{
			PlayerPrefs.SetInt ("ship4", 1);
			Debug.Log ("Player hit " + scoreThresholds[3]);
		} 
		if (score >= scoreThresholds [4] && PlayerPrefs.GetInt("ship5") == 0) 
		{
			PlayerPrefs.SetInt ("ship5", 1);
			Debug.Log ("Player hit " + scoreThresholds[4]);
		}
	}

    void OnCollisionEnter(Collision col)
    {
        // Debug.Log("A collision has occured!(1)");
        if (col.gameObject.CompareTag("asteroid") && !(shielded)) //if an asteroid hits the player, and is not shielded.
        {
			explosionSound.Play();
			ParticleSystem exp = Instantiate(explosionFx, playerTransform.position, Quaternion.identity) as ParticleSystem;
			StartCoroutine("Wait");
            Menu.SetActive(true);
            int checkScore = PlayerPrefs.GetInt("High_Score");
            // Debug.Log(checkScore);
            // Debug.Log(counter.getScore());
			unlockShips(counter.getScore());
            if (checkScore != 0) //if high score has been initalized
            {
				if (checkScore < counter.getScore ())//If the score that was met in this round is higher than check score
				{ 
					Debug.Log ("We have a new high score!");
					checkScore = counter.getScore (); //Set checkscore to this value. This is the newest high score.
					Save (checkScore);

				}

            }
            else //if high score has not been initalized
            {
                Debug.Log("We have a new score.");
                Save(counter.getScore());
            }
			Destroy(gameObject);
			Destroy(this);
        }
		else if(col.gameObject.CompareTag("asteroid") && shielded) //if an asteroid hits the player, and it is shielded
		{
			destroyShield();
			chargeDown();
		}
        else if(col.gameObject.CompareTag("sides"))
        {
            changeDirection();
        }
        //SceneManager.LoadScene("astroidBusters");
    }

}
