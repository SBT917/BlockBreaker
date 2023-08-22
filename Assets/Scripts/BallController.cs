using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//ボールの動きや反射を制御するスクリプト
public class BallController : MonoBehaviour
{
    private Rigidbody rigid; //リジッドボディ
    [SerializeField] private float moveSpeed; //移動速度

    public event System.Action OnOutOfScreen; //ボールが画面外に出た際に呼ばれるイベント

    private void Start()
    {
        TryGetComponent(out rigid);
        rigid.velocity = new Vector3(0.0f, -1.0f, 0.0f) * moveSpeed; //初速を与える
    }

    private void Update()
    {
        //移動
        Vector3 velocity = rigid.velocity;
        rigid.velocity = velocity.normalized * moveSpeed;
    }

    private void OnBecameInvisible()
    {
        //画面外に出たら破棄
        OnOutOfScreen?.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //破壊可能オブジェクトにぶつかった際の処理
        if(collision.transform.TryGetComponent(out IBreakable breakable))
        {
            breakable.Break(); //破壊処理を呼ぶ
        }

        //プレイヤーにぶつかった際の反射処理を変更
        if (collision.transform.CompareTag("Player"))
        {
            //プレイヤーとボールの位置を取得
            Vector3 hitPos = collision.transform.position;
            Vector3 ballPos = transform.position;

            //プレイヤーとボールのベクトルを取得
            Vector3 vector = ballPos - hitPos;
            vector.Normalize();

            //ボールの反射
            rigid.velocity = vector * moveSpeed;
        }
    }
}
