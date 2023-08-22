using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//�{�[���̃X�|�[���Ǝc�@���Ǘ�����
public class BallSpawnManager : MonoBehaviour
{
    [SerializeField] private PlayerController player; //�v���C���[�̎Q��
    [SerializeField] private BallController ballPrefab; //�{�[���̃v���t�@�u
    [SerializeField] private float respawnTime = 2.0f; //�{�[���̃��X�|�[������
    [SerializeField] private int ballRemain = 4; //�c��{�[����
    [SerializeField] private TextMeshProUGUI remainText; //�c��{�[�����̃e�L�X�g
    
    public static event Action OnBallRemainZero; //�c��{�[����0�ɂȂ������̃C�x���g

    private void Awake()
    {
        StartCoroutine(SpawnBall(0.0f)); //�����z�u
    }

    //�{�[���̃X�|�[��
    private IEnumerator SpawnBall(float time)
    {
        yield return new WaitForSeconds(time);

        //�{�[���̐���0�����ɂȂ�����Q�[���I�[�o�[
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
