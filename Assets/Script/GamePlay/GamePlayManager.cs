using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private static GamePlayManager _instance;
    public static GamePlayManager Instance
    {
        get { return _instance; }
    }


    [SerializeField] private FieldManager _fieldManager;

    [SerializeField] private BlockPool _blockPool;

    //queue to save adjacent blocks need to checked 
    private Queue<BlockController> _detectSimilarBlockQueue = new Queue<BlockController>();

    //list to save all same type block found
    private List<BlockController> _foundSimilarBlocksList = new List<BlockController>();

    

    private void Start()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            if(_instance != gameObject)
            {
                Destroy(gameObject);
            }
        }
    }

    private void CheckAdjacentBlock(int colIndex, int rowIndex, BlockController target)
    {

        bool isOverComeMatrix = colIndex >= _fieldManager.GetFieldInfors().NumCols 
            || rowIndex >= _fieldManager.GetFieldInfors().NumRows
            || colIndex < 0 || rowIndex < 0;

        if (isOverComeMatrix)
        {
            return;
        }

        BlockController checkFieldBlock = _fieldManager.GetFieldInfors().Field[rowIndex, colIndex];

        if (target.IsEqual(checkFieldBlock))
        {

            if (!_foundSimilarBlocksList.Contains(checkFieldBlock))
            {
                _detectSimilarBlockQueue.Enqueue(checkFieldBlock);

                _foundSimilarBlocksList.Add(checkFieldBlock);

            }
        }
    }

    //handle field block clicked event 
    public void OnBlockClicked(BlockController clickedFieldBlock)
    {

        _detectSimilarBlockQueue.Enqueue(clickedFieldBlock);

        _foundSimilarBlocksList.Add(clickedFieldBlock);


        //check adjacent blocks 
        while (_detectSimilarBlockQueue.Count > 0)
        {
            BlockController fieldBlock = _detectSimilarBlockQueue.Dequeue();

            Vector2 matrixIndex = FieldDrawController.TransformFromPosToMatrixIndex(fieldBlock.transform.position);

            int colIndex = (int)matrixIndex.y;

            int rowIndex = (int)matrixIndex.x; 

            CheckAdjacentBlock(colIndex + 1, rowIndex, fieldBlock);

            CheckAdjacentBlock(colIndex, rowIndex + 1, fieldBlock);

            CheckAdjacentBlock(colIndex - 1, rowIndex, fieldBlock);

            CheckAdjacentBlock(colIndex, rowIndex - 1, fieldBlock);
        }



        DisableAllAdjacentSimilarBlock();

        //reset list, queue
        _detectSimilarBlockQueue.Clear();
        _foundSimilarBlocksList.Clear();
    }

    private void DisableAllAdjacentSimilarBlock()
    {
        bool isHavingAdjacentSimilarBlockType = _foundSimilarBlocksList.Count > 1;

        if (!isHavingAdjacentSimilarBlockType) return;

        Dictionary<int, int> colsLackBlocks = new Dictionary<int, int>();

        foreach (BlockController fieldBlock in _foundSimilarBlocksList)
        {

            Vector2 matrixIndex = FieldDrawController.TransformFromPosToMatrixIndex(fieldBlock.transform.position);

            int targetColIndex = (int)matrixIndex.y;

            int targetRowIndex = (int)matrixIndex.x;

            if (colsLackBlocks.ContainsKey(targetColIndex))
            {
                colsLackBlocks[targetColIndex]++;
            }
            else
            {
                colsLackBlocks.Add(targetColIndex, 1);
            }

            _blockPool.SendBlockBackToPool(fieldBlock);

        }

        _fieldManager.FillingBlock(colsLackBlocks);

    }


}
