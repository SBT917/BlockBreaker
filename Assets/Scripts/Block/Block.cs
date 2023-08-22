using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBreakable
{
    public Color Color { get; private set; } //ブロックの色
    private Material material; //ブロックのマテリアル
    private Color[] blockColors = { Color.red, Color.green, Color.blue}; //ブロックの色のテーブル

    public event System.Action OnBreak; //ブロックが破壊されたときに呼ばれるイベント

    private void OnEnable()
    {
        material = GetComponent<Renderer>().material;

        //ブロックの色をランダムに設定
        Color = blockColors[Random.Range(0, blockColors.Length)]; 
        material.color = Color;
    }

    //破壊時処理
    public void Break()
    {
        OnBreak?.Invoke(); //イベントを発行
        gameObject.SetActive(false); //自身を無効にする
    }
}
