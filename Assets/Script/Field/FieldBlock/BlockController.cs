using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockController : MonoBehaviour
{
    [SerializeField] private int _type;

    [SerializeField] private SpriteRenderer _icon;

    [SerializeField] private Sprite _defaultIcon;

    private bool _isBooster;
    public bool IsBooster
    {
        get { return _isBooster; }
        
    }

    public int Type { get { return _type; } }

    public bool IsEqual(BlockController other)
    {
        
        return _type == other._type;
    }

    private void OnMouseDown()
    {

        GamePlayManager.Instance.OnBlockClicked(this);
    }

    public void OnInActiveEvent()
    {

        gameObject.SetActive(false);
        Debug.Log("block in " + FieldDrawController.TransformFromPosToMatrixIndex(gameObject.transform.position) + " is " + gameObject.activeSelf);

    }

    public void CheckIsBooster(Sprite boosterIcon)
    {
        if(boosterIcon != null)
        {
            _isBooster = true;
            _icon.sprite = boosterIcon;
        }
        else
        {
            _isBooster = false;
            _icon.sprite = _defaultIcon;
        }
    }

}
