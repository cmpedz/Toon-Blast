using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBlockController : MonoBehaviour
{
    [SerializeField] private FieldDrawController _topField;
    

    public BlockController GetBlockFromTopField(int rowIndex, int colIndex)
    {
        BlockController retrievedBlock = _topField.Field[rowIndex, colIndex];

        retrievedBlock.gameObject.SetActive(false);

        _topField.GetComponent<FillBlockController>().FillBlockIntoField(colIndex, 1);

        retrievedBlock.gameObject.SetActive(true);

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
