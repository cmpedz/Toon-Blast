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

    [SerializeField] private CheckMatrixController _checkMatrixController;

    

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

    

    //handle field block clicked event 
    public void OnBlockClicked(BlockController clickedFieldBlock)
    {

        List<BlockController> foundSimilarBlocksList = _checkMatrixController.CheckMatrix(clickedFieldBlock);

        DisableAllAdjacentSimilarBlock(foundSimilarBlocksList);

       
    }

    private void DisableAllAdjacentSimilarBlock(List<BlockController> _foundSimilarBlocksList)
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
