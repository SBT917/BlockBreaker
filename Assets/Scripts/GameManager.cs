using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> //シングルトン継承
{
    [SerializeField] private Vector3 playerSpanwPosition; //プレイヤーのスポーン位置
    [SerializeField] private PlayerController playerPrefab; //プレイヤーのプレハブ 
    [SerializeField] private GameObject gameOverPanel; //ゲームオーバー時に表示するパネル

    private PlayerController playerController; //プレイヤーの参照
    private PlayerInput input; //InputSystem

    protected override void Awake()
    {
        playerController = Instantiate(playerPrefab, playerSpanwPosition, Quaternion.identity);
        playerController.TryGetComponent(out input);
        BallLauncher.OnBallRemainZero += GameOver;
        base.Awake();
    }

    private void OnDisable()
    {
        BallLauncher.OnBallRemainZero -= GameOver;
    }

    //ゲームオーバー時に呼ばれる処理
    private void GameOver()
    {
        gameOverPanel.SetActive(true); //ゲームオーバー時のパネルを表示
        input.currentActionMap = input.actions.actionMaps[1];  //ゲームオーバー時のactionMapに切り替え
        input.actions["Continue"].performed += OnContinue; //Continueボタンを押したときの処理を追加
    }


    //ゲームオーバー時にContinueボタンを押したときの処理
    private void OnContinue(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainScene");
    }
}
