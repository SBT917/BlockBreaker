using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI scoreText; //�X�R�A��\������e�L�X�g
    private int score = 0; //�X�R�A
    private string scoreTextFormat = "D7"; //�X�R�A�̕\���`��

    [SerializeField] private int scorePerBlock = 10; //�u���b�N1������̃X�R�A
    [SerializeField] private ComboCounter comboCounter; //ComboCounter�̎Q��
    [SerializeField] private BlockManager blockManager; //BlockManager�̎Q��

    private void Start()
    {
        TryGetComponent(out scoreText);

        //�u���b�N�̔j�󎞂ɃX�R�A�����Z���鏈����o�^
        var cellListblock = blockManager.CellList;
        foreach (var cell in cellListblock)
        {
            cell.Block.OnBreak += AddScore;
        }

        scoreText.text = score.ToString(scoreTextFormat);
    }

    //�X�R�A�����Z����
    private void AddScore()
    {
        score += scorePerBlock * comboCounter.Combo;
        scoreText.text = score.ToString(scoreTextFormat);
    }
}
