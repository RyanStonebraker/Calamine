using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockZ : MonoBehaviour {

    public bool controllerEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Controller"))
        {
            controllerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Controller"))
        {
            controllerEntered = false;
        }
    }
    
}
