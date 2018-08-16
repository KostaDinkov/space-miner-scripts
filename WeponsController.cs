using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponsController : MonoBehaviour {

	// Use this for initialization
    private AudioSource audioSource;
    public GameObject shot;
    public Transform shotSpawn;
    public float FireRate;
    public float shotDelay;

	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
        InvokeRepeating("FireWeapon", shotDelay, FireRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FireWeapon()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
