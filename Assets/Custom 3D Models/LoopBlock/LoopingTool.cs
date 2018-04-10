using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingTool : MonoBehaviour {
    public int loopCount = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tool" && other.gameObject.GetComponent<LoopingBlock>())
        {
            other.gameObject.GetComponent<LoopingBlock>().loopCount = loopCount;
        }
    }
}
