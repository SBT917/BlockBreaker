using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�{�[���̓����┽�˂𐧌䂷��X�N���v�g
public class BallController : MonoBehaviour
{
    private Rigidbody rigid; //���W�b�h�{�f�B
    [SerializeField] private float moveSpeed; //�ړ����x

    public event System.Action OnOutOfScreen; //�{�[������ʊO�ɏo���ۂɌĂ΂��C�x���g

    public Vector3 Direction { get { return rigid.velocity.normalized; } set { rigid.velocity = value; } }

    private void OnEnable()
    {
        TryGetComponent(out rigid);
    }

    private void Update()
    {
        //�ړ�
        Vector3 velocity = rigid.velocity;
        rigid.velocity = velocity.normalized * moveSpeed;
    }

    private void OnBecameInvisible()
    {
        //��ʊO�ɏo����j��
        OnOutOfScreen?.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�v���C���[�ɂԂ������ۂ̔��ˏ���
        if (collision.transform.CompareTag("Player"))
        {
            //���������I�u�W�F�N�g�ƃ{�[���̈ʒu���擾
            Vector3 hitPos = collision.transform.position;
            Vector3 ballPos = transform.position;

            //�v���C���[�̒��S���������ɂ��炷
            hitPos.y -= 1.5f;

            //�x�N�g�����擾
            Vector3 vector = ballPos - hitPos;

            rigid.velocity = vector.normalized * moveSpeed;

            return;
        }

        //�j��\�I�u�W�F�N�g�ɂԂ������ۂ̏���
        if (collision.transform.TryGetComponent(out IBreakable breakable))
        {
            breakable.Break(); //�j�󏈗����Ă�
        }

        //���˂̋���
        Vector3 newVector = rigid.velocity;
        if(rigid.velocity.normalized.x > 0.99f) { newVector.x = 0.5f; }
        else if(rigid.velocity.normalized.x < -0.99f) { newVector.x = -0.5f; }
        rigid.velocity = newVector * moveSpeed;
    }
}
