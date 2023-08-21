using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBreakable
{
    private Color color; //�u���b�N�̐F
    private Material material; //�u���b�N�̃}�e���A��
    private Color[] blockColors = { Color.red, Color.blue, Color.green, Color.yellow }; //�u���b�N�̐F�̃e�[�u��

    public Color Color //�u���b�N�̐F�̃Q�b�^�[
    {
        get { return color; }
    }

    public event System.Action OnBreak; //�u���b�N���j�󂳂ꂽ�Ƃ��ɌĂ΂��C�x���g

    void Awake()
    {
        material = GetComponent<Renderer>().material;

        //�u���b�N�̐F�������_���ɐݒ�
        color = blockColors[Random.Range(0, blockColors.Length)]; 
        material.color = color;
    }

    //�j�󎞏���
    public void Break()
    {
        OnBreak?.Invoke(); //�C�x���g�𔭍s
        Destroy(gameObject);//���g��j�󂷂�
    }
}
