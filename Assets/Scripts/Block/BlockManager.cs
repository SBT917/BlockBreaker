using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//���ׂẴu���b�N�ƃu���b�N�Z�����Ǘ�����N���X
public class BlockManager : Singleton<BlockManager> //�V���O���g���p��
{
    public BlockCell[,] CellList { get; private set; } //�u���b�N�z�u�̓񎟌��z��
    [SerializeField] private BlockCell blockCellPrefab; //�u���b�N�����Z���̃v���t�@�u

    [SerializeField] private Vector3 startPosition; //�u���b�N�̐����J�n�ʒu

    [SerializeField] private int blockNumX; //�u���b�N�̐��i���j
    [SerializeField] private int blockNumY; //�u���b�N�̐��i�c�j

    [SerializeField] private float blockIntervalX; //�u���b�N�̊Ԋu�i���j
    [SerializeField] private float blockIntervalY; //�u���b�N�̊Ԋu�i�c�j

    protected override void Awake()
    {
        CellList = new BlockCell[blockNumY, blockNumX]; //�u���b�N�̓񎟌��z���������

        //�u���b�N�Z���̔z�u
        for (int y = 0; y < blockNumY; y++)
        {
            for (int x = 0; x < blockNumX; x++)
            {
                Vector3 pos = startPosition;
                pos.x += blockIntervalX * x;
                pos.y -= blockIntervalY * y;

                var cell = Instantiate(blockCellPrefab, pos, Quaternion.identity, transform);
                cell.CellNum = new Vector2Int(x, y); //�Z���̔ԍ���ݒ�
                cell.Block.OnBreak += () => { StartCoroutine(Check4DirColor(cell)); }; //�u���b�N���j�󂳂ꂽ�Ƃ��ɌĂ΂��C�x���g
                CellList[y, x] = cell;
            }
        }

        base.Awake();
    }

    //�w�肵���Z���Ԓn���L�����u���b�N�����݂��Ă��邩
    private bool CheckExsisting(Vector2Int index)
    {
        if (index.x < 0 || index.x >= blockNumX || index.y < 0 || index.y >= blockNumY) return false;
        return CellList[index.y, index.x].IsBlockExisting;
    }

    //index1��index2�̃u���b�N�̐F���������𔻒肷��
    private bool JudgeBlockColor(Vector2Int index1, Vector2Int index2)
    {
        if (CellList[index1.y, index1.x].Block.Color == CellList[index2.y, index2.x].Block.Color)
            return true;

        return false;
    }

    //�����̃Z���̏㉺���E�̃u���b�N�̐F���`�F�b�N���A�F����v�����u���b�N��j�󂷂�
    private IEnumerator Check4DirColor(BlockCell cell)
    {
        Vector2Int current = cell.CellNum; //���݂̃Z���Ԓn

        //�`�F�b�N����Z���Ԓn
        Vector2Int[] checkPos = 
        {
            new Vector2Int(current.x - 1, current.y), new Vector2Int(current.x + 1, current.y),
            new Vector2Int(current.x, current.y - 1), new Vector2Int(current.x, current.y + 1),
        };

        yield return new WaitForSeconds(0.1f);

        foreach (var p in checkPos)
        {
            if(!CheckExsisting(p)) continue;
            if (JudgeBlockColor(current, p))
            {
                CellList[p.y, p.x].Block.Break();
            }
        }

    }
}
