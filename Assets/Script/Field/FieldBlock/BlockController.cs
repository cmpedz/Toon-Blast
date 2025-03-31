using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockController : MonoBehaviour
{
    [SerializeField] private int type;

    private bool _isBooster;
    public bool IsBooster
    {
        get { return _isBooster; }
        set { _isBooster = value; } 
    }

    public int Type { get { return type; } }

    public bool IsEqual(BlockController other)
    {
        
        return type == other.type;
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

}
