using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoIsTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " entered " + gameObject.name);
    }
}
