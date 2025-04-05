using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    [SerializeField] private CheckMatrixController _checkMatrixController;

    [SerializeField] private BoosterSOData _boosterData;

    private static BoosterManager _instance;

    public static BoosterManager Instance
    {
        get { return _instance;}

        private set { _instance = value; }
    }

    private void Awake()
    {
        if (_instance == null) 
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


    public void HandleBoosterInMatrix(FieldDrawController _fieldHandle)
    {
        
        //matrix to mark assessed block
        bool[,] flag = new bool[_fieldHandle.NumRows, _fieldHandle.NumCols];
        for (int i = 0; i < _fieldHandle.NumRows; i++) 
        {
            for (int j = 0; j < _fieldHandle.NumCols; j++) 
            {

                if (flag[i, j] || _fieldHandle.Field[i, j] == null) continue;

                //get all similar adjacent blocks with clicked block
                List<BlockController> similarBlocksList = _checkMatrixController.CheckMatrix(_fieldHandle.Field[i,j]);

                int quantitiesSimilarBlocks = similarBlocksList.Count;
                BlockTypeName blockType = similarBlocksList[0].Type;

                if (blockType.Equals(BlockTypeName.Booster)) continue;

                //handle booster
                BoosterSOData.BoosterData booster = _boosterData.GetBooster(quantitiesSimilarBlocks, blockType);

                Debug.Log("check booster : " + booster);

                foreach (BlockController block in similarBlocksList)
                {
                    NormalBlockController normalBlock = block as NormalBlockController;

                    normalBlock.CheckIsAbleMergedBooster(booster);

                    Vector2 matrixIndex = FieldDrawController.TransformFromPosToMatrixIndex(normalBlock.transform.position);

                    int rowIndex = (int)matrixIndex.x;
                    int colIndex = (int)matrixIndex.y;

                    flag[rowIndex, colIndex] = true;
                }
            }
        }


    }


}
