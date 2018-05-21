using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAbstracts;

public class AmphitheatreBlockController : MonoBehaviour {

  public GameObject sourceBlock;
  private GameObject blockIcon;
  public float scaleFactor = 0.8f;
  private GameObject block;

  public Vector3 shiftCenter;

  public bool infiniteSpawn = true;

  private Transform startParent;

  void loadPrefabBlock () {
      Vector3 center = GetComponent<Renderer>().bounds.center + shiftCenter;
      block = Instantiate(blockIcon, center, sourceBlock.transform.rotation) as GameObject;
      block.transform.parent = gameObject.transform;
      block.AddComponent<UnusedDelete>();
      Bounds thisBound = GetComponent<Renderer>().bounds;
      Bounds iconBound = block.GetComponent<Collider>().bounds;

      Vector3 currSize = thisBound.max - thisBound.min;
      Vector3 icoSize = iconBound.max - iconBound.min;
      block.GetComponent<Transform>().localScale = block.transform.localScale * (Mathf.Max(currSize.x, currSize.y) / (Mathf.Max(icoSize.x, icoSize.y)));
      block.GetComponent<Transform>().localScale = new Vector3(block.transform.localScale.x * scaleFactor, block.transform.localScale.y * scaleFactor, block.transform.localScale.z * scaleFactor);
      block.GetComponent<Rigidbody>().isKinematic = true;
      block.GetComponent<Rigidbody>().useGravity = false;
    }
  void Start () {
    startParent = gameObject.transform.parent;
    blockIcon = Instantiate (sourceBlock, new Vector3(-100,-100,-100), new Quaternion(0,0,0,0));
    blockIcon.transform.parent = gameObject.transform.parent;
    blockIcon.transform.localScale = new Vector3(blockIcon.transform.localScale.x * scaleFactor, blockIcon.transform.localScale.y * scaleFactor, blockIcon.transform.localScale.z * scaleFactor);
    loadPrefabBlock();
  }

  void OnTriggerEnter(Collider other) {
    if (other.gameObject.name.Contains("Controller")) {
      block.GetComponent<Rigidbody>().isKinematic = false;
      block.GetComponent<Rigidbody>().useGravity = true;
    }
  }

  void exitSpinOut() {
    startParent.parent.GetComponent<Animator>().enabled = true;
    Destroy(startParent.parent.gameObject, 0.9f);
  }
  
  void OnTriggerExit(Collider other) {
        if (block && other.gameObject.GetInstanceID() == block.GetInstanceID())
        {
            if (gameObject.transform.parent == startParent) {
              Vector3 childPos = block.transform.position;
              block.transform.SetParent(startParent.parent);
            }
            other.transform.localScale = sourceBlock.transform.localScale;
            if (infiniteSpawn)
              loadPrefabBlock();
            else {
              Invoke("exitSpinOut", 1.5f);
              block = null;
            }
        }
  }
}
