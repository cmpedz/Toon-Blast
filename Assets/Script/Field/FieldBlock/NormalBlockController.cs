using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlockController : BlockController
{

    [SerializeField] private SpriteRenderer _icon;

    [SerializeField] private Sprite _defaultIcon;

    [SerializeField] private string _boosterName;

    

    public string BoosterName
    {
        get { return this._boosterName; }
    }


    public void CheckIsAbleMergedBooster(BoosterSOData.BoosterData booster)
    {
        if (booster != null )
        {
            
            _icon.sprite = booster.icon;

            _boosterName = booster.boosterName.ToString();
        }
        else
        {
            
            _icon.sprite = _defaultIcon;

            _boosterName = "";
        }
    }

    public override void OnDestroyEvent()
    {
        base.OnDestroyEvent();

        ParticleSystem blockBreakEffect = BlockBreaksEffectPool.Instance.GetBlockBreakEffect(Type, transform);

        blockBreakEffect.Play();

        gameObject.SetActive(false);

        _icon.sprite = _defaultIcon;

        _boosterName = "";
    }

}
