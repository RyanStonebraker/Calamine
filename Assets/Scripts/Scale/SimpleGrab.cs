using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGrab : MonoBehaviour {

    public Transform objectToSpawn;

    // Reference to object being tracked (for example, the vive controllers)
    private SteamVR_TrackedObject trackedObj;

    public GameObject collidingObject;

    /*************************************************************************************/
    public GameObject objectInHand; //NOTE: USE THIS LATER FOR OBJECT EDITS IN DEV ENV
    public Vector3 initialGrabPosition = Vector3.positiveInfinity;
    /*************************************************************************************/

    // Device property used to link the controller (via index) and return the controller's input
    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    //when the script is loaded, get the tracked object (a vive controller) from the component list
    //and assign it to trackedObj (the first variable created above)
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    public GameObject getObjectInHand()
    {
        return objectInHand;
    }

    // Collision detection - on function call, if the collider is within a grabable object, assign 
    // that object to a reference variable (collidingObject) for later use
    private void setCollidingObject(Collider col)
    {
        //if controller is already assigned an objectInHand or the game object
        //isn't a rigidbody then don't update the collidingObject
        if (collidingObject || !col.GetComponent<Rigidbody>())
            return;

        //assign colliding object as a potential grab target
            collidingObject = col.gameObject;
    }

    // Make it possible to grab objects from other controller by
    // reading collision data from other controller collider
    public void OnTriggerEnter(Collider other)
    {
        if (other.name != "Spawn Area")
            setCollidingObject(other);
    }

    // Fixes a bug where if the trigger is held on a collidable game object
    // it has a chance to fail a valid collision
    public void OnTriggerStay(Collider other)
    {
        if (other.name != "Spawn Area")
            setCollidingObject(other);
    }

    // When the collider exits an ungrabbed, grabbable game object, clear the
    // collidingObject game object reference
    public void OnTriggerExit(Collider other)
    {
        // don't spam null assignment
        if (!collidingObject)
            return;

        if (other.name != "Spawn Area")
            collidingObject = null;
    }

    private void grabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;

        // Connect the controller to the collided object via a joint
        // Note: object must be a rigidbody for joint to work
        var joint = addFixedJoint(); // (see function addFixedJoint below for physics properties)
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // Add some physical features to joint, if the breakforces are
    // exceeded, the joint is broken and the joined objects are decoupled
    private FixedJoint addFixedJoint()
    {
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.breakForce = uint.MaxValue; //NEVER BREAK HOPEFULLY
        joint.breakTorque = uint.MaxValue;
        return joint;
    }

    //release the object and transfer the velocity and angular velocity to the
    //object right before it's release for realistic effect.
    private void releaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            //release the object from the joint then destroy the joint
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            //transfer velocities
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        //truly release the object
        objectInHand = null;
    }

    /** SCALE CHANGER **/

    public void adjustScale(float value)
    {
#warning Tristan uncommented this line in
        objectInHand.transform.localScale += new Vector3(value, value, value);
    }

    private void recordInitialGrabPosition()
    {
        initialGrabPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //objectInHand.transform.position = transform.position;
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
                grabObject();

            if (initialGrabPosition == Vector3.positiveInfinity)
                recordInitialGrabPosition();

            //adjustScale(Controller.GetAxis().y);
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
                releaseObject();

            if (initialGrabPosition != Vector3.positiveInfinity)
                initialGrabPosition = Vector3.positiveInfinity;

        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip) || Controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Instantiate(objectToSpawn, gameObject.transform.position, Quaternion.identity);
        }
    }
}