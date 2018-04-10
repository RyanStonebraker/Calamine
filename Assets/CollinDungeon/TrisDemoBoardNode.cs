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
    bool shouldIHaveABaby = true;
    private double timeConnected;
    private GameObject templateNode;

    public static int depthCounter = 1;
    public int myDepth;
    public string whatObjectsDoITake;

    public float indentDistance = 0.5f;
    public float scrollDistance = 0.55f;

    public static bool isFirstObject = true;

    void Awake()
    {
        collidingObject = null;

        if (isFirstObject) {
            isFirstObject = false;
            this.GetComponent<Renderer>().material.shader = Shader.Find("Custom/GreenPulse");
            templateNode = Instantiate(this.gameObject, new Vector3(this.transform.position.x + 12, this.transform.position.y + 12, this.transform.position.z), this.transform.rotation);
        }

        myDepth = depthCounter;
        switch(myDepth)
        {
            case 1:
                whatObjectsDoITake = "Tool";
                break;
            case 2:
                whatObjectsDoITake = "Character";
                break;
            case 3:
                whatObjectsDoITake = "FlowOfControl";
                break;
            default:
                whatObjectsDoITake = "Finish";
                break;
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
        if (other.gameObject.tag != whatObjectsDoITake)
            return;
        Debug.Log("Impacted");
        try
        {
            if (!amITaken && other.tag == whatObjectsDoITake) {
                setCollidingObject(other);
                snap();

                joinObject();
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
        //setPhysics(false, true); WIP
        //collidingObject = null;
        //killJoint();
    }

    private void setPhysics(bool gravity, bool kinematic)
    {
        collidingObject.GetComponent<Rigidbody>().isKinematic = kinematic;
        collidingObject.GetComponent<Rigidbody>().useGravity = gravity;
    }

    private void createSubnode()
    {
        depthCounter++;
        GameObject newSubNode = Instantiate(this.gameObject, new Vector3(this.transform.position.x + indentDistance, this.transform.position.y - scrollDistance, this.transform.position.z), this.transform.rotation);

        if (newSubNode.GetComponent<TrisDemoBoardNode>().whatObjectsDoITake == "Character")
        {
            newSubNode.AddComponent<CharacterMeshManager>();
            newSubNode.GetComponent<Renderer>().material.shader = Shader.Find("Custom/HighlightPulse");
        }
        else if (newSubNode.GetComponent<TrisDemoBoardNode>().whatObjectsDoITake != "Character" && newSubNode.GetComponent<CharacterMeshManager>())
        {
            Destroy(newSubNode.GetComponent<CharacterMeshManager>());
        }

        if (newSubNode.GetComponent<TrisDemoBoardNode>().whatObjectsDoITake == "FlowOfControl")
        {
            newSubNode.AddComponent<LoopingBlock>();
            newSubNode.GetComponent<Renderer>().material.shader = Shader.Find("Custom/YellowPulse");
        }
        else if (newSubNode.GetComponent<TrisDemoBoardNode>().whatObjectsDoITake != "FlowOfControl" && newSubNode.GetComponent<LoopingBlock>())
        {
            Destroy(newSubNode.GetComponent<LoopingBlock>());
        }

        newSubNode.GetComponent<Renderer>().material.color = Color.red;
        //newSubNode.GetComponent<Renderer>().material.shader = Shader.Find("Custom/PulseNode");
        //this.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        subNodes.Add(newSubNode);
    }

    //FILE IO

    void writeToOutputFile(string data)
    {
    }

    void Update()
    {
        if (amITaken && shouldIHaveABaby) {
            //timerIsRunning = false;
            //amITaken = false;
            shouldIHaveABaby = false;
            createSubnode();

        }
    }
}
