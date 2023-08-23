using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ボールの動きや反射を制御するスクリプト
public class BallController : MonoBehaviour
{
    private Rigidbody rigid; //リジッドボディ
    [SerializeField] private float moveSpeed; //移動速度

    public event System.Action OnOutOfScreen; //ボールが画面外に出た際に呼ばれるイベント

    public Vector3 Direction { get { return rigid.velocity.normalized; } set { rigid.velocity = value; } }

    private void OnEnable()
    {
        TryGetComponent(out rigid);
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
        //プレイヤーにぶつかった際の反射処理
        if (collision.transform.CompareTag("Player"))
        {
            //当たったオブジェクトとボールの位置を取得
            Vector3 hitPos = collision.transform.position;
            Vector3 ballPos = transform.position;

            //プレイヤーの中心を少し下にずらす
            hitPos.y -= 1.5f;

            //ベクトルを取得
            Vector3 vector = ballPos - hitPos;

            rigid.velocity = vector.normalized * moveSpeed;

            return;
        }

        //破壊可能オブジェクトにぶつかった際の処理
        if (collision.transform.TryGetComponent(out IBreakable breakable))
        {
            breakable.Break(); //破壊処理を呼ぶ
        }

        //反射の矯正
        Vector3 newVector = rigid.velocity;
        if(rigid.velocity.normalized.x > 0.99f) { newVector.x = 0.5f; }
        else if(rigid.velocity.normalized.x < -0.99f) { newVector.x = -0.5f; }
        rigid.velocity = newVector * moveSpeed;
    }
}
