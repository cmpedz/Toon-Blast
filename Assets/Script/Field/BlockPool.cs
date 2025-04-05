using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    [SerializeField] private List<BlockController> _fieldBlocksType = new List<BlockController>();

    [SerializeField] private int _quantititesBlock;

    [SerializeField] private List<BlockController> _fieldBlocksPool = new List<BlockController>();

    [SerializeField] private Queue<BlockController> _inactiveBlocksPool = new Queue<BlockController>();
    [SerializeField] private Transform _blocksContain;

    private static BlockPool instance;
    public static BlockPool Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            for (int i = 0; i < _quantititesBlock; i++)
            {
                _fieldBlocksPool.Add(CreateRandomBlock(i + 1));
                _inactiveBlocksPool.Enqueue(_fieldBlocksPool[i]);
            }
        }
        else
        {
            if (instance != gameObject) 
            {
                Destroy(gameObject);
            }
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

    public BlockController RetrieveBlockFromPool(Vector2 position)
    {
        if (_inactiveBlocksPool.Count == 0)
        {
            Debug.Log("inactive pool is null");
            return null;
        }

        BlockController block = _inactiveBlocksPool.Dequeue();
        block.gameObject.SetActive(true);
        block.transform.position = position;
        return block;
    }

    public void SendBlockBackToPool(BlockController block)
    {
        
        _inactiveBlocksPool.Enqueue(block);
        
        block.OnDestroyEvent();
    }
    

    
}
