using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunScript : LiveGunOriginScript
{
    void Start()
    {
        //ShotGun�p�Ƀp�����[�^�[��ݒ�
        bulletAmount = 50;
        bulletSpeed = 13;
        fireRate = 1;
        fireRange = 100;
        reloadTime = 3;
        fireEnergyReq = 7;
        reloadEnergyReq = (bulletAmount / 10) * fireEnergyReq / 3;

        //�e�v���n�u�𑕒e���~2���p��
        unUsedBulletList = BulletInst(bulletAmount);
        usedBulletList = BulletInst(bulletAmount * 2);


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
            StartCoroutine(Fire(true));
        }
    }
}
