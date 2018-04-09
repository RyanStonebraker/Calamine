using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingScreenController : MonoBehaviour {
	public string boardName = "Board";
	public string triggerNode = "FunctionBlock";

	public GameObject Screen;
	private GameObject DynamicScreen;
	public float distanceFromBoard = 0.5f;
	public float rollingSpeed = 0.1f;
	private GameObject Board;

	public int startHeight = 100;
	public bool isVisible = false;
	public bool toggleStage = false;

	void Start () {
		Board = GameObject.Find(boardName);
		GameObject [] tools = GameObject.FindGameObjectsWithTag("Tool");
		foreach (GameObject tool in tools) {
			if (tool.name.Contains("DrawingCanvas")) {
				DynamicScreen = tool;
				return;
			}
		}
		InstantiateDisplay();
	}

	void Update () {
		if (!isVisible && toggleStage) {
			RollDownDisplay();
		}
		else if (isVisible && toggleStage) {
			RollUpDisplay();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.Contains(triggerNode)) {
			toggleStage = true;
		}
	}

	void RollDownDisplay () {
		if (!DynamicScreen)
			return;

		if (DynamicScreen.transform.position.y > Board.transform.position.y) {
			Vector3 updatedPosition = new Vector3(DynamicScreen.transform.position.x, 
												DynamicScreen.transform.position.y - rollingSpeed, 
												DynamicScreen.transform.position.z);
			DynamicScreen.transform.position = updatedPosition;
		}
		else {
			isVisible = true;
			toggleStage = false;
		}
	}

	void RollUpDisplay () {
		if (!DynamicScreen)
			return;

			if (DynamicScreen.transform.position.y < startHeight) {
				Vector3 updatedPosition = new Vector3(DynamicScreen.transform.position.x, 
													DynamicScreen.transform.position.y + rollingSpeed, 
													DynamicScreen.transform.position.z);
				DynamicScreen.transform.position = updatedPosition;
			}
			else {
				// Destroy(DynamicScreen);
				// DynamicScreen = null;
				isVisible = false;
				toggleStage = false;
			}
	}

	void InstantiateDisplay () {
		Vector3 shiftedPosition = new Vector3(Board.transform.position.x, 
											Board.transform.position.y + startHeight, 
											Board.transform.position.z - distanceFromBoard);
		DynamicScreen = Instantiate(Screen, shiftedPosition, Board.transform.rotation) as GameObject;
	}
}
