using UnityEngine;
using System.Collections;

public class destruct : MonoBehaviour
{
    public float offset;
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 5.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        int sw = Random.Range(0, 1);
        if(sw == 0)
        {
            offset = Random.Range(0, 5);
        }
        else
        {
            offset = Random.Range(-5, 0);
        }

        transform.Rotate(0, 0, 0 + offset);
	}
}
