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

    public async UniTask FillingBlock()
    {
        //List<UniTask> fillColTasks = new List<UniTask>();

        //foreach (KeyValuePair<int, int> colLackBlock in colsLackBlocks)
        //{
        //    int colIndex = colLackBlock.Key;
        //    int missingBlocks = colLackBlock.Value;

        //    fillColTasks.Add(_fillingBlock.FillBlockIntoField(colIndex, missingBlocks));
        //}

        //await UniTask.WhenAll(fillColTasks);


        _fillingBlock.FillBlockIntoField();
       
        _boosterManager.HandleBoosterInMatrix(_fieldDraw);
    }

    public FieldDrawController GetFieldInfors()
    {
        return _fieldDraw;
    }
}
