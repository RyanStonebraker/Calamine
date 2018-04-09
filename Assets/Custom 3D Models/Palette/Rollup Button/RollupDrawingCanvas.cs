using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollupDrawingCanvas : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.Contains("Controller")) {
            GameObject [] tools = GameObject.FindGameObjectsWithTag("Tool");
			foreach (GameObject tool in tools) {
				if (tool.name.Contains("ArtPalette")) {
					tool.GetComponent<DrawingScreenController>().toggleStage = true;
				}
			}
		}
	}

}
