using UnityEngine;
using System.Collections;

public class destruct : MonoBehaviour
{
    public float offset;
	// Use this for initialization
    private Controller interfaceToPlayer;
    public GameObject thisObject;
    public ParticleSystem trails;
    private Transform thisTransform;
    public GameObject explosionObject;
    ParticleSystem explosionFx;
	void Start ()
    {
        interfaceToPlayer = GameObject.FindObjectOfType(typeof(Controller)) as Controller;
        Destroy(gameObject, 5.0f);
        trails.Play();
        explosionFx = explosionObject.GetComponent<ParticleSystem>();
        thisTransform = thisObject.GetComponent<Transform>();
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

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
			ParticleSystem exp = Instantiate(explosionFx, thisTransform.position, Quaternion.identity) as ParticleSystem;
            Destroy(gameObject);
        }
    }
}
