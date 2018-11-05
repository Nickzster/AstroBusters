using UnityEngine;

public class ExplosionPackDemo : MonoBehaviour
{
    ParticleSystem[] explosions;
    int i = 0;
    string message;
    private GUIStyle guiStyle = new GUIStyle();

    // Use this for initialization
    void Start ()
    {
        explosions = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem explosion in explosions)
        {
            explosion.Pause();
        }
        explosions[0].Play();
        message = "A - Back,   S - Current,   D - Next";
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            i--;
            if (i < 0) i = explosions.Length-1;
            ExplosionPlay(i);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            explosions[i].Play();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            i++;
            if (i >= explosions.Length) i = 0;
            ExplosionPlay(i);
        }
    }


    void ExplosionPlay(int i)
    {
        explosions[i].Play();
        explosions[i].transform.position = new Vector3(Random.Range(-5, 6), Random.Range(-2, 3), 0);
    }


    private void OnGUI()
    {
        guiStyle.fontSize = 16;
        guiStyle.normal.textColor = new Color(1, 1, 1, 0.9f);
        GUI.Label(new Rect(10, 10, 500, 30), message, guiStyle);
        GUI.Label(new Rect(10, 30, 500, 30), "Object:  " + explosions[i].name, guiStyle);
    }
}
