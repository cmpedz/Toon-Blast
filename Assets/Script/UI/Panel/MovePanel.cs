using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovePanel : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _moveDisplay;

    [SerializeField] private int _quantitiesMove;

    [SerializeField] private LevelSO _levels;

    void Start()
    {
        int levelIndex = PlayerPrefs.GetInt(LevelSO.KEY_LEVEL, 1);
        _quantitiesMove = _levels.GetLevelData(levelIndex - 1).quantitiesMove;
        SetMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SetMove()
    {
        _moveDisplay.text = _quantitiesMove.ToString();
    }

    public bool IsOutOfMove()
    {
        return _quantitiesMove == 0;
    }
    public void DecreaseMove()
    {
        int quantitiesDecrease = 1;
        _quantitiesMove -= quantitiesDecrease;
        if(_quantitiesMove < 0)
        {
            _quantitiesMove = 0;
        }

        SetMove();
    }
}
