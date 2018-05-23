using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopBodyInteraction : MonoBehaviour {

    public Text bodyText;
    public Material occupiedBodyMat;
    private bool lerp = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Statement")
        {
            bodyText.text = other.name;
            lerp = true;
            gameObject.GetComponent<Renderer>().material = occupiedBodyMat;
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if(lerp)
            bodyText.color = Color.Lerp(Color.blue, Color.yellow, Mathf.PingPong(Time.time, 1));
    }
}
