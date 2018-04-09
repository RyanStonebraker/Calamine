using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnusedDelete : MonoBehaviour {
	public int destroyDelay = 5;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.Contains("Controller")) {
			CancelInvoke("MaybeDestroy");
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.name.Contains("Controller")) {
			Invoke("MaybeDestroy", destroyDelay);
		}
	}

	void MaybeDestroy() {
		Destroy(gameObject);
	}
}
