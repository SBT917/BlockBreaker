using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�u���b�N�̐����A�Đ������s���N���X
public class BlockCell : MonoBehaviour
{
    [SerializeField] private Block blockPrefab; //�u���b�N�̃v���t�@�u

    public Block Block { get; private set; } //�������ꂽ�u���b�N
    public bool IsBlockExisting { get; private set; } //�u���b�N�����݂��邩�ǂ���
    public Vector2Int CellNum { get; set; } //�Z���̔ԍ�

    void Awake()
    {
        GenerateBlock();     
    }

    //�u���b�N�̐���
    private void GenerateBlock()
    {
        Block = Instantiate(blockPrefab, transform);

        //�u���b�N���j�󂳂ꂽ�Ƃ��̏�����o�^
        Block.OnBreak += () =>
        {
            IsBlockExisting = false;
            StartCoroutine(RegenerateCoroutine());
        };

        IsBlockExisting = true;
    }

    //�u���b�N�̍Đ����R���[�`��
    private IEnumerator RegenerateCoroutine()
    {
        yield return (new WaitForSeconds(15.0f));
        Block.gameObject.SetActive(true);
        IsBlockExisting = true;
    }
}
