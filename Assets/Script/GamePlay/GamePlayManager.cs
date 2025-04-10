using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        if (GameEventManager.Instance.IsComplete || GameEventManager.Instance.IsFailed) {
            return;
        }

        BoosterManager.Instance.CurrentBoostersActive.Clear();

        List<BlockController> foundSimilarBlocksList = _checkMatrixController.CheckMatrix(clickedFieldBlock);


        bool isClickedBlockBooster = clickedFieldBlock.Type.Equals(BlockTypeName.Booster);

        if (isClickedBlockBooster) 
        {
            BoosterManager.Instance.CurrentBoostersActive.Add(OnClickedBlockIsBooster(clickedFieldBlock as BoosterBlockController));

            //List<UniTask> finishedBooster = new List<UniTask>();


            //foreach (UniTask boosterActive in BoosterManager.Instance.CurrentBoostersActive)
            //{
            //    finishedBooster.Add(FillingMatrixAfterBoosterAcitve(boosterActive));
            //}
            await UniTask.WhenAll(BoosterManager.Instance.CurrentBoostersActive);
        }
        else
        {
            OnClickedBlockIsNormal(foundSimilarBlocksList);

            
        }

        await _fieldManager.FillingBlock();

        //filling block for top field
        _topField.FillBlockIntoField();
    }

    private async UniTask FillingMatrixAfterBoosterAcitve(UniTask boosterActive)
    {
        await boosterActive;
        await _fieldManager.FillingBlock();
    }

    //handle event if clicked block is booster
    private async UniTask OnClickedBlockIsBooster(BoosterBlockController booster)
    {

        if (booster == null) return;

        GameEventManager.Instance.DecreaseMove();

        await booster.OnBoosterActive(_fieldManager.GetFieldInfors());
        
    }

    //handle event if clicked block is normal
    private void OnClickedBlockIsNormal(List<BlockController> _foundSimilarBlocksList)
    {

        //check if quantities of similar adjencent blocks > 1
        int quantitiesBlockDestroy = _foundSimilarBlocksList.Count;

        bool isHavingAdjacentSimilarBlockType = quantitiesBlockDestroy > 1;

        if (!isHavingAdjacentSimilarBlockType)
        {
            _foundSimilarBlocksList[0].transform.DOShakePosition(0.5f, 0.1f);
            return;
        }

        GameEventManager.Instance.DecreaseMove();

        BlockTypeName clickedBlockType = _foundSimilarBlocksList[0].Type;

        //check if clicked block can be merged into booster
        NormalBlockController clickedBlock = _foundSimilarBlocksList[0] as NormalBlockController;
        BoosterRecipeSO.BoosterRecipe boosterRecipe = _bosterRecipeSO.GetRecipe(clickedBlock.BoosterName);

        if (boosterRecipe != null)
        {
            Vector2 matrixIndex = FieldDrawController.TransformFromPosToMatrixIndex(clickedBlock.transform.position);

            int colIndex = (int)matrixIndex.y;

            int rowIndex = (int)matrixIndex.x;

            BoosterBlockController booster = Instantiate(boosterRecipe.booster);

            CanBoosterEnableColor(booster, clickedBlock.Type);

            CanBoosterChangeRotation(booster, _foundSimilarBlocksList);

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


    private void CanBoosterEnableColor(BoosterBlockController booster, BlockTypeName blockType)
    {
        if (booster is IEnableColor boosterColor)
        {

            boosterColor.ChangeColor(blockType);
            
        }
    }

    private void CanBoosterChangeRotation(BoosterBlockController booster, List<BlockController> similarBlocksList)
    {
        float angle = 90;

        if(booster is IEnableChangeRotation boosterRotation)
        {
            if (boosterRotation.IsEnableRotation(similarBlocksList))
            {
                boosterRotation.ChangeRotation(angle);
            }
            
        }
    }
}
