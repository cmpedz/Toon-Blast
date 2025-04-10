using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BlockTypeName
{
    Red,
    Green,
    Yellow,
    Purple,
    Booster
}


public abstract class BlockController : MonoBehaviour
{
    [SerializeField] private BlockTypeName _type;

    [SerializeField] protected ParticleSystem _destroyEffect;

    public BlockTypeName Type { get { return _type; } }

    public bool IsEqual(BlockController other)
    {

        return _type.Equals(other._type);
    }

    protected void Start()
    {
        
    }
    private async void OnMouseDown()
    {
        await GamePlayManager.Instance.OnBlockClicked(this);
        
    }



    public virtual void OnDestroyEvent()
    {
        GameEventManager.Instance.DecreaseQuantitiesTarget(1, Type);
        GameEventManager.Instance.IncreaseStar();
    }



}
