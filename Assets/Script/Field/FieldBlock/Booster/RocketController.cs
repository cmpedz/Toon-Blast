using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UIElements;

public class RocketController : MonoBehaviour
{
    [SerializeField] private float _speedMove;

    [SerializeField] private Transform _detectPoint;

    [SerializeField] private float _detectRadius;

    [SerializeField] private RocketBoosterController _rocketBooster;

    private FieldDrawController _field;

    private bool _active = false;

    public bool Active
    {
        get { return _active; }
        set { _active = value; }
    }

    public FieldDrawController Field
    {
        get { return _field; }
        set { _field = value; }
    }


    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_detectPoint.position, _detectRadius);
    }

    private void DetectBlocksToDestroy()
    {
        Collider2D detectedCollier = Physics2D.OverlapCircle(_detectPoint.position, _detectRadius);

        if (detectedCollier != null)
        {
            if (detectedCollier.tag == "Block")
            {
                Vector2 colliderPosInMatrix = FieldDrawController.TransformFromPosToMatrixIndex(detectedCollier.transform.position);

                if (colliderPosInMatrix.Equals(FieldDrawController.ErrorMatrixPos)) return;

                int currentRowIndex = (int)colliderPosInMatrix.x;
                int currentColIndex = (int)colliderPosInMatrix.y;

                if (_field.Field[currentRowIndex, currentColIndex] != null)
                {

                    BlockController block = _field.Field[currentRowIndex, currentColIndex];
                    _field.Field[currentRowIndex, currentColIndex] = null;

                    bool isBlockBooster = block.Type.Equals(BlockTypeName.Booster);

                    if (isBlockBooster)
                    {
                        BoosterBlockController booster = block as BoosterBlockController;

                        BoosterManager.Instance.CurrentBoostersActive.Add(booster.OnBoosterActive(_field));
                    }
                    else
                    {
                        BlockPool.Instance.SendBlockBackToPool(block);
                    }


                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_active)
        {
            Debug.Log("check rocket booster vector right : " + _rocketBooster.transform.right);

            transform.position += _rocketBooster.transform.right * _speedMove  * Time.deltaTime;

            DetectBlocksToDestroy();
            
        }
        
       
    }

  


}
