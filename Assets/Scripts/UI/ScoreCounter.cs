using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : Singleton<ScoreCounter> //�V���O���g���p��
{
    private TextMeshProUGUI scoreText; //�X�R�A��\������e�L�X�g
    private int score = 0; //�X�R�A
    private string scoreTextFormat = "D7"; //�X�R�A�̕\���`��

    [SerializeField] private int scorePerBlock = 10; //�u���b�N1������̃X�R�A

    private void Start()
    {
        TryGetComponent(out scoreText);

        //�u���b�N�̔j�󎞂ɃX�R�A�����Z���鏈����o�^
        var cellListblock = BlockManager.instance.CellList;
        foreach (var cell in cellListblock)
        {
            cell.Block.OnBreak += AddScore;
        }

        scoreText.text = score.ToString(scoreTextFormat);
    }

    //�X�R�A�����Z����
    private void AddScore()
    {
        score += scorePerBlock * ComboCounter.instance.Combo;
        scoreText.text = score.ToString(scoreTextFormat);
    }
}
