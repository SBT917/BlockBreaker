using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboCounter : MonoBehaviour
{
    public int Combo { get; private set; } = 0; //コンボ数

    [SerializeField] private float comboTime = 0.5f; //コンボの継続時間
    [SerializeField] private BlockManager blockManager; //BlockManagerの参照
    
    private TextMeshProUGUI comboText; //コンボ数を表示するテキスト
    private string comboUnit= " COMBO"; //コンボ数の単位

    private Coroutine ConnectCoroutine; //コンボの継続を処理するコルーチン

    void Start()
    {
        TryGetComponent(out comboText);

        foreach(var cell in blockManager.CellList)
        {
            cell.Block.OnBreak += AddCombo;
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
