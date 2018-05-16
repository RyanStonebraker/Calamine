using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLoopCount : MonoBehaviour {

    public GameObject loop;
	
	// Update is called once per frame
	void Update () {
        GetComponent<UnityEngine.UI.Text>().text = System.Convert.ToString(loop.GetComponent<LoopingTool>().loopCount);
	}
}
