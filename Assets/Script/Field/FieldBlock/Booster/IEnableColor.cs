using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnableColor 
{
   public BlockTypeName BlockType { get; set; }

   public void ChangeColor(BlockTypeName blockType);
}
