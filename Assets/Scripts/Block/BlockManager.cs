using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//すべてのブロックとブロックセルを管理するクラス
public class BlockManager : Singleton<BlockManager> //シングルトン継承
{
    public BlockCell[,] CellList { get; private set; } //ブロック配置の二次元配列
    [SerializeField] private BlockCell blockCellPrefab; //ブロック生成セルのプレファブ

    [SerializeField] private Vector3 startPosition; //ブロックの生成開始位置

    [SerializeField] private int blockNumX; //ブロックの数（横）
    [SerializeField] private int blockNumY; //ブロックの数（縦）

    [SerializeField] private float blockIntervalX; //ブロックの間隔（横）
    [SerializeField] private float blockIntervalY; //ブロックの間隔（縦）

    protected override void Awake()
    {
        CellList = new BlockCell[blockNumY, blockNumX]; //ブロックの二次元配列を初期化

        //ブロックセルの配置
        for (int y = 0; y < blockNumY; y++)
        {
            for (int x = 0; x < blockNumX; x++)
            {
                Vector3 pos = startPosition;
                pos.x += blockIntervalX * x;
                pos.y -= blockIntervalY * y;

                var cell = Instantiate(blockCellPrefab, pos, Quaternion.identity, transform);
                cell.CellNum = new Vector2Int(x, y); //セルの番号を設定
                cell.Block.OnBreak += () => { StartCoroutine(Check4DirColor(cell)); }; //ブロックが破壊されたときに呼ばれるイベント
                CellList[y, x] = cell;
            }
        }

        base.Awake();
    }

    //指定したセル番地が有効かつブロックが存在しているか
    private bool CheckExsisting(Vector2Int index)
    {
        if (index.x < 0 || index.x >= blockNumX || index.y < 0 || index.y >= blockNumY) return false;
        return CellList[index.y, index.x].IsBlockExisting;
    }

    //index1とindex2のブロックの色が同じかを判定する
    private bool JudgeBlockColor(Vector2Int index1, Vector2Int index2)
    {
        if (CellList[index1.y, index1.x].Block.Color == CellList[index2.y, index2.x].Block.Color)
            return true;

        return false;
    }

    //引数のセルの上下左右のブロックの色をチェックし、色が一致したブロックを破壊する
    private IEnumerator Check4DirColor(BlockCell cell)
    {
        Vector2Int current = cell.CellNum; //現在のセル番地

        //チェックするセル番地
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
