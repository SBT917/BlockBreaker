using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboCounter : Singleton<ComboCounter> //シングルトン継承
{
    public int Combo { get; private set; } = 0; //コンボ数

    [SerializeField] private float comboTime = 0.5f; //コンボの継続時間
    
    private TextMeshProUGUI comboText; //コンボ数を表示するテキスト
    private string comboUnit= " COMBO"; //コンボ数の単位

    private Coroutine ConnectCoroutine; //コンボの継続を処理するコルーチン

    void Start()
    {
        if(TryGetComponent(out comboText))
        {
            //ブロックの破壊時にコンボを加算する処理を登録
            foreach (var cell in BlockManager.instance.CellList)
            {
                cell.Block.OnBreak += AddCombo;
            }
        }
    }

    //コンボの加算
    private void AddCombo()
    {
        Combo++;
        comboText.text = Combo.ToString() + comboUnit;

        if(ConnectCoroutine != null)
        {
            StopCoroutine(ConnectCoroutine);
        }
        ConnectCoroutine = StartCoroutine(ComboConnectCountDownCoroutine());
    }

    //コンボのリセット
    private void ResetCombo()
    {
        Combo = 0;
        comboText.text = Combo.ToString() + comboUnit;
    }

    //コンボの継続時間を計測するコルーチン
    private IEnumerator ComboConnectCountDownCoroutine()
    {
        yield return new WaitForSeconds(comboTime);
        ResetCombo();
    }
}
