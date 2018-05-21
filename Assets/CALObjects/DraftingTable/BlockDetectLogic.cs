using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BlockDetectLogic : MonoBehaviour {

//                           __    __         
//| |  / /___ ______(_)___ _/ /_  / /__ _____
//| | / / __ `/ ___/ / __ `/ __ \/ / _ \/ ___/
//| |/ / /_/ / /  / / /_/ / /_/ / /  __(__  )
//|___/\__,_/_/  /_/\__,_/_.___/_/\___/____/  

    public GameObject collidingObject = null;
    public int jointBreakForce = 100;
    public int jointTorqueBreakForce = 100;
    private int totalObjects = 0;
    public float collidedOutlineWidth = 0.11f;


//    __ ___     __ __              __    
//   /  |/  /__  / /_/ /_ ____  ____/ /____
//  / /|_/ / _ \/ __/ __ \/ __ \/ __  / ___/
// / /  / /  __/ /_/ / / / /_/ / /_/ (__  ) 
///_/  /_/\___/\__/_/ /_/\____/\__,_/____/  

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
    }

    private void snapObjectToSelf(ref Collider other)
    {
        try
        {
            setCollidingObject(other);
            snap();

            joinObject();
        }
        catch
        {
            collidingObject = null;
        }
    }

    private void modifySingleObjectShader(GameObject theObj)
    {
        Renderer collidingObjRend;

        collidingObjRend = theObj.GetComponent<MeshRenderer>();
        collidingObjRend.material.shader = Shader.Find("Outlined/ModelProjectionDetailed");
        collidingObjRend.material.SetFloat("_Outline", collidedOutlineWidth);
    }

    private void modifyObjectShaders_Recurse(GameObject objectToModify)
    {
        /*Maybe we should check the type of object here*/

        /*Apply the shader to the object that actually hit the board*/
        if (objectToModify.GetComponent<MeshRenderer>() != null)
            modifySingleObjectShader(objectToModify);

        /*If the GameObject that hit the board has children, apply the shader to them too*/
        for (int i = 0; i < objectToModify.transform.childCount; i++)
        {
            GameObject subObj = objectToModify.transform.GetChild(i).gameObject;
            if (objectToModify.GetComponent<MeshRenderer>() != null)
                modifySingleObjectShader(subObj);
            if (subObj.transform.childCount != 0)
                modifyObjectShaders_Recurse(subObj);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        snapObjectToSelf(ref other);
        //modifyObjectShaders_Recurse(other.gameObject);
        modifySingleObjectShader(other.gameObject);
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

    void Update()
    {

    }
}
