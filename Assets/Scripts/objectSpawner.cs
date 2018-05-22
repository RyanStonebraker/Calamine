﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpawner : MonoBehaviour {
	public GameObject spawnObject;
	public float spawnObjectDim = 1;
	public float spawnHeight = 20f;
	public int spawnCount = 20;
	public GameObject playArea;

	public List<GameObject> spawnedObjects;

	public void Start() {
		if (!playArea) {
			playArea = GameObject.FindGameObjectWithTag("PlayArea");
		}
	}

	private void spawnObjects() {
		Vector3 playAreaCenter = playArea.transform.position;
		for (var i = 0; i < spawnCount; ++i) {
			var xInc = (i % 4) * spawnObjectDim;
			var zInc = Mathf.Floor(i / 5) * spawnObjectDim;
			spawnedObjects.Add(Instantiate(spawnObject, new Vector3(playAreaCenter.x + xInc, playAreaCenter.y + spawnHeight, playAreaCenter.z + zInc), new Quaternion()));
		}
	}

	private void OnTriggerExit(Collider other) {
		spawnObjects();
	}
}
