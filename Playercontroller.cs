using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class Playercontroller : MonoBehaviour
{
    public Boundary boundary;
    public float fireRate = 0.5f;
    private float gameSpeed = 5f;

    private bool isMoving;
    private bool isRotating;
    private float nextfire;
    private readonly float playerRotationSpeed = 200;
    private readonly float playerSpeed = 4;
    public GameObject shot;
    public Transform shotSpawn;
    public float speed;

    private float unitSize = 1;

    private void Update()
    {
        var particleSystems = GetComponentsInChildren<ParticleSystem>();

        foreach (var psys in particleSystems)
        {
            var newMain = psys.main;
            newMain.startRotation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        }

        // Player controls
        if (Input.GetButton("Fire1") && Time.time > nextfire)
        {
            nextfire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.I) && !isMoving) StartCoroutine(MoveForward(gameObject, playerSpeed, 2));

        if (Input.GetKeyDown(KeyCode.J) && !isRotating)
        {
            var rotation = Quaternion.Euler(0, -90, 0);
            StartCoroutine(RotateOverSpeed(gameObject, rotation, playerRotationSpeed));
        }

        if (Input.GetKeyDown(KeyCode.L) && !isRotating) RotateRight(90);
    }

    public void RotateRight(float degrees)
    {
        var rotation = Quaternion.Euler(0, degrees, 0);
        StartCoroutine(RotateOverSpeed(gameObject, rotation, playerRotationSpeed));
    }

    public void RotateLeft(float degrees)
    {
        var rotation = Quaternion.Euler(0, -degrees, 0);
        StartCoroutine(RotateOverSpeed(gameObject, rotation, playerRotationSpeed));
    }


    private IEnumerator RotateOverSpeed(GameObject objectToMove, Quaternion end, float speed)
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


    public IEnumerator MoveForward(GameObject objectToMove, float speed, float distance)
    {
        isMoving = true;
        var endPosition = objectToMove.transform.position + transform.forward * distance;
        endPosition = CheckBoundaries(endPosition);

        while (objectToMove.transform.position != endPosition)
        {
            objectToMove.transform.position =
                Vector3.MoveTowards(objectToMove.transform.position, endPosition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isMoving = false;
    }

    private Vector3 CheckBoundaries(Vector3 endPosition)
    {
        if (endPosition.x > boundary.xMax)
        {
            endPosition.x = boundary.xMax;
        }

        if (endPosition.x < boundary.xMin)
        {
            endPosition.x = boundary.xMin;
        }

        if (endPosition.z < boundary.zMin)
        {
            endPosition.z = boundary.zMin;
        }

        if (endPosition.z > boundary.zMax)
        {
            endPosition.z = boundary.zMax;
        }

        return endPosition;
    }
}