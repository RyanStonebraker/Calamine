using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAbstracts;

public class If_Controller : GameBlock {
  public void Start () {
    StartLine = "IF ";
    EndLine = "END IF;";
  }

	public override string FlushCurrentDataBlock() {
    return conditional.FlushCurrentDataBlock();
    // IF GameBoolean (have a to string representation/FlushCurrentDataBlock)
	}

  public GameBoolean conditional;
}
