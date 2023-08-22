using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//ボールのスポーンと残機を管理する
public class BallSpawnManager : MonoBehaviour
{
    [SerializeField] private PlayerController player; //プレイヤーの参照
    [SerializeField] private BallController ballPrefab; //ボールのプレファブ
    [SerializeField] private float respawnTime = 2.0f; //ボールのリスポーン時間
    [SerializeField] private int ballRemain = 4; //残りボール数
    [SerializeField] private TextMeshProUGUI remainText; //残りボール数のテキスト
    
    public static event Action OnBallRemainZero; //残りボールが0になった時のイベント

    private void Awake()
    {
        StartCoroutine(SpawnBall(0.0f)); //初期配置
    }

    //ボールのスポーン
    private IEnumerator SpawnBall(float time)
    {
        yield return new WaitForSeconds(time);

        //ボールの数が0未満になったらゲームオーバー
        if (--ballRemain < 0)
        {
            OnBallRemainZero?.Invoke(); 
            yield break;
        }

        remainText.text = "x" + ballRemain.ToString();
        var ball = Instantiate(ballPrefab, player.transform.position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
        ball.OnOutOfScreen += () => { StartCoroutine(SpawnBall(respawnTime)); };
    }
}
