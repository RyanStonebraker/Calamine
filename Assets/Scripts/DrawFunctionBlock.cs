using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFunctionBlock : MonoBehaviour {

    public Vector3[] movementPoints;
    public GameObject moveFromDrawing;

    public void sendToMoveFromDrawing()
    {
        moveFromDrawing.GetComponent<MoveFromDrawing>().movementPoints = movementPoints;
    }

    public void getMovementPoints(Vector3[] trailRenderPoints)
    {
        movementPoints = trailRenderPoints;
    }

}
