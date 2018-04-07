using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflate : MonoBehaviour {

    public float deflateRate = 0.01f;
    public float minSize = 0.1f;

    private bool isMinSize(Collision collision)
    {
        float maxMagnitude = (float)System.Math.Sqrt(3.0f * minSize * minSize);
        return collision.gameObject.transform.localScale.magnitude <= maxMagnitude;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag != "Tool" && !isMinSize(collision))
            collision.gameObject.transform.localScale -= new Vector3(deflateRate, deflateRate, deflateRate);
    }
}
