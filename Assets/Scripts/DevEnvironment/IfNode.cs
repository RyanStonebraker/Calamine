using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IfNode : MonoBehaviour {

    public float xShiftOnCollide = 0.5f;
    public GameObject collidingObject = null;
    public int jointBreakForce = 100;
    public int jointTorqueBreakForce = 100;
    public string outputFilePath = "Assets/output.txt";

    private void setCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
            return;

        collidingObject = col.gameObject;
    }
    
    private void joinObject()
    {
        // Connect the node to the collided object via a joint
        // Note: object must be a rigidbody for joint to work
        var joint = addWeakFixedJoint(); // (see function addFixedJoint below for physics properties)
        joint.connectedBody = collidingObject.GetComponent<Rigidbody>();
        Debug.Log("Joint created");
    }

    private FixedJoint addWeakFixedJoint()
    {
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.breakForce = jointBreakForce;
        joint.breakTorque = jointTorqueBreakForce;
        return joint;
    }

    public void snap()
    {
        //transform.collidingObject.translate(0,0,0);
        Vector3 shift = new Vector3(xShiftOnCollide, 0, 0);
        collidingObject.transform.position = gameObject.transform.position + shift;
        Debug.Log("Position Snap Executed");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Impacted");
        try
        {
            setCollidingObject(other);
            snap();

            joinObject();
            writeToOutputFile(gameObject.name + " Jones Soda makes Bone Hurting Juice");
        }
        catch
        {
            Debug.Log("Bag impact, no rigidbody?");
            collidingObject = null;
        }
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Staying");
        //setPhysics(true, false); WIP
    }

    private void killJoint()
    {
        GetComponent<FixedJoint>().connectedBody = null;
        Destroy(GetComponent<FixedJoint>());
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        //setPhysics(false, true); WIP
        collidingObject = null;
        killJoint();
        Debug.Log("Joint Destroyed");
    }

    private void setPhysics(bool gravity, bool kinematic)
    {
        collidingObject.GetComponent<Rigidbody>().isKinematic = kinematic;
        collidingObject.GetComponent<Rigidbody>().useGravity = gravity;
    }

    //FILE IO

    void writeToOutputFile(string data)
    {
        try
        {
            StreamWriter outFile = new StreamWriter(outputFilePath, true); //true = append, false = overwrite
            outFile.WriteLine(data);
            outFile.Close();
            Debug.Log("File Write Good");
        }
        catch
        {
            Debug.Log("File Write Failed");
        }
    }

    void Update()
    {

    }
}
