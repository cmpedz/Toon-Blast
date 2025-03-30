using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockController : MonoBehaviour
{
    [SerializeField] private int type;
    public int Type { get { return type; } }

    public bool IsEqual(BlockController other)
    {
        
        return type == other.type;
    }

    private void OnMouseDown()
    {

        GamePlayController.Instance.OnBlockClicked(this);
    }

    public void OnDestroyEvent()
    {

        gameObject.SetActive(false);
        Debug.Log("block in " + FieldDrawController.TransformFromPosToMatrixIndex(gameObject.transform.position) + " is " + gameObject.activeSelf);

    }

}
