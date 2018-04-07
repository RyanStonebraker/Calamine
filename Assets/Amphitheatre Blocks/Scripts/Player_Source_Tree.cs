using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAbstracts;

public class Player_Source_Tree : GameBlock {
  
  public Player_Source_Tree () {
    StartLine = "BLOCKDEF(MAIN)\nBEGIN MAIN_IMPLEMENTATION";
    EndLine = "END MAIN_IMPLEMENTATION;";
  }

  public override string FlushCurrentDataBlock()
  {
    return "";
  }

}
