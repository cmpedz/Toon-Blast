using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoosterName
{
    Rocket,
    Bomb,
    Fan
}

[CreateAssetMenu(fileName = "BoosterRecipe", menuName = "ScriptableObject/Booster/BoosterRecipe")]
public class BoosterRecipeSO : ScriptableObject
{
    [System.Serializable]
    public class BoosterRecipe
    {
        public BoosterName boosterName;
        public int minRequiredBlock;
        public int maxRequiredBlock;
    }

    [SerializeField] private List<BoosterRecipe> _recipe;

    public BoosterRecipe GetRecipe(BoosterName name)
    {
        foreach (BoosterRecipe recipe in _recipe)
        {
            if(recipe.boosterName == name)
            {
                return recipe;
            }
        }

        return null;
    }
}
