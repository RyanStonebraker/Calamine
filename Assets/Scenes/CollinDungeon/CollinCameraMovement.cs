using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollinCameraMovement : MonoBehaviour {

	public GameObject playerObj;
	private Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
		cameraOffset = transform.position - playerObj.transform.position;
	}

	void LateUpdate () {
		transform.position = playerObj.transform.position + cameraOffset;
	}
}
