using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class RocketBoosterController : BoosterBlockController, IEnableChangeRotation
{
    [SerializeField] private RocketController _rocket;

    [SerializeField] private RocketController _reRocket;

    [SerializeField] private float _speedMove;

    [SerializeField] private float _varience;


    private bool _isBothRocketOutOfMatrix = false;

    private bool _isBothRocketOverComeScreen = false;

    private bool _isActive;

    private FieldDrawController _field;

    public void ChangeRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public bool IsEnableRotation(List<BlockController> similarBlocksList)
    {
        List<float> rowIndexs = new List<float>();

        List<float> colIndexs = new List<float>();

        foreach (BlockController block in similarBlocksList) {
            Vector2 indexInMatrix = FieldDrawController.TransformFromPosToMatrixIndex(block.transform.position);
            rowIndexs.Add(indexInMatrix.x);
            colIndexs.Add(indexInMatrix.y);
        }

        rowIndexs.Sort();
        colIndexs.Sort();

        float distanceRowIndex = rowIndexs[rowIndexs.Count - 1] - rowIndexs[0];

        float distanceColIndex = colIndexs[colIndexs.Count - 1] - colIndexs[0];

        Debug.Log("check distance row index : " + distanceRowIndex);

        Debug.Log("check distance col index : " + distanceColIndex);

        if (distanceColIndex < distanceRowIndex) return true;

        return false;
    }

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
                (_rocket.transform.position.x < _topLeft.transform.position.x - _varience 
                && _reRocket.transform.position.x > _bottomRight.position.x + _varience) ||
                (_reRocket.transform.position.y > _topLeft.transform.position.y + _varience 
                && _rocket.transform.position.y < _bottomRight.position.y - _varience);

            if (_isBothRocketOverComeScreen)
            {
                Destroy(gameObject);
            }


        }
       
           

            

    }
}
