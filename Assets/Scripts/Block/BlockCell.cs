using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ブロックの生成、再生成を行うクラス
public class BlockCell : MonoBehaviour
{
    [SerializeField] private Block blockPrefab; //ブロックのプレファブ

    public Block Block { get; private set; } //生成されたブロック
    public bool IsBlockExisting { get; private set; } //ブロックが存在するかどうか
    public Vector2Int CellNum { get; set; } //セルの番号

    void Awake()
    {
        GenerateBlock();     
    }

    //ブロックの生成
    private void GenerateBlock()
    {
        Block = Instantiate(blockPrefab, transform);

        //ブロックが破壊されたときの処理を登録
        Block.OnBreak += () =>
        {
            IsBlockExisting = false;
            StartCoroutine(RegenerateCoroutine());
        };

        IsBlockExisting = true;
    }

    //ブロックの再生成コルーチン
    private IEnumerator RegenerateCoroutine()
    {
        yield return (new WaitForSeconds(15.0f));
        Block.gameObject.SetActive(true);
        IsBlockExisting = true;
    }
}
