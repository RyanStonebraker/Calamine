using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeshManager : MonoBehaviour {

    public Mesh characterMesh;
    public Renderer characterRenderer;
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

            if (other.GetComponent<Renderer>())
            {
                characterRenderer = other.GetComponent<Renderer>();
                Debug.Log("Inside Character if - characterShader: " + characterRenderer.name);
                moveFromDrawing.GetComponent<Renderer>().material = characterRenderer.material;
            }
        }
    }
}
