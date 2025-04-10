using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoosterBlockController : BlockController
{
    [SerializeField] private BoosterName _boosterName;
   
    public BoosterName BoosterName
    {
        get { return _boosterName; }
    }

    public virtual async UniTask OnBoosterActive(FieldDrawController field)
    {
        
    }

    public override void OnDestroyEvent()
    {
        base.OnDestroyEvent();

        if (_destroyEffect != null)
        {
            ParticleSystem destroyEffect = Instantiate(_destroyEffect, transform.position, Quaternion.identity);

            destroyEffect.Play();
        }

        Destroy(gameObject);
    }

}
