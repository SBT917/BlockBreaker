using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBreakable
{
    private Color color; //ブロックの色
    private Material material; //ブロックのマテリアル
    private Color[] blockColors = { Color.red, Color.blue, Color.green, Color.yellow }; //ブロックの色のテーブル

    public Color Color //ブロックの色のゲッター
    {
        get { return color; }
    }

    public event System.Action OnBreak; //ブロックが破壊されたときに呼ばれるイベント

    void Awake()
    {
        material = GetComponent<Renderer>().material;

        //ブロックの色をランダムに設定
        color = blockColors[Random.Range(0, blockColors.Length)]; 
        material.color = color;
    }

    //破壊時処理
    public void Break()
    {
        OnBreak?.Invoke(); //イベントを発行
        Destroy(gameObject);//自身を破壊する
    }
}
