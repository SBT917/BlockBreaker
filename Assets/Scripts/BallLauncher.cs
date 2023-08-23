using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UIElements;

//�{�[���̔��˂ƃ{�[���̎c�@���Ǘ�����
public class BallLauncher : MonoBehaviour
{
    [SerializeField] private PlayerController player; //�v���C���[�̎Q��
    [SerializeField] private PlayerInput input; //InputSystem
    [SerializeField] private BallController ballPrefab; //�{�[���̃v���t�@�u
    [SerializeField] private int ballRemain = 4; //�c��{�[����
    [SerializeField] private TextMeshProUGUI remainText; //�c��{�[�����̃e�L�X�g
    [SerializeField] private Arrow arrow; //���̎Q��

    
    private BallController ball; //�{�[���̎Q��
    
    public static event Action OnBallRemainZero; //�c��{�[����0�ɂȂ������̃C�x���g

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

    //�{�[���̔���
    private void LaunchBall(Vector2 angle)
    {
        --ballRemain;
        remainText.text = "x" + ballRemain.ToString();
        ball = Instantiate(ballPrefab, player.transform.position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
        ball.Direction = angle; //�{�[���̔��˕�����ݒ�
        ball.OnOutOfScreen += () => { 
            if(arrow != null) arrow.gameObject.SetActive(true);
            CheckBallRemain();
        };
    }

    
    //�c��{�[�������`�F�b�N
    private void CheckBallRemain()
    {
        if(ballRemain <= 0)
        {
            arrow.gameObject.SetActive(false);
            OnBallRemainZero?.Invoke();
        }
    }

    //�{�[���𔭎˂���L�[���������Ƃ��̏���
    private void OnLaunch(InputAction.CallbackContext context)
    {
        if(ball != null) return;
        LaunchBall(arrow.ArrowVector());
        arrow.gameObject.SetActive(false);
    }

    

}
