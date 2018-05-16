using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAbstracts;

public class DemoBoardNodeController : MonoBehaviour {

  public GameObject sourceBlock;
  private GameObject blockIcon;
  public float scaleFactor = 0.8f;
  private GameObject block;
  private string currentName;
  private double lastSpawnTime;

  void loadPrefabBlock () {
        try
        {
            lastSpawnTime = Time.time;
      Vector3 center = GetComponent<Renderer>().bounds.center;
      block = Instantiate(blockIcon, center, sourceBlock.transform.rotation) as GameObject;
      Bounds thisBound = GetComponent<Renderer>().bounds;
      Bounds iconBound = block.GetComponent<Collider>().bounds;

      Vector3 currSize = thisBound.max - thisBound.min;
      Vector3 icoSize = iconBound.max - iconBound.min;
            Debug.Log("Local Scale is: " + block.transform.localScale + " for " + block.name + " with multiplyer of: " + (Mathf.Max(currSize.x, currSize.y) / (Mathf.Max(icoSize.x, icoSize.y))));
            block.GetComponent<Transform>().localScale = block.transform.localScale * (Mathf.Max(currSize.x, currSize.y) / (Mathf.Max(icoSize.x, icoSize.y)));

      block.GetComponent<Transform>().localScale = new Vector3(block.transform.localScale.x * scaleFactor, block.transform.localScale.y * scaleFactor, block.transform.localScale.z * scaleFactor);
      currentName = block.name;
        }
        catch
        {

        }
    }
  void Start () {
    blockIcon = Instantiate (sourceBlock, new Vector3(10,10,10), new Quaternion(0,0,0,0));
    blockIcon.transform.localScale = new Vector3(blockIcon.transform.localScale.x * scaleFactor, blockIcon.transform.localScale.y * scaleFactor, blockIcon.transform.localScale.z * scaleFactor);
    loadPrefabBlock();
  }

  void OnTriggerExit(Collider other) {
    // other.transform.localScale = sourceBlock.transform.localScale;
        if (other.gameObject.name == currentName && Time.time - lastSpawnTime > 0.5)
        {
            other.transform.localScale = sourceBlock.transform.localScale;
            loadPrefabBlock();
        }
  }
}
