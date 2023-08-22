using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBreakable
{
    public Color Color { get; private set; } //�u���b�N�̐F
    private Material material; //�u���b�N�̃}�e���A��
    private Color[] blockColors = { Color.red, Color.green, Color.blue}; //�u���b�N�̐F�̃e�[�u��

    public event System.Action OnBreak; //�u���b�N���j�󂳂ꂽ�Ƃ��ɌĂ΂��C�x���g

    private void OnEnable()
    {
        material = GetComponent<Renderer>().material;

        //�u���b�N�̐F�������_���ɐݒ�
        Color = blockColors[Random.Range(0, blockColors.Length)]; 
        material.color = Color;
    }

    //�j�󎞏���
    public void Break()
    {
        OnBreak?.Invoke(); //�C�x���g�𔭍s
        gameObject.SetActive(false); //���g�𖳌��ɂ���
    }
}
