using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    private static GameEventManager _instance;
    public static GameEventManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] private ScoreBarPanel _scoreBarPanel;
    [SerializeField] private MovePanel _movePanel;

    [SerializeField] private TargetHolderPanel _targetHolderPanel;

    [SerializeField] private CompleteMenuPanel _completeMenuPanel;

    [SerializeField] private GameObject _loseMenuPanel;

    private bool _isComplete;

    private bool _isFailed;

    public bool IsComplete
    {
        get { return _isComplete; }
    }

    public bool IsFailed
    {
        get { return _isFailed; }
    }

    private void Awake()
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

    public void OnEscapeGame()
    {
        LifePanel.DecreaseLife();
    }

    public void DecreaseQuantitiesTarget(int quantities, BlockTypeName blockTypeName)
    {
        _targetHolderPanel.DecreaseQuantitiesTarget(quantities, blockTypeName);


        if (!_isComplete && !_isFailed) 
        {
            if (_targetHolderPanel.IsCompletedTarget())
            {
                StartCoroutine(OnCompleteStage());

            }

            if (_movePanel.IsOutOfMove())
            {
                StartCoroutine(OnFailedStage());
            }

        }
       
    }

    public IEnumerator OnCompleteStage()
    {
        _isComplete = true;

        yield return new WaitForSeconds(3);

        _completeMenuPanel.gameObject.SetActive(true);

        _completeMenuPanel.SetStarsAchieved(_scoreBarPanel.GetStarsAchieved());
    }



    public void DecreaseMove()
    {
        if(_movePanel != null && !_isComplete && !_isFailed)
        {
            _movePanel.DecreaseMove();

        }

        
    }


    public IEnumerator OnFailedStage()
    {
        _isFailed = true;

        yield return new WaitForSeconds(3);

        _loseMenuPanel.gameObject.SetActive(true);

        LifePanel.DecreaseLife();


    }

 

    public void IncreaseStar()
    {
        if(_scoreBarPanel != null)
        {
            _scoreBarPanel.IncreaseScoreBar();
        }
    }

    

}
