using System.Collections;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class Playercontroller : MonoBehaviour
{
    public float speed;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.5f;
    private float nextfire = 0.0f;

    private float unitSize = 1;
    private float playerSpeed = 2;
    private float playerRotationSpeed = 200;
    private float gameSpeed = 5f;

    private bool isMoving = false;
    private bool isRotating = false;

    void Update()
    {
        var particleSystems = GetComponentsInChildren<ParticleSystem>();

        foreach (var psys in particleSystems)
        {
            ParticleSystem.MainModule newMain = psys.main;
            newMain.startRotation = this.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        }

        if (Input.GetButton("Fire1") && Time.time > nextfire)
        {
            nextfire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isMoving)
            {
                StartCoroutine(MoveForward(this.gameObject, this.playerSpeed));
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!isRotating)
            {
                var rotation = Quaternion.Euler(0, -90, 0);
                StartCoroutine(RotateOverSpeed(this.gameObject, rotation, playerRotationSpeed));
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!isRotating)
            {
                var rotation = Quaternion.Euler(0, 90, 0);
                StartCoroutine(RotateOverSpeed(this.gameObject, rotation, playerRotationSpeed));
            }
        }

        
    }


    public IEnumerator RotateOverSpeed(GameObject objectToMove, Quaternion end, float speed)
    {
        isRotating = true;
        var endRotation = objectToMove.transform.rotation * end;
        while (objectToMove.transform.rotation != endRotation)
        {
            objectToMove.transform.rotation =
                Quaternion.RotateTowards(objectToMove.transform.rotation, endRotation, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        isRotating = false;
    }


    public IEnumerator MoveForward(GameObject objectToMove, float speed)
    {
        isMoving = true;
        var endPosition = objectToMove.transform.position + transform.forward*2;

        if (endPosition.x > boundary.xMax) endPosition.x = boundary.xMax;
        if (endPosition.x < boundary.xMin) endPosition.x = boundary.xMin;
        if (endPosition.z < boundary.zMin) endPosition.x = boundary.zMin;
        if (endPosition.z > boundary.zMax) endPosition.z = boundary.zMax;

        while (objectToMove.transform.position != endPosition)
        {
            objectToMove.transform.position =
                Vector3.MoveTowards(objectToMove.transform.position, endPosition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        isMoving = false;
    }
}