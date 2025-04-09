using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockBreakColor", menuName = "ScriptableObject/BlockBreakColor")]
public class BlockBreakColorController : ScriptableObject
{

    [System.Serializable]
    public class BlockColor
    {
        public BlockTypeName type;
        public Color color;
    }

    [SerializeField] private List<BlockColor> colors = new List<BlockColor>();


    public void SetColor(BlockTypeName type, ParticleSystem _blockBreakEffect)
    {
        foreach(BlockColor blockColor in colors)
        {
            if(blockColor.type == type)
            {
                var _main = _blockBreakEffect.main;
                _main.startColor = new Color(blockColor.color.r, blockColor.color.g, blockColor.color.b);
                break;
            }
        }
    }

}
