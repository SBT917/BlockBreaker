using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private BlockGenerateCell[,] cellList; //�u���b�N�z�u�̓񎟌��z��
    [SerializeField] private BlockGenerateCell blockGenerateCellPrefab; //�u���b�N�����Z���̃v���t�@�u

    [SerializeField] private Vector3 startPosition; //�u���b�N�̐����J�n�ʒu

    [SerializeField] private int blockNumX; //�u���b�N�̐��i���j
    [SerializeField] private int blockNumY; //�u���b�N�̐��i�c�j

    [SerializeField] private float blockIntervalX; //�u���b�N�̊Ԋu�i���j
    [SerializeField] private float blockIntervalY; //�u���b�N�̊Ԋu�i�c�j

    void Awake()
    {
        cellList = new BlockGenerateCell[blockNumY, blockNumX]; //�u���b�N�̓񎟌��z���������

        //�u���b�N�Z���̔z�u
        for (int y = 0; y < blockNumY; y++)
        {
            for (int x = 0; x < blockNumX; x++)
            {
                Vector3 pos = startPosition;
                pos.x += blockIntervalX * x;
                pos.y -= blockIntervalY * y;

                var cell = Instantiate(blockGenerateCellPrefab, pos, Quaternion.identity, transform);
                cell.CellNum = new Vector2Int(x, y); //�Z���̔ԍ���ݒ�
                cell.Block.OnBreak += () => { StartCoroutine(CheckBlockColor(cell)); }; //�u���b�N���j�󂳂ꂽ�Ƃ��̏�����o�^
                cellList[y, x] = cell;
            }
        }
    }

    //�u���b�N�̏㉺���E�̐F���`�F�b�N����R���[�`��
    private IEnumerator CheckBlockColor(BlockGenerateCell cell)
    {
        int x = cell.CellNum.x;
        int y = cell.CellNum.y;

        Debug.Log(x.ToString() + "," + y.ToString());

        yield return new WaitForSeconds(0.1f);

        //X������
        if (x > 0)
        {
            if (cellList[y, x - 1].Block.Color == cell.Block.Color && cellList[y, x - 1].IsBlockExisting)
            {
                cellList[y, x - 1].Block.Break();
            }
        }

        //X�E����
        if (x < blockNumX - 1)
        {
            if (cellList[y, x + 1].Block.Color == cell.Block.Color && cellList[y, x + 1].IsBlockExisting)
            {
                cellList[y, x + 1].Block.Break();
            }
        }

        //Y�����
        if (y > 0)
        {
            if (cellList[y - 1, x].Block.Color == cell.Block.Color && cellList[y - 1, x].IsBlockExisting)
            {
                cellList[y - 1, x].Block.Break();
            }
        }

        //Y������
        if (y < blockNumY - 1)
        {
            if (cellList[y + 1, x].Block.Color == cell.Block.Color && cellList[y + 1, x].IsBlockExisting)
            {
                cellList[y + 1, x].Block.Break();
            }
        }
    }
}
