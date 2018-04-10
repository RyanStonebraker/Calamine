using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnusedDelete : MonoBehaviour {
	public int destroyDelay = 5;
	public bool deleteTriggered = false;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.Contains("Controller") && deleteTriggered) {
			CancelInvoke("MaybeDestroy");
			deleteTriggered = false;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.name.Contains("Controller")) {
			deleteTriggered = true;
			Invoke("MaybeDestroy", destroyDelay);
		}
	}

	void MaybeDestroy() {
		Destroy(gameObject);
	}
}
