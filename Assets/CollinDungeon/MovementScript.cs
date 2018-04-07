using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

	public float speed;
	private Rigidbody myRigidBody;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 moveVec = new Vector3(moveHorizontal, 0.0f, moveVertical);

		myRigidBody.AddForce(moveVec * speed);
	}
}
