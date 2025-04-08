using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class RocketBoosterController : BoosterBlockController
{
    [SerializeField] private RocketController _rocket;

    [SerializeField] private RocketController _reRocket;

    [SerializeField] private float _speedMove;


    private bool _isBothRocketOutOfMatrix = false;

    private bool _isBothRocketOverComeScreen = false;

    private bool _isActive;

    private FieldDrawController _field;


    public override async UniTask OnBoosterActive(FieldDrawController field)
    {
        Vector2 currentPosInMatrix = FieldDrawController.TransformFromPosToMatrixIndex(gameObject.transform.position);
        int currentRowIndex = (int)currentPosInMatrix.x;
        int currentColIndex = (int)currentPosInMatrix.y;

        field.Field[currentRowIndex, currentColIndex] = null;

        _field = field;

        FireRocket(_rocket);

        FireRocket(_reRocket);

        await UniTask.WaitUntil(() => this._isBothRocketOverComeScreen);


    }

    private void FireRocket(RocketController rocket)
    {
        rocket.Active = true;
        rocket.Field = _field;
    }


    private void Update()
    {
        if(_reRocket.Active && _rocket.Active && _field != null)
        {
            //handle both rockets out of matrix


            _isBothRocketOutOfMatrix = _field.IsOverComeMatrix(_rocket.transform.position) && _field.IsOverComeMatrix(_reRocket.transform.position);


            //handle both rockets overcome screen
            Transform _topLeft = ScreenPos.Instance.topLeft;

            Transform _bottomRight = ScreenPos.Instance.bottomRight;

            _isBothRocketOverComeScreen =
                (_rocket.transform.position.x < _topLeft.transform.position.x 
                && _reRocket.transform.position.x > _bottomRight.position.x) ||
                (_reRocket.transform.position.y > _topLeft.transform.position.y 
                && _rocket.transform.position.y < _bottomRight.position.y );

            if (_isBothRocketOverComeScreen)
            {
                Destroy(gameObject);
            }


        }
       
           

            

    }
}
