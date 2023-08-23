using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

//ボール発射時に表示する矢印
public class Arrow : MonoBehaviour
{
    private float angle = 90; //矢印の現在の角度
    private float rotate; //矢印の回転量
    private float rotateSpeed = 100; //矢印の回転速度
    private float maxRotation = 45.0f; //矢印の最大回転角度
    private float minRotation = -45.0f; //矢印の最小回転角度

    [SerializeField]private PlayerInput input; //プレイヤーの入力

    private void Update()
    {
        ArrowRotate();
    }

    private void OnEnable()
    {
        input.actions["ArrowMove"].performed += OnArrowRotate;
        input.actions["ArrowMove"].canceled += OnArrowRotateStop;
    }

    private void OnDisable()
    {
        input.actions["ArrowMove"].performed -= OnArrowRotate;
        input.actions["ArrowMove"].canceled -= OnArrowRotateStop;
    }

    //矢印の向きからベクトルを取得
    public Vector2 ArrowVector()
    {
        float radian = angle * (Mathf.PI / 180);
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;
    }

    //矢印の回転処理
    private void ArrowRotate()
    {
        Vector3 rot = transform.eulerAngles + new Vector3(0.0f, 0.0f, -rotate) * rotateSpeed * Time.deltaTime;
        //rot.z = Mathf.Clamp(rot.z, minRotation, maxRotation); //回転角度を制限
        transform.eulerAngles = rot;
    }

    //矢印の回転キーを押したときの処理
    private void OnArrowRotate(InputAction.CallbackContext context)
    {
        Vector2 rot = context.ReadValue<Vector2>();
        rotate = rot.x;
    }

    //矢印の回転キーを離したときの処理
    private void OnArrowRotateStop(InputAction.CallbackContext context)
    {
        angle = transform.eulerAngles.z + 90;
        rotate = 0;
    }

}
