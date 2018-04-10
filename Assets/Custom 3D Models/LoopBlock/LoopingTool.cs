using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingTool : MonoBehaviour {
    public int loopCount = 1;
    public float loopIncreaseSpeed = 70;
    public float floor = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tool" && other.gameObject.GetComponent<LoopingBlock>())
        {
            other.gameObject.GetComponent<LoopingBlock>().loopCount = loopCount;
        }
    }

    private void Update()
    {
        if(gameObject.transform.localScale.x >= floor)
            loopCount =  (int)(gameObject.transform.localScale.x + (gameObject.transform.localScale.x - floor)*loopIncreaseSpeed);
    }
}
