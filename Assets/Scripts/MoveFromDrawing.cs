using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromDrawing : MonoBehaviour {

    public bool startMoving = false;
    public Vector3[] movementPoints = null;
    public GameObject pencilTip;
    public float moveMultiplyer = 1f;

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
        Vector3 nextMovePoint = (currentPosition + drawPointDifference);
        pointCounter++;
        Debug.Log("Calculated nextDrawPoint: " + nextMovePoint);
        return nextMovePoint;
    }

    private void Reset()
    {
        startMoving = false;
        pointCounter = 1;
        //previousDrawPoint = initialPosition;
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
