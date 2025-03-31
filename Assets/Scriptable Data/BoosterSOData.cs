using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoosterData", menuName = "ScriptableObject/Booster/BoosterData")]
public class BoosterSOData : ScriptableObject
{
   

    [System.Serializable]
    class BoosterData
    {
        public Sprite icon;
        public int typeBlock;
        public BoosterName boosterName;
    }

    [SerializeField] private BoosterRecipeSO _recipe;

    [SerializeField] private List<BoosterData> _boosters;

    public Sprite GetBooster( int quantitiesSimilarBlock, int typeBlock)
    {
        foreach ( BoosterData booster in _boosters)
        {
            BoosterRecipeSO.BoosterRecipe recipe = _recipe.GetRecipe(booster.boosterName);

            if(booster.typeBlock == typeBlock && 
               recipe.minRequiredBlock <= quantitiesSimilarBlock &&
               recipe.maxRequiredBlock >= quantitiesSimilarBlock)
            {
                return booster.icon;
            }
        }

        return null;
    }
}
