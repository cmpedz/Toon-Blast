using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBlockController : MonoBehaviour
{
    [SerializeField] private FieldDrawController _topField;

    [SerializeField] private BlockPool _blockPool;
    

    //using async method to optimize performance
    public BlockController GetBlockFromTopField(int rowIndex, int colIndex)
    {
        BlockController retrievedBlock = _topField.Field[rowIndex, colIndex];

        _topField.Field[rowIndex, colIndex] = null;

        //_topField.GetComponent<FillBlockController>().FillBlockIntoField(rowIndex, colIndex);


        return retrievedBlock;

    }

    public int GetTopFieldNumRows()
    {
        return _topField.NumRows;
    }

    public int GetTopFieldNumCols()
    {
        return _topField.NumCols;
    }
}
