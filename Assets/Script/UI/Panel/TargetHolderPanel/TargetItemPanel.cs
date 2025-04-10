using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetItemPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _quantitiesDisplay;

    [SerializeField] private Image _spriteDisplay;

    [SerializeField] private int _quantities;

    [SerializeField] private BlockTypeName _blockTypeName;
    public BlockTypeName BlockTypeName
    {
        get { return _blockTypeName; }
    }

    public int Quantities
    {
        get { return _quantities; }
    }
    
    public void SetTargetItem(int quantities, Sprite sprite, BlockTypeName blockTypeName)
    {
        _quantitiesDisplay.text = quantities.ToString();
        _spriteDisplay.sprite = sprite;
        _quantities = quantities;
        _blockTypeName = blockTypeName;
    }

    public void DecreaseQuantities(int quantities)
    {
        _quantities -= quantities;
        if (_quantities < 0) {
            _quantities = 0;

        }

        _quantitiesDisplay.text = _quantities.ToString();
    }
}
