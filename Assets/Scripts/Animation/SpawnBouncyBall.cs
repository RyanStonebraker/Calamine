using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBouncyBall : MonoBehaviour {

    public GameObject bouncyBall;
    public Vector3 displacementFromFox = new Vector3(5f, 20f, 0f);
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(bouncyBall, transform.position += displacementFromFox, new Quaternion());
    }
}
