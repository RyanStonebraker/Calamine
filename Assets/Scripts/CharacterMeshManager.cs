﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeshManager : MonoBehaviour {

    public Mesh characterMesh;
    public GameObject moveFromDrawing;

    private void Start()
    {
        moveFromDrawing = GameObject.Find("MoveFromDraw");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            characterMesh = other.GetComponent<MeshFilter>().mesh;
            Debug.Log("Inside Character if - characterMeshManager: " + characterMesh.name);
            moveFromDrawing.GetComponent<MeshFilter>().mesh = characterMesh;
        }
    }
}