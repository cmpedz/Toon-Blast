using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FillBlockController : MonoBehaviour
{
    [SerializeField] private float _speedFallDown;

    [SerializeField] private FieldDrawController _fieldDraw;

    [SerializeField] private BlockPool _blockPool;

    [SerializeField] private SummonBlockController _summonBlockController;
    private void AddBlock(int rowIndex, int colIndex, BlockController block)
    {
        Debug.Log("fill block !");

        Vector2 posInMatrix = _fieldDraw.TransformFromMatrixIndexToPos(rowIndex, colIndex);

        _fieldDraw.Field[rowIndex, colIndex] = block;

        float height = block.transform.position.y - posInMatrix.y;

        block.transform.DOMove(posInMatrix, _speedFallDown * height);
 
    }

    public void FillBlockIntoField(int colIndex, int quantitiesMissBlock)
    {

        Debug.Log("start fill block process in col : " + colIndex + "with quantities miss blocks : " + quantitiesMissBlock);

        BlockController[,] field = _fieldDraw.Field;
      
        //push top block down to fill the field
        for (int rowIndex = _fieldDraw.NumRows - 1; rowIndex >= 0 ;rowIndex--)
        {
          
            if (!field[rowIndex, colIndex].gameObject.activeSelf)
            {
                int j = rowIndex; 
                Debug.Log("check 2" + rowIndex + " " + colIndex);
                while (!field[j, colIndex].gameObject.activeSelf && j >= 0)
                {
                    Debug.Log("check 3");
                    j--;
                    if (j < 0) break;
                }

                if(j >= 0)
                {
                   
                    Debug.Log("detect top block to fill : " + rowIndex + " " + colIndex);
                    BlockController tmp = field[rowIndex, colIndex];
                    AddBlock(rowIndex, colIndex, field[j, colIndex]);
                    field[j, colIndex] = tmp;



                }
            }
        }

        Debug.Log("quantities missing block : " + quantitiesMissBlock + " in " + colIndex);

        //bug in here
        //fill blanket by adding new block
        for (int i = 0; i < quantitiesMissBlock ; i++)
        {
            // get pos need to fill block in matrix
            int needFilledColIndex = colIndex;
            int needFilledRowIndex = quantitiesMissBlock - 1 - i;

            BlockController blockFill = null;

            // if having top field matrix
            if (_summonBlockController != null)
            {
                //get retrieved block pos from top field
                int retrievedColIndex = colIndex;
                int retrievedRowIndex = _summonBlockController.GetTopFieldNumRows() - 1;
                Debug.Log("check retrieved matrix index : " + retrievedRowIndex + " , " + retrievedColIndex);

                blockFill = _summonBlockController.GetBlockFromTopField(retrievedRowIndex, retrievedColIndex);


            }
            else
            {
                Debug.Log("adding new block directly into matrix");
                Vector2 posFilled = _fieldDraw.TransformFromMatrixIndexToPos(needFilledRowIndex, needFilledColIndex);
                blockFill = _blockPool.GetRandomBlock(posFilled);
                blockFill.gameObject.SetActive(true);
            }

            AddBlock(needFilledRowIndex, needFilledColIndex, blockFill);
        }

    }
}
