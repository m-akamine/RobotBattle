using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : LiveGunOriginScript
{
    void Start()
    {
        //Cannon�p�Ƀp�����[�^�[��ݒ�
        bulletAmount = 1;
        bulletSpeed = 10;
        fireRate = 1;
        fireRange = 500;
        reloadTime = 5;
        fireEnergyReq = 50;
        reloadEnergyReq = bulletAmount * fireEnergyReq / 3;

        //�e�v���n�u�𑕒e���~2���p��
        unUsedBulletList = BulletInst(bulletAmount);
        usedBulletList = BulletInst(bulletAmount * 5);


        //�e�̃X�N���v�g�擾
        foreach (var list in unUsedBulletList)
        {
            unUsedBulletSCList.Add(list.GetComponent<BulletScript>());
        }
        foreach (var list in usedBulletList)
        {
            usedBulletSCList.Add(list.GetComponent<BulletScript>());
        }
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            StartCoroutine(Fire());
        }
    }

}
