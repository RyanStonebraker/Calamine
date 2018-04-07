using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbstracts {
  public class BlockVector
  {
      //// Swap loc in BlockVector by identifiers
      //public void Swap (int id1, int id2)
      //{
      //    GameBlock temp;
      //    int first_id = (id1 <= id2) ? id1 : id2;
      //    int second_id = (id2 > id1) ? id2 : id1;

      //    foreach (GameBlock block in _gblocks)
      //    {
      //        if (block.identifier == first_id)
      //            temp = block;
      //        else if (block.identifier == second_id))
      //    }
      //}

      public void AddBlock (GameBlock block) {
        _gblocks.Add(block);
      }

      public void RemoveBlock (int index) {
        _gblocks.RemoveAt(index);
      }

      public int Length
      {
          get { return _gblocks.Count; }
      }

      public GameBlock this[int num]
      {
        get { return _gblocks[num]; }
        set { _gblocks[num] = value; }
      }

      private List<GameBlock> _gblocks;
  }
}
