using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//���͂���v���C���[�̓����𐧌䂷��X�N���v�g
public class PlayerController : MonoBehaviour
{
    private PlayerInput input; //InputSystem;
    private Rigidbody rigid; //���W�b�g�{�f�B
    private Vector3 moveDirection; //�ړ�����

    [SerializeField] private float moveSpeed; //�ړ����x
    [SerializeField] private float minMoveRange; //�ړ��͈͂̍ŏ��l
    [SerializeField] private float maxMoveRange; //�ړ��͈͂̍ő�l

    private void Awake()
    {
        TryGetComponent(out input);
        TryGetComponent(out rigid);
    }

    private void OnEnable()
    {
        input.actions["Move"].performed += OnMove;
        input.actions["Move"].canceled += OnMoveStop;
    }

    private void OnDisable()
    {
        input.actions["Move"].performed -= OnMove;
        input.actions["Move"].canceled -= OnMoveStop;
    }

    private void FixedUpdate()
    {
        Move(moveDirection);
    }

    //�ړ��L�[���������Ƃ��̏���
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector3 value = context.ReadValue<Vector2>();
        moveDirection = value;
    }

    //�ړ��L�[�𗣂����Ƃ��̏���
    private void OnMoveStop(InputAction.CallbackContext context)
    {
        moveDirection = Vector3.zero;
    }

    //FixedUpdate�ŉ񂷈ړ�����
    private void Move(Vector3 direction)
    {
        Vector3 move = transform.position + direction * moveSpeed * Time.deltaTime;
        move.x = Mathf.Clamp(move.x, minMoveRange, maxMoveRange); //�ړ��͈͂𐧌�
        rigid.MovePosition(move);
    }

}
