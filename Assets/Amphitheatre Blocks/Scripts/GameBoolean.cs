using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAbstracts;


// LHS, Compare Operator, RHS
public class GameBoolean : GameBlock
{
  public GameObject LeftHandSide;
  public GameObject RightHandSide;

  /* >, <, ==, !=, Collides with, Is above, Is below, etc. */
  public string interaction;

  public override string FlushCurrentDataBlock()
  {
    return "[2==2] THEN\n";
  }
}
