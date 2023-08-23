using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboCounter : Singleton<ComboCounter> //�V���O���g���p��
{
    public int Combo { get; private set; } = 0; //�R���{��

    [SerializeField] private float comboTime = 0.5f; //�R���{�̌p������
    
    private TextMeshProUGUI comboText; //�R���{����\������e�L�X�g
    private string comboUnit= " COMBO"; //�R���{���̒P��

    private Coroutine ConnectCoroutine; //�R���{�̌p������������R���[�`��

    void Start()
    {
        if(TryGetComponent(out comboText))
        {
            //�u���b�N�̔j�󎞂ɃR���{�����Z���鏈����o�^
            foreach (var cell in BlockManager.instance.CellList)
            {
                cell.Block.OnBreak += AddCombo;
            }
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
