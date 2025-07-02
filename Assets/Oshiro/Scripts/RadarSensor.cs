using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSensorObj
{

    protected GameObject targetObj = null;//�^�[�Q�b�g�̃I�u�W�F�N�g

    public RadarSensorObj(GameObject lockOnObj)
    {
        this.targetObj = lockOnObj;
    }

    /// <summary>
    /// �^�[�Q�b�g�̈ʒu���擾
    /// </summary>
    /// <returns>�^�[�Q�b�g�̈ʒu</returns>
    public Vector3 GetPos()
    {
        return targetObj.transform.position;
    }

    /// <summary>
    /// �@�̂��ǂ����𒲂ׂ�
    /// </summary>
    /// <returns>�@�̂ł����true</returns>
    public bool IsEnemy()
    {
        return targetObj.CompareTag("Player");
    }

    /// <summary>
    /// �e���ǂ����𒲂ׂ�
    /// </summary>
    /// <returns>�e�ł����true</returns>
    public bool IsBullet()
    {
        List<string> tags = new() { "Bullet", "Energy", "Missile" };
        return tags.Contains(targetObj.tag);
    }

    /// <summary>
    /// �^�[�Q�b�g�̃^�O���擾
    /// </summary>
    /// <returns>�^�[�Q�b�g�̃^�O</returns>
    public string GetTag()
    {
        return targetObj.tag;
    }

    /// <summary>
    /// �^�[�Q�b�g�I�u�W�F�N�g�̔�r
    /// </summary>
    /// <param name="lossObj">��r�Ώ�</param>
    /// <returns>�^�[�Q�b�g�ƈ�������v����Ȃ�true</returns>
    public bool EqualGameObj(GameObject lossObj)
    {
        return targetObj == lossObj;
    }
}

public class RadarSensor : MonoBehaviour
{

    private List<RadarSensorObj> targets = new();

    public CoreScript coreScript;

    public List<string> tags = new();

    public float sensorSize = 1;

    private void Awake()
    {
        transform.localScale = Vector3.one * sensorSize;//�T�C�Y��ݒ�
        transform.localPosition = Vector3.zero;//�|�W�V������������
    }

    private void OnTriggerEnter(Collider other)
    {

        //�ݒ肵���^�O�ɃI�u�W�F�N�g���܂܂�Ă�����
        if (tags.Contains(other.gameObject.tag))
        {
            //�^�[�Q�b�g���X�g�Ɋ܂܂�A�{�̂̃X�N���v�g�ɓ`����
            targets.Add(new RadarSensorObj(other.gameObject));
            coreScript.OnRadarSensor(targets, isEnter: true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        //�ݒ肵���^�O�ɃI�u�W�F�N�g���܂܂�Ă�����
        if (tags.Contains(other.gameObject.tag))
        {

            //�^�[�Q�b�g���X�g���珜�O���A�{�̂̃X�N���v�g�ɓ`����
            targets.RemoveAt(targets.FindIndex(x => x.EqualGameObj(other.gameObject)));
            coreScript.OnRadarSensor(targets, isEnter: false);
        }
    }
}
