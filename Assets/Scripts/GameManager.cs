using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> //�V���O���g���p��
{
    [SerializeField] private Vector3 playerSpanwPosition; //�v���C���[�̃X�|�[���ʒu
    [SerializeField] private PlayerController playerPrefab; //�v���C���[�̃v���n�u 
    [SerializeField] private GameObject gameOverPanel; //�Q�[���I�[�o�[���ɕ\������p�l��

    private PlayerController playerController; //�v���C���[�̎Q��
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

    //�Q�[���I�[�o�[���ɌĂ΂�鏈��
    private void GameOver()
    {
        gameOverPanel.SetActive(true); //�Q�[���I�[�o�[���̃p�l����\��
        input.currentActionMap = input.actions.actionMaps[1];  //�Q�[���I�[�o�[����actionMap�ɐ؂�ւ�
        input.actions["Continue"].performed += OnContinue; //Continue�{�^�����������Ƃ��̏�����ǉ�
    }


    //�Q�[���I�[�o�[����Continue�{�^�����������Ƃ��̏���
    private void OnContinue(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainScene");
    }
}
