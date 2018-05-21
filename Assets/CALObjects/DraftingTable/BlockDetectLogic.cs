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
    /*In the script from the old board, these values were 100*/
    public int jointBreakForce = 100000;
    public int jointTorqueBreakForce = 100000;
    public float collidedOutlineWidth = 0.02f;
	public float objScaleFactor = 0.5f;
	private Material outlineMaterial;
	private int objectsOnBoard = 0;


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
        //collidingObjRend.material.shader = Shader.Find("Outlined/ModelProjectionDetailed");
		//collidingObjRend.material.SetFloat("_Outline", collidedOutlineWidth);
		collidingObjRend.material = outlineMaterial;
		//collidingObjRend.material.SetFloat ("_Outline", collidedOutlineWidth);
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

    public void modifyTransformAndRotation_PHIL(GameObject objectToModify)
    {
		/*Spin the object and make it dizzy*/

        //Vector3 parallelVec = gameObject.transform.position - objectToModify.transform.position;
        //objectToModify.transform.rotation = Quaternion.FromToRotation(objectToModify.transform.up, parallelVec) * objectToModify.transform.localRotation;
		Quaternion parallelQuat = Quaternion.Euler(64.0f, 0.0f, 0.0f);
		objectToModify.transform.rotation = parallelQuat;

		/*Scale the object uniformly*/
		objectToModify.transform.localScale *= 0.8f;

		/*Shove the object onto the table*/
		Vector3 newPos = objectToModify.transform.position;
		newPos.y -= 0.3f;
		objectToModify.transform.position = newPos;
    }

	public void modifyTransformAndRotation_FOX(GameObject objectToModify)
	{
		/*Spin the object and make it dizzy*/

		//Vector3 parallelVec = gameObject.transform.position - objectToModify.transform.position;
		//objectToModify.transform.rotation = Quaternion.FromToRotation(objectToModify.transform.up, parallelVec) * objectToModify.transform.localRotation;
		Quaternion parallelQuat = Quaternion.Euler(-28.03f,-0.57f, 89.0120f);
		objectToModify.transform.rotation = parallelQuat;


		/*Squash the object*/
		//Vector3 newScale = objectToModify.transform.localScale;
		//newScale.z = 0.1f;
		//objectToModify.transform.localScale = newScale;

		/*Scale the object uniformly*/
		objectToModify.transform.localScale *= 0.8f;

		/*Shove the object onto the table*/
		Vector3 newPos = objectToModify.transform.position;
		newPos.y -= 0.6f;
		objectToModify.transform.position = newPos;
	}

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().isKinematic = true;
        //snapObjectToSelf(ref other);
        //modifyObjectShaders_Recurse(other.gameObject);
		modifySingleObjectShader(other.gameObject);

		Vector3 indentTransform = new Vector3(0.0f,0.0f, 0.0f);

		if (other.gameObject.name.IndexOf ("Fox") != -1) {
			indentTransform.x = (-1.0f + -1.0f * (float)objectsOnBoard);
			indentTransform.y = 0.3f;
			indentTransform.z += 0.0f;
			other.gameObject.transform.position += indentTransform;
			modifyTransformAndRotation_FOX (other.gameObject);
		} else {
			indentTransform.x = (0.1f + 0.95f * (float)objectsOnBoard);
			indentTransform.y = -0.1f;
			indentTransform.z += 0.2f;
			other.gameObject.transform.position += indentTransform;
			modifyTransformAndRotation_PHIL (other.gameObject);
		}

		objectsOnBoard++;


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

	void Awake()
	{
		outlineMaterial = Resources.Load("OutlineMat", typeof(Material)) as Material;
	}

    void Update()
    {

    }
}
