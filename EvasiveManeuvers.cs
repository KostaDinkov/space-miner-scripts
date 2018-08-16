using System.Collections;
using UnityEngine;


public class EvasiveManeuvers : MonoBehaviour {

	// Use this for initialization
    public Boundary Boundary;
    private float targetmaneuver;
    public float Dodge;
    public float Smoothing;
    public float Tilt;

    public Vector2 StartWait;
    public Vector2 ManeuverTime;
    public Vector2 ManeuverWait;

    private float currentSpeed;
    private Rigidbody rb;
	void Start ()
	{
	    StartCoroutine(Evade());
	    rb = GetComponent<Rigidbody>();
	    currentSpeed = rb.velocity.z;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetmaneuver, Time.deltaTime * Smoothing);
	    rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, Boundary.xMin, Boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, Boundary.zMin,Boundary.zMax)
            );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -Tilt);
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(StartWait.x, StartWait.y));
        while (true)
        {
            targetmaneuver = Random.Range(1,Dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(ManeuverTime.x, ManeuverTime.y));
            targetmaneuver = 0;
            yield return new WaitForSeconds(Random.Range(ManeuverWait.x,ManeuverWait.y));

        }
        
    }
}
