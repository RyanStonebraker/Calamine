using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnusedDelete : MonoBehaviour {
	public int destroyDelay = 5;
	public bool deleteTriggered = false;
    private int parentID = 0;

	void OnTriggerEnter(Collider other) {
        if (parentID == 0 && (other.gameObject.name.Contains("Controller")))
        {
            parentID = other.GetInstanceID();
        }
        if (deleteTriggered && (other.gameObject.name.Contains("Controller") || other.gameObject.name.Contains("Node"))) {
			CancelInvoke("MaybeDestroy");
			deleteTriggered = false;
            parentID = other.GetInstanceID();
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.GetInstanceID() == parentID) {
			deleteTriggered = true;
			Invoke("MaybeDestroy", destroyDelay);
            parentID = 0;
		}
	}

	void MaybeDestroy() {
		Destroy(gameObject);
	}
}
