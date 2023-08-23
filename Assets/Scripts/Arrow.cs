using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

//�{�[�����ˎ��ɕ\��������
public class Arrow : MonoBehaviour
{
    private float angle = 90; //���̌��݂̊p�x
    private float rotate; //���̉�]��
    private float rotateSpeed = 100; //���̉�]���x
    private float maxRotation = 45.0f; //���̍ő��]�p�x
    private float minRotation = -45.0f; //���̍ŏ���]�p�x

    [SerializeField]private PlayerInput input; //�v���C���[�̓���

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

    //���̌�������x�N�g�����擾
    public Vector2 ArrowVector()
    {
        float radian = angle * (Mathf.PI / 180);
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;
    }

    //���̉�]����
    private void ArrowRotate()
    {
        Vector3 rot = transform.eulerAngles + new Vector3(0.0f, 0.0f, -rotate) * rotateSpeed * Time.deltaTime;
        //rot.z = Mathf.Clamp(rot.z, minRotation, maxRotation); //��]�p�x�𐧌�
        transform.eulerAngles = rot;
    }

    //���̉�]�L�[���������Ƃ��̏���
    private void OnArrowRotate(InputAction.CallbackContext context)
    {
        Vector2 rot = context.ReadValue<Vector2>();
        rotate = rot.x;
    }

    //���̉�]�L�[�𗣂����Ƃ��̏���
    private void OnArrowRotateStop(InputAction.CallbackContext context)
    {
        angle = transform.eulerAngles.z + 90;
        rotate = 0;
    }

}
