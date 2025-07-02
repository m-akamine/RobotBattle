using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBulletScript : MonoBehaviour
{
    // privert�֐�

    // �������������A���^�C���Ō�����������

    float duration = 0;

    Vector3 startPos;
    float distance = 0;

    [HideInInspector] public float usedEnergy;

    Color color;
    ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();

        startPos = transform.position;
    }

    public int EnergyDamege()
    {
        if ((int)(distance / 100) >= 1)
        {
            for (int _ = 0; _ <= (int)(distance / 100); _++)
            {
                DistanceDecay();
            }
        }

        return (int)usedEnergy;
    }

    void Update()
    {
        duration += Time.deltaTime;

        if(usedEnergy <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        distance = (int)(Vector3.Distance(startPos, transform.position));
        Destroy(gameObject);
    }

    //�e�X�g�p�A���������͂ł���
    void OnDestroy()
    {
        //Debug.Log(EnergyDamege());
    }

    // ���������p�̊֐��A�����̂͊m�F�ς�
    void DistanceDecay()
    {
        usedEnergy = (int)(usedEnergy * 0.8f);
        //Debug.Log("�_���[�W�����I");
    }
}
