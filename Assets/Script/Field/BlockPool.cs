using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    [SerializeField] private List<BlockController> _fieldBlocksType = new List<BlockController>();

    [SerializeField] private int _quantititesBlock;

    [SerializeField] private List<BlockController> _fieldBlocksPool = new List<BlockController>();

    [SerializeField] private Transform _blocksContain;

    private void Awake()
    {
        for (int i = 0; i < _quantititesBlock; i++)
        {
            _fieldBlocksPool.Add(CreateRandomBlock(i + 1));


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
    public BlockController GetRandomBlock(Vector2 position)
    {
        foreach(BlockController block in _fieldBlocksPool)
        {
            if (!block.gameObject.activeInHierarchy) { 
                block.gameObject.SetActive(true);
                block.transform.position = position;    
                return block; 
            }
        }

        return null;
    }
    

    
}
