using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level/Level")]
public class LevelSO : ScriptableObject
{

    public static readonly string KEY_LEVEL = "LEVEL";

    [SerializeField] private List<LevelDataSO> levels = new List<LevelDataSO>();

    public LevelDataSO GetLevelData(int levelIndex)
    {
        if(levelIndex >= levels.Count)
        {
            levelIndex = 0;
        }

        return levels[levelIndex];  
    }
}
