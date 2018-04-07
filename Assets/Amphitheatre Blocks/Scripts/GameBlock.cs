using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: On collision with any other object, spawn a new one of the GameBlocks in amphitheatre

namespace GameAbstracts {
  public abstract class GameBlock : MonoBehaviour
  {
  // Default constructor, simply adds to instance count
      public GameBlock()
      {
          ++instanceCount;
          _childBlocks = new BlockVector();
      }

  // returns the instance count of the object. This should be used to determine what scope to add blocks in.
  public string Identifier {
    get { return gameObject.transform.name + instanceCount.ToString(); }
  }



  public void AddChild(GameBlock childBlock) {
    _childBlocks.AddBlock(childBlock);
  }

  public void RemoveChild(string child_id) {
    for (int i = 0; i < _childBlocks.Length; ++i) {
      if (_childBlocks[i].Identifier == child_id) {
        _childBlocks.RemoveBlock(i);
      }
    }
  }

  // turn the current block into a string representation, different for each block
  abstract public string FlushCurrentDataBlock();

  public string FlushAllBlocks()
  {
    _currentLevelCache = "";

    _currentLevelCache += StartLine + "\n";

    _currentLevelCache += this.FlushCurrentDataBlock();

    // Every object is attached to cube, so need to iterate through these for live copy?
    for (int i = 0; i < _childBlocks.Length; ++i) {
      _currentLevelCache += _childBlocks[i].FlushAllBlocks();
    }

    _currentLevelCache += EndLine + ";\n";

    return _currentLevelCache;
  }

private void OnTriggerEnter(Collider other)
{
  Instantiate(gameObject, this.StartPosition, this.StartRotation);
}
  // public GameObject
  public Quaternion StartRotation;
  public Vector3 StartPosition;

  // Keeps track of which object this is.
  static int instanceCount = 0;

  // Ex. BlockDef ... Begin NAME...
  public string StartLine;

  // Ex. End NAME
  public string EndLine;

  // children scope to pass off to
  public BlockVector _childBlocks;

  // cached string representation of block
  private string _currentLevelCache;
  }
}
