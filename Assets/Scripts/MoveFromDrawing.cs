using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromDrawing : MonoBehaviour {

    public bool startMoving = false;
    public Vector3[] movementPoints = null;
    public GameObject pencilTip;
    public float moveMultiplyer = 1f;
    public bool flipYX = false;
    public bool flipYZ = false;
    public GameObject victoryObject;

    private Rigidbody rb;
    private int pointCounter = 1;
    private Vector3 previousDrawPoint;
    private Vector3 initialPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = gameObject.transform.position;
        //previousDrawPoint = initialPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FailOnHit")
        {
            Reset();
            rb.transform.position = initialPosition;
        }
        else if (other.tag == "Goal")
        {
            for (int i = 0; i < 100; i++)
            {
                GameObject newSpawn = Instantiate(victoryObject, gameObject.transform.position, Quaternion.identity);
                newSpawn.GetComponent<Transform>().localScale *= 7;
            }
            Debug.Log("Level Complete!");
        }
    }

    private void initializeMovementPoints()
    {
        Debug.Log("**Entered InitializeMovementPoints**");
        movementPoints = pencilTip.GetComponent<DrawableArea>().vertices;
    }

    private Vector3 calculatePointToMoveTo()
    {
        previousDrawPoint = movementPoints[pointCounter - 1];
        Vector3 currentPosition = gameObject.transform.position;
        Vector3 drawPointDifference = (movementPoints[pointCounter] - previousDrawPoint) * moveMultiplyer;
        if (flipYX)
        {
            // le std::swap face
            float temp = drawPointDifference.y;
            drawPointDifference.y = drawPointDifference.x;
            drawPointDifference.x = temp;
        }
        if (flipYZ)
        {
            // le std::swap face
            float temp = drawPointDifference.z;
            drawPointDifference.z = drawPointDifference.y;
            drawPointDifference.y = temp;
        }
        Vector3 nextMovePoint = (currentPosition + drawPointDifference);
        pointCounter++;
        Debug.Log("Calculated nextDrawPoint: " + nextMovePoint);
        return nextMovePoint;
    }

    private void Reset()
    {
        startMoving = false;
        pointCounter = 1;
        movementPoints = new Vector3[0];
        Debug.Log("Reset Complete");
    }

    public void resetPosition()
    {
        gameObject.transform.position = initialPosition;
    }

    // Update is called once per frame
    void FixedUpdate () {
		if(startMoving)
        {
            if(movementPoints.Length == 0)
                initializeMovementPoints();

            if (pointCounter < movementPoints.Length)
                rb.MovePosition(calculatePointToMoveTo() + transform.forward * Time.deltaTime);
            else
                Reset();
        }

    }
}
