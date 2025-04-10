using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHolderPanel : MonoBehaviour
{

    [SerializeField] private TargetItemPanel _targetItemForm;

    [SerializeField] private LevelSO _levels;

    [SerializeField] private List<TargetItemPanel> targetItems = new List<TargetItemPanel>();
    void Start()
    {
        int levelIndex = PlayerPrefs.GetInt(LevelSO.KEY_LEVEL, 1);

        foreach (var levelTarget in _levels.GetLevelData(levelIndex - 1).LevelTargetObjects)
        {
            TargetItemPanel target = Instantiate(_targetItemForm, transform.position, Quaternion.identity, transform);

            target.transform.localScale = _targetItemForm.transform.localScale;

            target.SetTargetItem(levelTarget.quantities, levelTarget.sprite, levelTarget.blockTypeName);
            
            target.gameObject.SetActive(true);

            targetItems.Add(target);
        }
    }

    public bool IsCompletedTarget()
    {

        foreach (var target in targetItems) {
            if(target.Quantities != 0)
            {
                return false;
            }
        }

        return true;

    }

    public void DecreaseQuantitiesTarget(int quantities, BlockTypeName blockTypeName)
    {
        foreach(var  target in targetItems)
        {
            if (target.BlockTypeName.Equals(blockTypeName))
            {
                target.DecreaseQuantities(quantities);
                return;
            }
        }
    }
}
