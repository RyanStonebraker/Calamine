using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TrisDemoBoardNode : MonoBehaviour {

    public GameObject collidingObject = null;
    public int jointBreakForce = 100;
    public int jointTorqueBreakForce = 100;
    public string outputFilePath = "Assets/output.txt";
    List<GameObject> subNodes = new List<GameObject>();
    private int frameCountTimer = 0;
    private bool timerIsRunning = false;
    bool amITaken = false;
    private double timeConnected;
    private GameObject templateNode;

    public static bool isFirstObject = true;

    void Awake()
    {
        if (isFirstObject) {
            isFirstObject = false;
            templateNode = Instantiate(this.gameObject, new Vector3(this.transform.position.x + 12, this.transform.position.y + 12, this.transform.position.z), this.transform.rotation);
        }
    }

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
        timerIsRunning = true;
        timeConnected = Time.time;
        Debug.Log("Joint created");
    }

    private FixedJoint addWeakFixedJoint()
    {
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.breakForce = jointBreakForce; 
        joint.breakTorque = jointTorqueBreakForce;
        return joint;
    }

    private void snap()
    {
        //transform.collidingObject.translate(0,0,0);
        collidingObject.transform.position = gameObject.transform.position;
        Debug.Log("Position Snap Executed");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Impacted");
        try
        {
            if (!amITaken) {
                setCollidingObject(other);
                snap();

                joinObject();
                writeToOutputFile(gameObject.name + " Jones Soda makes Bone Hurting Juice");
            }
        }
        catch
        {
            Debug.Log("Bag impact, no rigidbody?");
            collidingObject = null;
        }
        amITaken = true;
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

    private void createSubnode()
    {
        GameObject newSubNode = Instantiate(templateNode, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y + 0.85f, this.transform.position.z), this.transform.rotation);
        subNodes.Add(newSubNode);
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
        if (timerIsRunning && ((Time.time - timeConnected) > 2.0)) {
            timerIsRunning = false;
            createSubnode();
            Debug.Log("Execution within if statement");
            collidingObject = null;
            killJoint();

        }
    }
}
