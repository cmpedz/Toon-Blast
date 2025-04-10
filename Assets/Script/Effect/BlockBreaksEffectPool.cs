using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreaksEffectPool : MonoBehaviour
{

    private static BlockBreaksEffectPool _instance;

    public static BlockBreaksEffectPool Instance
    {
        get { return _instance; }
        
    }

    [SerializeField] private ParticleSystem _blockBreakEffect;

    [SerializeField] private int _quantities;

    [SerializeField] private BlockBreakColorSO _blockBreakColor;

    

    private void Awake()
    {
        if(_instance == null)
        {
            SimplePool.Preload(_blockBreakEffect.gameObject, _quantities, transform);
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

    public ParticleSystem GetBlockBreakEffect(BlockTypeName blockType, Transform activePos)
    {
        ParticleSystem blockBreakEffect = SimplePool.Spawn(_blockBreakEffect, activePos.position, Quaternion.identity);

        _blockBreakColor.SetColor(blockType, blockBreakEffect);

        return blockBreakEffect;
    }


}
