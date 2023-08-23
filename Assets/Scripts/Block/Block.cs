using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBreakable
{
    public Color Color { get; private set; } //�u���b�N�̐F
    private Material material; //�u���b�N�̃}�e���A��
    private Color[] blockColors = { Color.red, Color.green, Color.blue}; //�u���b�N�̐F�̃e�[�u��

    [SerializeField] private ParticleSystem breakParticle; //�u���b�N���j�󂳂ꂽ�Ƃ��ɍĐ�����p�[�e�B�N��

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
        var p = Instantiate(breakParticle, transform.position, Quaternion.identity); //�p�[�e�B�N���𐶐�
        var main = p.main;
        main.startColor = Color; //�p�[�e�B�N���̐F���u���b�N�̐F�Ɠ������̂ɕύX
        AudioManager.instance.PlaySE("BlockBreak"); //SE���Đ�
        gameObject.SetActive(false); //���g�𖳌��ɂ���
    }
}
