using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoosterData", menuName = "ScriptableObject/Booster/BoosterData")]
public class BoosterSOData : ScriptableObject
{
   

    [System.Serializable]
    public class BoosterData
    {
        public Sprite icon;
        public BlockTypeName typeBlock;
        public BoosterName boosterName;
    }

    [SerializeField] private BoosterRecipeSO _recipe;

    [SerializeField] private List<BoosterData> _boosters;

    public BoosterData GetBooster( int quantitiesSimilarBlock, BlockTypeName typeBlock)
    {
        foreach ( BoosterData booster in _boosters)
        {
           
            BoosterRecipeSO.BoosterRecipe recipe = _recipe.GetRecipe(booster.boosterName.ToString());
            Debug.Log("check booster recipe : " + recipe);
            Debug.Log("check quantities similar block : " + quantitiesSimilarBlock + " , check type block : " + typeBlock);
            if(booster.typeBlock.Equals(typeBlock) && 
               recipe.minRequiredBlock <= quantitiesSimilarBlock &&
               recipe.maxRequiredBlock >= quantitiesSimilarBlock)
            {
                return booster;
            }
        }


        Debug.Log("no booster recipe found");
        return null;
    }
}
