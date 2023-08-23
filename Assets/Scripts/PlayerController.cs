using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//入力からプレイヤーの動きを制御するスクリプト
public class PlayerController : MonoBehaviour
{
    private PlayerInput input; //InputSystem;
    private Rigidbody rigid; //リジットボディ
    private Vector3 moveDirection; //移動方向

    [SerializeField] private float moveSpeed; //移動速度
    [SerializeField] private float minMoveRange; //移動範囲の最小値
    [SerializeField] private float maxMoveRange; //移動範囲の最大値

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

    //移動キーを押したときの処理
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector3 value = context.ReadValue<Vector2>();
        moveDirection = value;
    }

    //移動キーを離したときの処理
    private void OnMoveStop(InputAction.CallbackContext context)
    {
        moveDirection = Vector3.zero;
    }

    //FixedUpdateで回す移動処理
    private void Move(Vector3 direction)
    {
        Vector3 move = transform.position + direction * moveSpeed * Time.deltaTime;
        move.x = Mathf.Clamp(move.x, minMoveRange, maxMoveRange); //移動範囲を制限
        rigid.MovePosition(move);
    }

}
