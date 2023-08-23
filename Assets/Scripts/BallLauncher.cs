using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UIElements;

//ボールの発射とボールの残機を管理する
public class BallLauncher : MonoBehaviour
{
    [SerializeField] private PlayerController player; //プレイヤーの参照
    [SerializeField] private PlayerInput input; //InputSystem
    [SerializeField] private BallController ballPrefab; //ボールのプレファブ
    [SerializeField] private int ballRemain = 4; //残りボール数
    [SerializeField] private TextMeshProUGUI remainText; //残りボール数のテキスト
    [SerializeField] private Arrow arrow; //矢印の参照

    
    private BallController ball; //ボールの参照
    
    public static event Action OnBallRemainZero; //残りボールが0になった時のイベント

    private void Start()
    {
        remainText .text = "x" + ballRemain.ToString();
    }

    private void OnEnable()
    {
        input.actions["BallLaunch"].performed += OnLaunch;
    }

    private void OnDisable()
    {
        input.actions["BallLaunch"].performed -= OnLaunch;
    }

    //ボールの発射
    private void LaunchBall(Vector2 angle)
    {
        --ballRemain;
        remainText.text = "x" + ballRemain.ToString();
        ball = Instantiate(ballPrefab, player.transform.position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
        ball.Direction = angle; //ボールの発射方向を設定
        ball.OnOutOfScreen += () => { 
            if(arrow != null) arrow.gameObject.SetActive(true);
            CheckBallRemain();
        };
    }

    
    //残りボール数をチェック
    private void CheckBallRemain()
    {
        if(ballRemain <= 0)
        {
            arrow.gameObject.SetActive(false);
            OnBallRemainZero?.Invoke();
        }
    }

    //ボールを発射するキーを押したときの処理
    private void OnLaunch(InputAction.CallbackContext context)
    {
        if(ball != null) return;
        LaunchBall(arrow.ArrowVector());
        arrow.gameObject.SetActive(false);
    }

    

}
