using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ブロックの生成を行うクラス(基本的にブロックはこれを通して生成される)
public class BlockGenerateCell : MonoBehaviour
{
    [SerializeField] private Block blockPrefab; //ブロックのプレファブ

    private Block block; //生成されたブロック
    private bool isBlockExistind; //ブロックが存在するかどうか
    private Vector2Int cellNum; //セルの番号

    public Block Block //ブロックのプロパティ
    {
        get { return block; }
    }

    public bool IsBlockExisting //ブロックが存在するかどうかのプロパティ
    {
        get { return isBlockExistind; }
    }

    public Vector2Int CellNum //セルの番号のプロパティ
    {
        get { return cellNum; }
        set { cellNum = value; }
    }

    void Awake()
    {
        GenerateBlock();
    }

    //ブロックの生成
    private void GenerateBlock()
    {
        block = Instantiate(blockPrefab, transform);

        //ブロックが破壊されたときの処理を登録
        block.OnBreak += () =>
        {
            isBlockExistind = false;
            //StartCoroutine(RegenerateCoroutine());
        };

        isBlockExistind = true;
    }

    //ブロックの再生成コルーチン
    private IEnumerator RegenerateCoroutine()
    {
        Debug.Log("再生成中");
        yield return (new WaitForSeconds(10.0f));
        GenerateBlock();
    }
}
