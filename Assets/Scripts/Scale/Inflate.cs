using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inflate : MonoBehaviour {

    public float inflateRate = 0.01f;
    public float maxSize = 3.0f;

    private bool isMaxSize(Collision collision)
    {
        float maxMagnitude = (float)System.Math.Sqrt(3.0f * maxSize * maxSize);
        return collision.gameObject.transform.localScale.magnitude >= maxMagnitude;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Tool" && !isMaxSize(collision))
            collision.gameObject.transform.localScale += new Vector3(inflateRate, inflateRate, inflateRate);
    }

}
