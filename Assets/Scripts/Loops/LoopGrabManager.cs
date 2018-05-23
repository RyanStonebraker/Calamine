using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopGrabManager : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject + " collided with " + collision.gameObject.name);
        if (collision.gameObject.name.Contains("Controller"))
        {
            Rigidbody rigidBody = GetComponent<Rigidbody>();
            if (collision.gameObject.GetComponent<BasicControllerInput>().Controller.GetHairTriggerDown() && !rigidBody.useGravity)
            {
                rigidBody.useGravity = true;
                rigidBody.isKinematic = false;
                Debug.Log("Gravity set on loop");
            }
        }
    }

    private void Update()
    {
        
    }
}
