using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;

public class FieldManager : MonoBehaviour, IFieldManager
{
    [SerializeField] private FillBlockController _fillingBlock;

    [SerializeField] private FieldDrawController _fieldDraw;

    public void FillingBlock(Dictionary<int, int> colsLackBlocks)
    {
        foreach (KeyValuePair<int, int> colLackBlock in colsLackBlocks)
        {
            int colIndex = colLackBlock.Key;
            int missingBlocks = colLackBlock.Value;

            _fillingBlock.FillBlockIntoField(colIndex, missingBlocks);
        }

        //for (int rowIndex = _fieldDraw.NumRows - 1; rowIndex >= 0; rowIndex--)
        //{
        //    for(int colIndex = 0; colIndex < _fieldDraw.NumCols; colIndex++)
        //    {
        //        Debug.Log("check block in " + rowIndex + " , " + colIndex + " is " + _fieldDraw.Field[rowIndex, colIndex] +
        //            " is active : " + !_fieldDraw.Field[rowIndex, colIndex].gameObject.activeSelf);

        //        if (!_fieldDraw.Field[rowIndex, colIndex].gameObject.activeSelf)
        //        {
        //            Debug.Log("check block in " + rowIndex + " , " + colIndex + " is inactive");
        //            _fillingBlock.FillBlockIntoField(colIndex, 1);
        //        }
        //    }
        //}
    }

    public FieldDrawController GetFieldInfors()
    {
        return _fieldDraw;
    }
}
