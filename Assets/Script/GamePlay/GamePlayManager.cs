using Cysharp.Threading.Tasks;
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

    [SerializeField] private FillBlockController _topField;


    [SerializeField] private CheckMatrixController _checkMatrixController;

    [SerializeField] private BoosterRecipeSO _bosterRecipeSO;

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
    public async UniTask OnBlockClicked(BlockController clickedFieldBlock)
    {

        List<BlockController> foundSimilarBlocksList = _checkMatrixController.CheckMatrix(clickedFieldBlock);


        bool isClickedBlockBooster = clickedFieldBlock.Type.Equals(BlockTypeName.Booster);

        if (isClickedBlockBooster) 
        {
            OnClickedBlockIsBooster(foundSimilarBlocksList);
        }
        else
        {
            OnClickedBlockIsNormal(foundSimilarBlocksList);
        }


       await _fieldManager.FillingBlock();

        //filling block for top field

        _topField.FillBlockIntoField();
    }

    //handle event if clicked block is booster
    private void OnClickedBlockIsBooster(List<BlockController> foundSimilarBlocksList)
    {
        BoosterBlockController booster =  foundSimilarBlocksList[0] as BoosterBlockController;

        if (booster == null) return;

        booster.OnBoosterActive(_fieldManager.GetFieldInfors());
        
    }

    //handle event if clicked block is normal
    private void OnClickedBlockIsNormal(List<BlockController> _foundSimilarBlocksList)
    {

        //check if quantities of similar adjencent blocks > 1
        bool isHavingAdjacentSimilarBlockType = _foundSimilarBlocksList.Count > 1;

        if (!isHavingAdjacentSimilarBlockType) return;

        //check if clicked block can be merged into booster
        NormalBlockController clickedBlock = _foundSimilarBlocksList[0] as NormalBlockController;
        BoosterRecipeSO.BoosterRecipe boosterRecipe = _bosterRecipeSO.GetRecipe(clickedBlock.BoosterName);

        if (boosterRecipe != null)
        {
            Vector2 matrixIndex = FieldDrawController.TransformFromPosToMatrixIndex(clickedBlock.transform.position);

            int colIndex = (int)matrixIndex.y;

            int rowIndex = (int)matrixIndex.x;

            BlockController booster = Instantiate(boosterRecipe.booster);

            _fieldManager.GetComponent<FillBlockController>().AddBlockDirectlyToMatrix(rowIndex, colIndex, booster);

            BlockPool.Instance.SendBlockBackToPool(clickedBlock);

            _foundSimilarBlocksList.Remove(clickedBlock);
        }


        foreach (BlockController fieldBlock in _foundSimilarBlocksList)
        {

            Vector2 matrixIndex = FieldDrawController.TransformFromPosToMatrixIndex(fieldBlock.transform.position);

            int targetColIndex = (int)matrixIndex.y;

            int targetRowIndex = (int)matrixIndex.x;

           
            _fieldManager.GetFieldInfors().Field[targetRowIndex, targetColIndex] = null;

            BlockPool.Instance.SendBlockBackToPool(fieldBlock);

        }

        

    }
}
