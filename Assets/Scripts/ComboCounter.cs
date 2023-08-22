using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboCounter : MonoBehaviour
{
    public int Combo { get; private set; } = 0; //�R���{��

    [SerializeField] private float comboTime = 0.5f; //�R���{�̌p������
    [SerializeField] private BlockManager blockManager; //BlockManager�̎Q��
    
    private TextMeshProUGUI comboText; //�R���{����\������e�L�X�g
    private string comboUnit= " COMBO"; //�R���{���̒P��

    private Coroutine ConnectCoroutine; //�R���{�̌p������������R���[�`��

    void Start()
    {
        TryGetComponent(out comboText);

        foreach(var cell in blockManager.CellList)
        {
            cell.Block.OnBreak += AddCombo;
        }
    }

    //�R���{�̉��Z
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

    //�R���{�̃��Z�b�g
    private void ResetCombo()
    {
        Combo = 0;
        comboText.text = Combo.ToString() + comboUnit;
    }

    //�R���{�̌p�����Ԃ��v������R���[�`��
    private IEnumerator ComboConnectCountDownCoroutine()
    {
        yield return new WaitForSeconds(comboTime);
        ResetCombo();
    }
}
