using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//�{�[���̓����┽�˂𐧌䂷��X�N���v�g
public class BallController : MonoBehaviour
{
    private Rigidbody rigid; //���W�b�h�{�f�B
    [SerializeField] private float moveSpeed; //�ړ����x

    public event System.Action OnOutOfScreen; //�{�[������ʊO�ɏo���ۂɌĂ΂��C�x���g

    private void Start()
    {
        TryGetComponent(out rigid);
        rigid.velocity = new Vector3(0.0f, -1.0f, 0.0f) * moveSpeed; //������^����
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
        //�j��\�I�u�W�F�N�g�ɂԂ������ۂ̏���
        if(collision.transform.TryGetComponent(out IBreakable breakable))
        {
            breakable.Break(); //�j�󏈗����Ă�
        }

        //�v���C���[�ɂԂ������ۂ̔��ˏ�����ύX
        if (collision.transform.CompareTag("Player"))
        {
            //�v���C���[�ƃ{�[���̈ʒu���擾
            Vector3 hitPos = collision.transform.position;
            Vector3 ballPos = transform.position;

            //�v���C���[�ƃ{�[���̃x�N�g�����擾
            Vector3 vector = ballPos - hitPos;
            vector.Normalize();

            //�{�[���̔���
            rigid.velocity = vector * moveSpeed;
        }
    }
}
