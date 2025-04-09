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
           
            if(booster.typeBlock.Equals(typeBlock) && 
               recipe.minRequiredBlock <= quantitiesSimilarBlock &&
               recipe.maxRequiredBlock >= quantitiesSimilarBlock)
            {
                return booster;
            }
        }


        return null;
    }

  
}
