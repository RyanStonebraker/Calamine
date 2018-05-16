using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAbstracts;

public class AmphitheatreBlockController : MonoBehaviour {

  public GameObject sourceBlock;
  private GameObject blockIcon;
  public float scaleFactor = 0.8f;
  private GameObject block;

  void loadPrefabBlock () {
      Vector3 center = GetComponent<Renderer>().bounds.center;
      block = Instantiate(blockIcon, center, sourceBlock.transform.rotation) as GameObject;
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
    blockIcon = Instantiate (sourceBlock, new Vector3(10,10,10), new Quaternion(0,0,0,0));
    blockIcon.transform.localScale = new Vector3(blockIcon.transform.localScale.x * scaleFactor, blockIcon.transform.localScale.y * scaleFactor, blockIcon.transform.localScale.z * scaleFactor);
    loadPrefabBlock();
  }

  void OnTriggerEnter(Collider other) {
    if (other.gameObject.name.Contains("Controller")) {
      block.GetComponent<Rigidbody>().isKinematic = false;
      block.GetComponent<Rigidbody>().useGravity = true;
    }
  }
  void OnTriggerExit(Collider other) {
        if (other.gameObject.GetInstanceID() == block.GetInstanceID())
        {
            // NOTE: These may or may not be needed for actual VR
            // block.GetComponent<Rigidbody>().isKinematic = false;
            // block.GetComponent<Rigidbody>().useGravity = true;
            other.transform.localScale = sourceBlock.transform.localScale;
            loadPrefabBlock();
        }
  }
}
