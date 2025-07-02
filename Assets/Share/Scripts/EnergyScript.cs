using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyScript : MonoBehaviour
{
    [SerializeField, Header("���b�̑�����")]
    float incAmount = 1.0f;
    [SerializeField, Header("�G�l���M�[�̍ő�e��")]
    float maxAmount = 1000f;
    //���݂̃G�l���M�[��
    public float energyAmount = 0;

    void Start()
    {
        IncEnergy();
    }

    void Update()
    {

    }

    //��莞�Ԃ��ƂɃG�l���M�[�ʂ𑝂₷
    void IncEnergy()
    {
        energyAmount += incAmount;
        
        if (energyAmount > maxAmount)//�ő�ʂ𒴂���Ƃ�
        {
            energyAmount = maxAmount;
        }
        
        Invoke("IncEnergy", 1);
    }

    //�G�l���M�[����֐�
    public bool UseEnergy(float useAmount)//�֐����Ăԑ��ŏ���ʂ��w��
    {
        if (energyAmount - useAmount >= 0)//�K�v�G�l���M�[������Ƃ�
        {
            energyAmount -= useAmount;

            return true;
        }
        else //�Ȃ��Ƃ�
        {
            return false;
        }
    }
}
