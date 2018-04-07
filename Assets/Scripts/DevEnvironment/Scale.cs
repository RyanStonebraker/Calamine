using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour {

    public GameObject rightController = GameObject.Find("Controller (right)");
    public GameObject leftController = GameObject.Find("Controller (left)");

    public GameObject rightObject = null;
    public GameObject leftObject = null;

    private float getFastestXVelocity()
    {
        return System.Math.Max(-leftController.GetComponent<Rigidbody>().velocity.x,
                                rightController.GetComponent<Rigidbody>().velocity.x);
    }

    private float getFastestYVelocity()
    {
        return System.Math.Max(-leftController.GetComponent<Rigidbody>().velocity.y,
                                rightController.GetComponent<Rigidbody>().velocity.y);
    }

    private Vector3 calculateScale()
    {
        float velocityMax = System.Math.Max(getFastestXVelocity(), getFastestYVelocity());
        return new Vector3(velocityMax, velocityMax, 0f);
    }

    private void dynamicallyScale()
    {
        gameObject.transform.localScale = calculateScale();
    }

    void Update ()
    {
        if (rightController == null)
            rightController = GameObject.Find("Controller (right)");
        else if (leftController == null)
            leftController = GameObject.Find("Controller (left)");

        rightObject = rightController.GetComponent<SimpleGrab>().objectInHand;
        leftObject = leftController.GetComponent<SimpleGrab>().objectInHand;

        if(rightObject == leftObject)
            dynamicallyScale();
        
    }
}


/*
 public GameObject leftController = null;
    public GameObject rightController = null;

    void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "Controller")
        {
            Debug.Log("Impact " + collider.gameObject.name + " to Scale List");

            if (collider.gameObject.name == "Controller (left)")
                leftController = collider.gameObject;
            else
                rightController = collider.gameObject;
        }
    }

    void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.tag == "Controller")
        {
            Debug.Log("Exited " + collider.gameObject.name + " to Scale List");

            if (collider.gameObject.name == "Controller (left)")
                leftController = null;
            else
                rightController = null;
        }
    }

    private float getFastestXVelocity()
    {
        return System.Math.Max( -leftController.GetComponent<Rigidbody>().velocity.x,
                                rightController.GetComponent<Rigidbody>().velocity.x );
    }

    private float getFastestYVelocity()
    {
        return System.Math.Max( -leftController.GetComponent<Rigidbody>().velocity.y,
                                rightController.GetComponent<Rigidbody>().velocity.y );
    }

    private Vector3 calculateScale()
    {
        float velocityMax = System.Math.Max(getFastestXVelocity(), getFastestYVelocity());
        return new Vector3(velocityMax, velocityMax, 0f);
    }

    private void dynamicallyScale()
    {
        gameObject.transform.localScale = calculateScale();
    }

	void Update ()
    {
        if (leftController && rightController)
            dynamicallyScale();
    }
    */
