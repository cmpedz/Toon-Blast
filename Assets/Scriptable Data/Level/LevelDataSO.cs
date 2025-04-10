using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObject/Level/LevelData")]
public class LevelDataSO : ScriptableObject
{
    [System.Serializable]
    public class LevelTargetObject
    {
        public Sprite sprite;
        public int quantities;
        public BlockTypeName blockTypeName;

    }

    [SerializeField] private List<LevelTargetObject> _levelTargetObjects = new List<LevelTargetObject>();

    public List<LevelTargetObject> LevelTargetObjects
    {
        get { return _levelTargetObjects; }
    }

    public int quantitiesMove;
}
