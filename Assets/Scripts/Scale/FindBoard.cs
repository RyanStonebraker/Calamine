using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBoard : MonoBehaviour {

    public int yieldFrameCounter = 0;
    public float frameUpAmount = 0.03f;

    void Update () {

        if (!GameObject.Find("PlayerBoard").GetComponent<Board>().yieldViewForCompile)
            return;

        if (yieldFrameCounter < 200)
        {
            transform.Translate(Vector3.up * frameUpAmount);
            yieldFrameCounter++;
        }
        else if (yieldFrameCounter == 200)
        {
            yieldFrameCounter++;
        }
    }
}
