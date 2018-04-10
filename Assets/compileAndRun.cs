using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compileAndRun : MonoBehaviour {

    public GameObject moveFromDrawing;
    private MoveFromDrawing moveFromDrawingScript;

    private void Start()
    {
        moveFromDrawingScript = moveFromDrawing.GetComponent<MoveFromDrawing>();

        if (!moveFromDrawing)
            Debug.Log("Failed to init MoveFromDrawScript in compileAndRun");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Contains("Controller"))
        {
            Debug.Log("Controller in compile/run block");
            moveFromDrawingScript.startMoving = true;
        }
    }
}
