using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBlock : MonoBehaviour {
    public bool startLoop = false;
    public int loopCount = 1;
    GameObject compileBlock;
    private MoveFromDrawing moveFromDrawingScript;

    private void Start()
    {
        compileBlock = GameObject.Find("StartBlock");
        moveFromDrawingScript = GameObject.Find("MoveFromDraw").GetComponent<MoveFromDrawing>();
    }
    void Update () {
        if (startLoop && loopCount > 0 && moveFromDrawingScript.startMoving == false)
        {
            moveFromDrawingScript.startMoving = true;
            --loopCount;
        }
        if (startLoop && loopCount <= 0)
        {
            startLoop = false;
            loopCount = 1;
        }
	}
}
