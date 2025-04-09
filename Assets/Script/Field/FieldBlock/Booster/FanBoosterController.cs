using Cysharp.Threading.Tasks;
using POPBlocks.Scripts.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBoosterController : BoosterBlockController, IEnableColor
{
    [SerializeField] private int _maxBlocksDestroy;
    [SerializeField] private Lightning _lightEffect;
    [SerializeField] private FanSpriteController _fanSpritesController;

    private BlockTypeName _blockTypeName;

    public BlockTypeName BlockType { get => _blockTypeName; set => _blockTypeName = value; }


    private void CreateLightningEffect(Transform pos1, Transform pos2)
    {
        Lightning lightEffect = Instantiate(_lightEffect, Vector2.zero, Quaternion.identity);
        lightEffect.SetLight(pos1.position, pos2.position);
    }
    public async override UniTask OnBoosterActive(FieldDrawController fieldDraw)
    {
        BlockController[,] field = fieldDraw.Field;
        Vector2 matrixIndex = FieldDrawController.TransformFromPosToMatrixIndex(transform.position);
        int colIndex = (int)matrixIndex.y;
        int rowIndex = (int)matrixIndex.x;

        field[rowIndex, colIndex] = null;

        for (int i = 0; i < fieldDraw.NumRows; i++) {

            if (_maxBlocksDestroy == 0) break;
            for (int j = 0; j < fieldDraw.NumCols; j++)
            {
                if (_maxBlocksDestroy == 0) break;

                if (field[i, j] != null)
                { 
                    if (field[i, j].Type.Equals(_blockTypeName))
                    {
                        Debug.Log("check block destroyed by fan : " + field[i, j].Type);
                        CreateLightningEffect(transform, field[i, j].transform);

                        BlockPool.Instance.SendBlockBackToPool(field[i, j]);
                        field[i, j] = null;
                        _maxBlocksDestroy --;
                    }

                }
            }
        }

        await UniTask.Delay(300);
        this.OnDestroyEvent();

    }

    public void ChangeColor(BlockTypeName blockType)
    {
        _blockTypeName = blockType;
        _fanSpritesController.SetColor(blockType);
    }
}
