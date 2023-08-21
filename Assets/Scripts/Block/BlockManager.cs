using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private BlockGenerateCell[,] cellList; //ブロック配置の二次元配列
    [SerializeField] private BlockGenerateCell blockGenerateCellPrefab; //ブロック生成セルのプレファブ

    [SerializeField] private Vector3 startPosition; //ブロックの生成開始位置

    [SerializeField] private int blockNumX; //ブロックの数（横）
    [SerializeField] private int blockNumY; //ブロックの数（縦）

    [SerializeField] private float blockIntervalX; //ブロックの間隔（横）
    [SerializeField] private float blockIntervalY; //ブロックの間隔（縦）

    void Awake()
    {
        cellList = new BlockGenerateCell[blockNumY, blockNumX]; //ブロックの二次元配列を初期化

        //ブロックセルの配置
        for (int y = 0; y < blockNumY; y++)
        {
            for (int x = 0; x < blockNumX; x++)
            {
                Vector3 pos = startPosition;
                pos.x += blockIntervalX * x;
                pos.y -= blockIntervalY * y;

                var cell = Instantiate(blockGenerateCellPrefab, pos, Quaternion.identity, transform);
                cell.CellNum = new Vector2Int(x, y); //セルの番号を設定
                cell.Block.OnBreak += () => { StartCoroutine(CheckBlockColor(cell)); }; //ブロックが破壊されたときの処理を登録
                cellList[y, x] = cell;
            }
        }
    }

    //ブロックの上下左右の色をチェックするコルーチン
    private IEnumerator CheckBlockColor(BlockGenerateCell cell)
    {
        int x = cell.CellNum.x;
        int y = cell.CellNum.y;

        Debug.Log(x.ToString() + "," + y.ToString());

        yield return new WaitForSeconds(0.1f);

        //X左方向
        if (x > 0)
        {
            if (cellList[y, x - 1].Block.Color == cell.Block.Color && cellList[y, x - 1].IsBlockExisting)
            {
                cellList[y, x - 1].Block.Break();
            }
        }

        //X右方向
        if (x < blockNumX - 1)
        {
            if (cellList[y, x + 1].Block.Color == cell.Block.Color && cellList[y, x + 1].IsBlockExisting)
            {
                cellList[y, x + 1].Block.Break();
            }
        }

        //Y上方向
        if (y > 0)
        {
            if (cellList[y - 1, x].Block.Color == cell.Block.Color && cellList[y - 1, x].IsBlockExisting)
            {
                cellList[y - 1, x].Block.Break();
            }
        }

        //Y下方向
        if (y < blockNumY - 1)
        {
            if (cellList[y + 1, x].Block.Color == cell.Block.Color && cellList[y + 1, x].IsBlockExisting)
            {
                cellList[y + 1, x].Block.Break();
            }
        }
    }
}
