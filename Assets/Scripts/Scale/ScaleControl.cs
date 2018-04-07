using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleControl : MonoBehaviour {

    private GameObject leftController;
    private GameObject rightController;
    private GameObject objectInBothControllers;
    public float scaleFactor = 0.05f;
    public bool uniformScale = true;

    private void Start()
    {
        try
        {
            leftController = GameObject.Find("Controller (left)");
            rightController = GameObject.Find("Controller (right)");
        }
        catch
        {
            Debug.Log("Failed to find controllers... Scale script init error.. Retrying...");
            Start();
        }
    }

    private bool controllersAreBeingTracked()
    {
        if (!leftController || !rightController)
            Start();

        return leftController && rightController;
    }

    //needed! or else infinite loop when program starts since objectInHand is initialized to null for both controllers and is therefore "the same object"
    private bool theObjectIsNotNull() 
    {
        return leftController.GetComponent<SimpleGrab>().objectInHand != null && rightController.GetComponent<SimpleGrab>().objectInHand != null;
    }

    private bool controllersAreGrabbingSameObject()
    {
        return (leftController.GetComponent<SimpleGrab>().objectInHand == rightController.GetComponent<SimpleGrab>().objectInHand) && theObjectIsNotNull();
    }

    private void setObjectInHands()
    {
        if (leftController)
            objectInBothControllers = leftController.GetComponent<SimpleGrab>().objectInHand;
        else
            //redundancy set just in case left is unbound during this if block execution
            objectInBothControllers = rightController.GetComponent<SimpleGrab>().objectInHand;  

        if (!objectInBothControllers)
        {
            Debug.Log("setObjectInHands() did not bind object held in controller to objectInBothControllers GameObject variable. Retrying...");
            setObjectInHands();
        }
    }

    private void scaleObject()
    {
        Vector3 leftControllerVelocity = leftController.GetComponent<SimpleGrab>().Controller.velocity;
        Vector3 rightControllerVelocity = rightController.GetComponent<SimpleGrab>().Controller.velocity;

        Debug.Log("Left Controller Velocity: " + leftControllerVelocity + " Right Controller Velocity: " + rightControllerVelocity);
        Debug.Log("Scaling " + objectInBothControllers + " by " + objectInBothControllers.transform.localScale + " + " + (-rightControllerVelocity + leftControllerVelocity) * scaleFactor);

        rightControllerVelocity.x *= -1;
        Vector3 newScale;
        if (uniformScale)
        {
            float rightVelocityScalar = (-rightControllerVelocity.x + -rightControllerVelocity.y + -rightControllerVelocity.z)/3;
            float leftVelocityScalar = (leftControllerVelocity.x + leftControllerVelocity.y + leftControllerVelocity.z)/3;
            float uniformScalar = rightVelocityScalar + leftVelocityScalar;
            newScale = objectInBothControllers.transform.localScale += new Vector3(uniformScalar,uniformScalar,uniformScalar) * scaleFactor;
            Debug.Log("New Scale: " + newScale);
        }
        else
        {
            newScale = objectInBothControllers.transform.localScale += (-rightControllerVelocity + leftControllerVelocity) * scaleFactor;
            Debug.Log("New Scale: " + newScale);
        }
    }

    // Update is called once per frame
    void Update () {
        if (controllersAreBeingTracked() && controllersAreGrabbingSameObject())
        {
            if(!objectInBothControllers)
                setObjectInHands();

            Debug.Log("Both controllers are have " + objectInBothControllers + " as their object in hand");
            scaleObject();
        }
        else if(objectInBothControllers)
        {
            objectInBothControllers = null;
        }
	}
}
