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
            try
            {
                GameObject.Find("EditorBoard").gameObject.SetActive(false);
                GameObject.Find("DrawingCanvas").gameObject.SetActive(false);
            }
            catch
            { }
            Debug.Log("Controller in compile/run block");
            bool handledByLoop = false;

            GameObject[] tools = GameObject.FindGameObjectsWithTag("Tool");
            foreach (GameObject tool in tools)
            {
                if (tool.GetComponent<LoopingBlock>())
                {
                    Debug.Log("Calling loop from inside compileAndRun");
                    tool.GetComponent<LoopingBlock>().startLoop = true;
                    handledByLoop = true;
                }
                if (tool.name.Contains("Node"))
                {
                    tool.transform.position = new Vector3(100, 100, 100);
                }
            }

            if (!handledByLoop)
                moveFromDrawingScript.startMoving = true;
        }
    }
}
