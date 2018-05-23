using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountLoopIgnoreCollide : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Statement")
            return;
    }
}
