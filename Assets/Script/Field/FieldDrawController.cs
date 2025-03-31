using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;

public class FieldDrawController : MonoBehaviour
{

    private static Dictionary<Vector2, Vector2> _blockPosToMatrixIndexDic = new Dictionary<Vector2, Vector2>();

    [SerializeField] private int _numRows;

    [SerializeField] private int _numCols;

    [SerializeField] private BlockPool _blockPool;

    [SerializeField] private Transform _centerPoint;

    [SerializeField] private GameObject _fieldBlock;

    private BlockController[,] _field;
    private float _blockHeight;
    private float _blockWidth;

    private Vector2 _summonPoint;

    public BlockController[,] Field
    {
        get { return _field; }
    }

    public int NumRows
    {
        get { return this._numRows; }
    }

    public int NumCols
    {
        get { return this._numCols; }
    }

  

    void Awake()
    {
        _field = new BlockController[_numRows, _numCols];
    }
    void Start()
    {

        //construct draw attributes
        _blockHeight = _fieldBlock.GetComponent<SpriteRenderer>().size.y * _fieldBlock.transform.localScale.y;

        _blockWidth = _fieldBlock.GetComponent<SpriteRenderer>().size.x * _fieldBlock.transform.localScale.x;


        //calculate initial draw point
        float summonPointX = _centerPoint.position.x - NumRows * _blockWidth * 0.5f;
        float summonPointY = _centerPoint.position.y + NumCols * _blockHeight * 0.5f;
        _summonPoint = new Vector2(summonPointX, summonPointY);


        //construct field
        for (int i = 0; i < _numRows; i++)
        {
            for (int j = 0; j < _numCols; j++)
            {
                Vector2 renderPos = GetRenderFieldBlockPos(j, i);

                _field[i, j] = _blockPool.RetrieveBlockFromPool(renderPos);

                Vector2 roundRenderPos = new Vector2((float) Math.Round(renderPos.x, 2), (float)Math.Round(renderPos.y, 2));

                if (!_blockPosToMatrixIndexDic.ContainsKey(roundRenderPos))
                {
                    _blockPosToMatrixIndexDic[roundRenderPos] = new Vector2(i, j);
                }

                


            }
        }


    }

    public Vector2 TransformFromMatrixIndexToPos(int rowIndex, int colIndex)
    {
        return GetRenderFieldBlockPos( colIndex, rowIndex);
    }

    public static Vector2 TransformFromPosToMatrixIndex(Vector2 pos)
    {
        Vector2 roundPos = new Vector2((float)Math.Round(pos.x, 2), (float)Math.Round(pos.y, 2));

        Vector2 matrixIndex = _blockPosToMatrixIndexDic[roundPos];

        float rowIndex = matrixIndex.x;

        float colIndex = matrixIndex.y;

        return new Vector2(rowIndex, colIndex);
    }

    public Vector2 GetRenderFieldBlockPos(int colIndex, int rowIndex)
    {

        float posX = _summonPoint.x + _blockWidth * colIndex;

        float posY = _summonPoint.y - _blockHeight * rowIndex;

        return new Vector2( posX, posY);
    }
    
}
