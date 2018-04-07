using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAbstracts;

public class AmphitheatreBlockController : MonoBehaviour {

  public GameObject blockIcon;
  void Start () {
    // If a GameObject is attached, turn off prefab block
    if (blockIcon)
            GetComponent<Renderer>().enabled = false;

    GameObject block = Instantiate(blockIcon, transform.position, transform.rotation) as GameObject;
    Bounds thisBound = GetComponent<MeshRenderer>().bounds;
    Bounds iconBound = block.GetComponent<MeshRenderer>().bounds;

    Vector3 currSize = thisBound.max - thisBound.min;
    Vector3 icoSize = iconBound.max - iconBound.min;

    block.transform.localScale = block.transform.localScale * (Mathf.Max(currSize.x, currSize.y) / Mathf.Max(icoSize.x, icoSize.y));
	
	// Fix wrong location generation
    blockIcon.GetComponent<GameBlock>().StartPosition = gameObject.transform.position;
    blockIcon.GetComponent<GameBlock>().StartRotation = gameObject.transform.rotation;
  }
}
