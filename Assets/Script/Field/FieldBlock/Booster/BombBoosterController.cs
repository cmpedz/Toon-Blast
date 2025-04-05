using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBoosterController : BoosterBlockController
{
    public override void OnBoosterActive(FieldDrawController fieldDraw)
    {
        BlockController[,] field = fieldDraw.Field;
        Vector2 matrixIndex = FieldDrawController.TransformFromPosToMatrixIndex(transform.position);
        int colIndex = (int)matrixIndex.y;
        int rowIndex = (int)matrixIndex.x;

        field[rowIndex, colIndex] = null;



        for (int i = rowIndex-1; i<= rowIndex + 1 ; i++)
        {
            for(int j = colIndex - 1; j <= colIndex + 1; j++)
            {
                if (fieldDraw.IsOverComeMatrix(i, j) || field[i, j ] == null) continue;

                if (field[i, j].gameObject.activeInHierarchy)
                {
                    if (field[i, j].Type.Equals(BlockTypeName.Booster))
                    {
                        BoosterBlockController booster = field[i, j] as BoosterBlockController;

                        booster.OnBoosterActive(fieldDraw);

                    }
                    else
                    {
                        BlockPool.Instance.SendBlockBackToPool(field[i, j]);
                    }

                    field[i, j] = null;

                }
                
            }
        }

        this.OnDestroyEvent();
    }
}
