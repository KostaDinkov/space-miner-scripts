using System;
using System.Collections;
using Assets.Scripts.Commands;
using Assets.Scripts.Commands.PlayerCommands;

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
    private CommandQueue commandQueue = new CommandQueue();

    private bool isIdle = true;
    private float nextfire;
    private readonly float playerRotationSpeed = 200;
    private readonly float playerSpeed = 4;
    public GameObject shot;
    public Transform shotSpawn;
    public float speed;
    public MoveForward MoveForwardCommand;
    public RotateLeft RotateLeftCommand;
    public RotateRight RotateRightCommand;
    private float unitSize = 1;

    public Playercontroller()
    {
        this.MoveForwardCommand = new MoveForward(this);
        this.RotateLeftCommand = new RotateLeft(this);
        this.RotateRightCommand = new RotateRight(this);
    }

    private void Update()
    {
        //execute the next command in the command queue
        if (this.isIdle && !this.commandQueue.IsEmpty())
        {
            commandQueue.Execute();
        }

        var particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (var psys in particleSystems)
        {
            var newMain = psys.main;
            newMain.startRotation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        }

        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetButton("Fire1") && Time.time > nextfire)
        {
            nextfire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.I) )
        {
            this.commandQueue.Enqueue(MoveForwardCommand);
            
        }

        if (Input.GetKeyDown(KeyCode.J)  )
        {
            this.commandQueue.Enqueue(RotateLeftCommand);
        }

        if (Input.GetKeyDown(KeyCode.L) )
        {
            this.commandQueue.Enqueue(RotateRightCommand);
        }
    }

    public IEnumerator MoveForward()
    {
        isIdle = false;
        var endPosition = this.transform.position + transform.forward * GameData.gridSize;
        endPosition = CheckBoundaries(endPosition);

        while (this.transform.position != endPosition)
        {
            this.transform.position =
                Vector3.MoveTowards(this.transform.position, endPosition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        isIdle = true;
    }

    public void RotateRight(float degrees = 90)
    {
        var rotation = Quaternion.Euler(0, degrees, 0);
        StartCoroutine(RotateOverSpeed(gameObject, rotation, playerRotationSpeed));
    }

    public void RotateLeft(float degrees = 90)
    {
        var rotation = Quaternion.Euler(0, -degrees, 0);
        StartCoroutine(RotateOverSpeed(gameObject, rotation, playerRotationSpeed));
    }


    private IEnumerator RotateOverSpeed(GameObject objectToMove, Quaternion end, float speed)
    {
        isIdle = false;
        var endRotation = objectToMove.transform.rotation * end;
        while (objectToMove.transform.rotation != endRotation)
        {
            objectToMove.transform.rotation =
                Quaternion.RotateTowards(objectToMove.transform.rotation, endRotation, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        isIdle = true;
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