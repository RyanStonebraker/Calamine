using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawableArea : MonoBehaviour {

    public GameObject PencilTip;
    public bool insideDrawArea = false;
    public Vector3[] vertices;
    public GameObject objectToMoveFromDrawing;

    private MoveFromDrawing objectToMoveFromDrawingScript;

    private void Start()
    {
        objectToMoveFromDrawingScript = objectToMoveFromDrawing.GetComponent<MoveFromDrawing>();
    }

    private void createVertexList()
    {
        TrailRenderer curentLine = PencilTip.GetComponent<TrailRenderer>();
        vertices = new Vector3[curentLine.positionCount];
        curentLine.GetPositions(vertices);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DrawArea")
        {
            //PencilTip = Instantiate(PencilTip); //MULTILINE IDEA
            objectToMoveFromDrawingScript.startMoving = false;
            objectToMoveFromDrawingScript.resetPosition();
            PencilTip.gameObject.GetComponent<TrailRenderer>().enabled = true;
            insideDrawArea = true;
            Debug.Log("Trail Renderer is " + PencilTip.gameObject.GetComponent<TrailRenderer>().enabled);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "DrawArea")
        {
            createVertexList();
            objectToMoveFromDrawingScript.startMoving = true;
            PencilTip.gameObject.GetComponent<TrailRenderer>().enabled = false;
            insideDrawArea = false;
            Debug.Log("Trail Renderer is " + PencilTip.gameObject.GetComponent<TrailRenderer>().enabled);
        }
    }
}
