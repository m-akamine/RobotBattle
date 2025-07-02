using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacineGunScript : LiveGunOriginScript
{

    void Start()
    {
        //MacineGun�p�Ƀp�����[�^�[��ݒ�
        bulletAmount = 30;
        bulletSpeed = 15;
        fireRate = 6;
        fireRange = 450;
        reloadTime = 2;
        fireEnergyReq = 2;
        reloadEnergyReq = bulletAmount * fireEnergyReq / 3;

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
            StartCoroutine(Fire());
        }
    }

}
