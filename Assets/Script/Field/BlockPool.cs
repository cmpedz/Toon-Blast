using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    [SerializeField] private List<BlockController> _fieldBlocksType = new List<BlockController>();

    [SerializeField] private int _quantititesBlock;

    [SerializeField] private List<BlockController> _fieldBlocksPool = new List<BlockController>();

    [SerializeField] private Queue<BlockController> _inactiveBlocksPool = new Queue<BlockController>();
    [SerializeField] private Transform _blocksContain;

    private void Awake()
    {
        for (int i = 0; i < _quantititesBlock; i++)
        {
            _fieldBlocksPool.Add(CreateRandomBlock(i + 1));
            _inactiveBlocksPool.Enqueue(_fieldBlocksPool[i]);
        }
    }

    private void Start()
    {
      
    }

    public BlockController CreateRandomBlock(int orderSummon)
    {
        int index = Random.Range(0, _fieldBlocksType.Count);

        _fieldBlocksType[index].gameObject.SetActive(false);


        BlockController newBlock = Instantiate(_fieldBlocksType[index]);

        newBlock.transform.parent = _blocksContain;

        newBlock.gameObject.name += " " + orderSummon;

        return newBlock;  
    }

    //bug do lỗi GetRandomBlock : khả năng logic kiểm tra block inactive đang lỗi
    public BlockController RetrieveBlockFromPool(Vector2 position)
    {
        BlockController block = _inactiveBlocksPool.Dequeue();
        block.gameObject.SetActive(true);
        block.transform.position = position;
        return block;
    }

    public void SendBlockBackToPool(BlockController block)
    {
        _inactiveBlocksPool.Enqueue(block);
        block.OnInActiveEvent();
    }
    

    
}
