using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private FillBlockController _fillingBlock;

    [SerializeField] private FieldDrawController _fieldDraw;

    [SerializeField] private BoosterManager _boosterManager;

    private void Start()
    {
        _boosterManager.HandleBoosterInMatrix(_fieldDraw);
    }

    public async UniTask FillingBlock(Dictionary<int, int> colsLackBlocks)
    {
        List<UniTask> fillColTasks = new List<UniTask>();

        foreach (KeyValuePair<int, int> colLackBlock in colsLackBlocks)
        {
            int colIndex = colLackBlock.Key;
            int missingBlocks = colLackBlock.Value;

            fillColTasks.Add(_fillingBlock.FillBlockIntoField(colIndex, missingBlocks));
        }

        await UniTask.WhenAll(fillColTasks);

        _boosterManager.HandleBoosterInMatrix(_fieldDraw);

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
