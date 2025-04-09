using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnableChangeRotation 
{
   public void ChangeRotation(float angle);

   public bool IsEnableRotation(List<BlockController> similarBlocksList);
}
