using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpriteController : MonoBehaviour
{
    [System.Serializable]
    class FanColor
    {
        public BlockTypeName blockType;
        public Sprite color;
    }

    [SerializeField] private List<SpriteRenderer> _fanSprites = new List<SpriteRenderer>();

    [SerializeField] private List<FanColor> _fanColors = new List<FanColor>();

    public void SetColor(BlockTypeName blockType)
    {

        Debug.Log("assign color for fan : " + blockType);
        Sprite color = null;

        foreach(FanColor fanColor in _fanColors)
        {
            if (fanColor.blockType.Equals(blockType)){
                color = fanColor.color;
                break;
            }
        }

        if (color != null) { 
            foreach(SpriteRenderer sprite in _fanSprites)
            {
                sprite.sprite = color;
            }
        }
    }
}
