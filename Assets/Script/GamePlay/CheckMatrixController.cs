using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMatrixController : MonoBehaviour
{
    [SerializeField] private FieldManager _fieldManager;

    //queue to save adjacent similar blocks need to checked 
    private Queue<BlockController> _detectSimilarBlockQueue = new Queue<BlockController>();

    public void CheckAdjacentBlock(int colIndex, int rowIndex, BlockController target, 
        List<BlockController> foundSimilarBlocksList)
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

            if (!foundSimilarBlocksList.Contains(checkFieldBlock))
            {
                _detectSimilarBlockQueue.Enqueue(checkFieldBlock);

                foundSimilarBlocksList.Add(checkFieldBlock);

            }
        }
    }
    
    //check matrix from specified block and return list of similar and adjacent block
    public List<BlockController> CheckMatrix(BlockController clickedFieldBlock)
    {
        List<BlockController> foundSimilarBlocksList = new List<BlockController>();

        _detectSimilarBlockQueue.Enqueue(clickedFieldBlock);

        foundSimilarBlocksList.Add(clickedFieldBlock);

        List<BlockController> result = new List<BlockController>();


        //check adjacent blocks 
        while (_detectSimilarBlockQueue.Count > 0)
        {
            BlockController fieldBlock = _detectSimilarBlockQueue.Dequeue();

            Vector2 matrixIndex = FieldDrawController.TransformFromPosToMatrixIndex(fieldBlock.transform.position);

            int colIndex = (int)matrixIndex.y;

            int rowIndex = (int)matrixIndex.x;

            CheckAdjacentBlock(colIndex + 1, rowIndex, fieldBlock, foundSimilarBlocksList);

            CheckAdjacentBlock(colIndex, rowIndex + 1, fieldBlock, foundSimilarBlocksList);

            CheckAdjacentBlock(colIndex - 1, rowIndex, fieldBlock, foundSimilarBlocksList);

            CheckAdjacentBlock(colIndex, rowIndex - 1, fieldBlock, foundSimilarBlocksList);
        }

        //reset list, queue
        _detectSimilarBlockQueue.Clear();
        
        return foundSimilarBlocksList;

    }


}
