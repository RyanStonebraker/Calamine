using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalLink : MonoBehaviour {

    public GameObject collidingObject;
    public int jointBreakForce = 1000;
    public int jointTorqueBreakForce = 1000;

    public void joinObject(Collider collision)
    {
        collision.gameObject.transform.parent = gameObject.transform;
        var joint = addFixedJoint();
        joint.connectedBody = collidingObject.GetComponent<Rigidbody>();
        Debug.Log("Joint Created with " + collidingObject.gameObject);
    }

    private FixedJoint addFixedJoint()
    {
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.breakForce = jointBreakForce;
        joint.breakTorque = jointTorqueBreakForce;
        return joint;
    }

    private void setCollidingObject(Collider col)
    {
        if (collidingObject || !col.gameObject.GetComponent<Rigidbody>())
            return;

        collidingObject = col.gameObject;
    }

    private void killJoint()
    {
        GetComponent<FixedJoint>().connectedBody = null;
        Destroy(GetComponent<FixedJoint>());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.name.Contains("Controller"))
        {
            setCollidingObject(collision);
            joinObject(collision);
        }
    }

    //private void OnTriggerStay(Collider collision)
    //{
    //    if (!collision.name.Contains("Controller"))
    //    {
    //        joinObject(collision);
    //        setCollidingObject(collision);
    //    }
    //}

    private void OnTriggerExit(Collider collision)
    {
        if (!collision.name.Contains("Controller"))
            killJoint();
    }
}
