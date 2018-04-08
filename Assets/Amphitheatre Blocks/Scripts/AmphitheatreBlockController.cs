using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAbstracts;

public class AmphitheatreBlockController : MonoBehaviour {

  public GameObject sourceBlock;

  private GameObject blockIcon;
  public float scaleFactor = 0.8f;

  private GameObject block;

  private string currentName;

  private double lastSpawnTime;

  void loadPrefabBlock () {
      blockIcon = Instantiate (sourceBlock, new Vector3(0,0,0), new Quaternion(0,0,0,0));
      lastSpawnTime = Time.time;
      Vector3 center = GetComponent<Renderer>().bounds.center;
      Vector3 scaledShape = new Vector3(blockIcon.transform.localScale.x * scaleFactor, blockIcon.transform.localScale.y * scaleFactor, blockIcon.transform.localScale.z * scaleFactor);
      blockIcon.transform.localScale = scaledShape;
      block = Instantiate(blockIcon, center, sourceBlock.transform.rotation) as GameObject;
      Bounds thisBound = GetComponent<Renderer>().bounds;
      Bounds iconBound = block.GetComponent<Collider>().bounds;

      Vector3 currSize = thisBound.max - thisBound.min;
      Vector3 icoSize = iconBound.max - iconBound.min;

      block.transform.localScale = block.transform.localScale *  Mathf.Max(currSize.x, currSize.y) / (Mathf.Max(icoSize.x, icoSize.y));
      block.transform.localScale = new Vector3(block.transform.localScale.x * scaleFactor, block.transform.localScale.y * scaleFactor, block.transform.localScale.z * scaleFactor);
      currentName = block.name;
  }
  void Start () {
    loadPrefabBlock();
  }

  void OnTriggerExit(Collider other) {
    other.transform.localScale = sourceBlock.transform.localScale;
    if (other.gameObject.name == currentName && Time.time - lastSpawnTime > 0.5)
      loadPrefabBlock();
  }
}
