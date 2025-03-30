using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFieldManager
{
    public FieldDrawController GetFieldInfors();

    public void FillingBlock(Dictionary<int, int> colsLackBlocks);
}
