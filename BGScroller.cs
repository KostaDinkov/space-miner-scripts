using UnityEngine;


public class BGScroller : MonoBehaviour
{

    public float ScrollSpeed;

    public float tileSizeZ;

    private Vector3 startPosition;
	// Use this for initialization
	void Start ()
	{
	    startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float newPosition = Mathf.Repeat(Time.time * ScrollSpeed, tileSizeZ );
	    transform.position = startPosition + Vector3.forward * newPosition;
	}
}
