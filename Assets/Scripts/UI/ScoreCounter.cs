using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : Singleton<ScoreCounter> //シングルトン継承
{
    private TextMeshProUGUI scoreText; //スコアを表示するテキスト
    private int score = 0; //スコア
    private string scoreTextFormat = "D7"; //スコアの表示形式

    [SerializeField] private int scorePerBlock = 10; //ブロック1つあたりのスコア

    private void Start()
    {
        TryGetComponent(out scoreText);

        //ブロックの破壊時にスコアを加算する処理を登録
        var cellListblock = BlockManager.instance.CellList;
        foreach (var cell in cellListblock)
        {
            cell.Block.OnBreak += AddScore;
        }

        scoreText.text = score.ToString(scoreTextFormat);
    }

    //スコアを加算する
    private void AddScore()
    {
        score += scorePerBlock * ComboCounter.instance.Combo;
        scoreText.text = score.ToString(scoreTextFormat);
    }
}
