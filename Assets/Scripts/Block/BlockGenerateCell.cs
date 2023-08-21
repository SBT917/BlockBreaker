using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�u���b�N�̐������s���N���X(��{�I�Ƀu���b�N�͂����ʂ��Đ��������)
public class BlockGenerateCell : MonoBehaviour
{
    [SerializeField] private Block blockPrefab; //�u���b�N�̃v���t�@�u

    private Block block; //�������ꂽ�u���b�N
    private bool isBlockExistind; //�u���b�N�����݂��邩�ǂ���
    private Vector2Int cellNum; //�Z���̔ԍ�

    public Block Block //�u���b�N�̃v���p�e�B
    {
        get { return block; }
    }

    public bool IsBlockExisting //�u���b�N�����݂��邩�ǂ����̃v���p�e�B
    {
        get { return isBlockExistind; }
    }

    public Vector2Int CellNum //�Z���̔ԍ��̃v���p�e�B
    {
        get { return cellNum; }
        set { cellNum = value; }
    }

    void Awake()
    {
        GenerateBlock();
    }

    //�u���b�N�̐���
    private void GenerateBlock()
    {
        block = Instantiate(blockPrefab, transform);

        //�u���b�N���j�󂳂ꂽ�Ƃ��̏�����o�^
        block.OnBreak += () =>
        {
            isBlockExistind = false;
            //StartCoroutine(RegenerateCoroutine());
        };

        isBlockExistind = true;
    }

    //�u���b�N�̍Đ����R���[�`��
    private IEnumerator RegenerateCoroutine()
    {
        Debug.Log("�Đ�����");
        yield return (new WaitForSeconds(10.0f));
        GenerateBlock();
    }
}
