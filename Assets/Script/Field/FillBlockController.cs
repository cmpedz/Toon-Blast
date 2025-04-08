using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FillBlockController : MonoBehaviour
{
    [SerializeField] private float _speedFallDown;

    [SerializeField] private FieldDrawController _fieldDraw;

    [SerializeField] private BlockPool _blockPool;

    [SerializeField] private SummonBlockController _summonBlockController;
    private DG.Tweening.Tween AddBlockFromTopPos(int rowIndex, int colIndex, BlockController block)
    {
        Debug.Log("fill block !");

        if (block == null) return null;

        Vector2 posInMatrix = _fieldDraw.TransformFromMatrixIndexToPos(rowIndex, colIndex);

        _fieldDraw.Field[rowIndex, colIndex] = block;

        float height = block.transform.position.y - posInMatrix.y;

        return block.transform.DOMove(posInMatrix, _speedFallDown * height)
            .SetEase(Ease.InQuad);
 
    }

    public void AddBlockDirectlyToMatrix(int rowIndex, int colIndex, BlockController block)
    {
        Vector2 posInMatrix = _fieldDraw.TransformFromMatrixIndexToPos(rowIndex, colIndex);

        block.gameObject.SetActive(true);

        block.transform.position = posInMatrix;

        _fieldDraw.Field[rowIndex, colIndex] = block;
    }

    private DG.Tweening.Tween PushTopBlockDown(int rowIndex, int colIndex)
    {
        BlockController[,] field = _fieldDraw.Field;
        //push top block down to fill the field
        int j = rowIndex;

        while (field[j, colIndex] == null && j >= 0)
        {
            Debug.Log("check 3");
            j--;
            if (j < 0) break;
        }

        if (j >= 0)
        {
            BlockController tmp = field[j, colIndex];
            field[j, colIndex] = null;
            return AddBlockFromTopPos(rowIndex, colIndex, tmp);
            
        }
        else
        {

            //fill blanket by adding new block
            // get pos need to fill block in matrix
            int needFilledColIndex = colIndex;
            int needFilledRowIndex = rowIndex;

            BlockController blockFill = null;

            //if having top field matrix
            if (_summonBlockController != null)
            {
                //get retrieved block pos from top field
                int retrievedColIndex = colIndex;
                int retrievedRowIndex = _summonBlockController.GetTopFieldNumRows() - 1;
                Debug.Log("check retrieved matrix index : " + retrievedRowIndex + " , " + retrievedColIndex);

                //search block from top field
                do
                {
                    blockFill = _summonBlockController.GetBlockFromTopField(retrievedRowIndex, retrievedColIndex);
                    retrievedRowIndex--;
                    if(retrievedRowIndex < 0) break;
                } while (blockFill == null);

                return AddBlockFromTopPos(needFilledRowIndex, needFilledColIndex, blockFill);

            }
            else
            {

                Debug.Log("adding new block directly into matrix");
                Vector2 posFilled = _fieldDraw.TransformFromMatrixIndexToPos(needFilledRowIndex, needFilledColIndex);
                blockFill = _blockPool.RetrieveBlockFromPool(posFilled);

                AddBlockDirectlyToMatrix(needFilledRowIndex, needFilledColIndex, blockFill);

                return null;
            }


            



        }
    }

   
    public async UniTask FillBlockIntoField()
    {
        DG.Tweening.Tween lastBlockFallDown = null;

        for (int rowIndex = _fieldDraw.NumRows - 1; rowIndex >= 0; rowIndex--)
        {
            for (int colIndex = 0; colIndex < _fieldDraw.NumCols; colIndex++)
            {

                if (_fieldDraw.Field[rowIndex, colIndex] == null)
                {
                    lastBlockFallDown = PushTopBlockDown(rowIndex, colIndex);
                }
            }
        }

        if (lastBlockFallDown != null) {
            Debug.Log("check last block fall down : " + lastBlockFallDown);
            await lastBlockFallDown.AsyncWaitForCompletion();
        }

        

    }
}
