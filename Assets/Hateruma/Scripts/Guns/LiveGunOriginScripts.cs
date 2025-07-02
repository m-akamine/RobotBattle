using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class LiveGunOriginScript : MonoBehaviour
{
    public int bulletAmount;//���e��
    public float fireRate;//���ˑ��x
    public float bulletSpeed;//�e��
    public float fireRange;//�˒�
    public float reloadTime;//�����[�h����
    public int fireEnergyReq;//�K�v�G�l���M�[(1��������)
    public int reloadEnergyReq;

    bool isReload = false;//�����[�h�����ǂ���

    bool isRunningFire = false;//���ˏ����̃R���[�`���������Ă��邩

    [SerializeField] GameObject bulletObj;//�e�̃v���n�u�I�u�W�F�N�g
    public List<GameObject> unUsedBulletList = new List<GameObject>();//�c�e�p���X�g
    public List<GameObject> usedBulletList = new List<GameObject>();//�g�p�ς݂̒e�p���X�g

    public List<BulletScript> unUsedBulletSCList = new List<BulletScript>();//�c�e�̃X�N���v�g�p���X�g
    public List<BulletScript> usedBulletSCList = new List<BulletScript>();//�g�p�ςݒe�̃X�N���v�g�p���X�g

    public EnergyScript energySC;//�G�l���M�[�X�N���v�g
    public CoreScript coreSC;//�R�A�X�N���v�g

    public List<GameObject> BulletInst(int amount)
    {
        var bulletList = new List<GameObject>();
        //�e�v���n�u�𑕒e�����p��
        for (int i = 0; i < amount; i++)
        {
            bulletList.Add(
            Instantiate(
                bulletObj,
                transform.position,
                Quaternion.identity
            ));
        }

        return bulletList;
    }


    //�����ˊ֐�
    public IEnumerator Fire(bool isShotGun = false)
    {

        //�R���[�`���d���h�~
        if (isRunningFire || isReload)
        {
            yield break;
        }

        isRunningFire = true;

        //�c�e������Ό���
        if (unUsedBulletList.Count > 0 && (energySC.UseEnergy(fireEnergyReq)))
        {
            unUsedBulletList[0].transform.position = transform.position;
            unUsedBulletList[0].transform.rotation = transform.rotation;

            //�c�e�̃X�N���v�g��Shot�֐��Ăяo��
            StartCoroutine(unUsedBulletSCList[0].Shot(bulletSpeed, fireRange));

            //�����o���ꂽ�e�Ƃ��̃X�N���v�g���g�p�ς݃��X�g�ɒǉ�
            usedBulletList.Add(unUsedBulletList[0]);
            usedBulletSCList.Add(unUsedBulletSCList[0]);


            //�c�e���X�g����폜
            unUsedBulletList.RemoveAt(0);
            unUsedBulletSCList.RemoveAt(0);

            //�V���b�g�K���̂�
            if (isShotGun)
            {
                var currentAngle = transform.localEulerAngles;//�p�x�ۑ�

                //9���ǉ��Ŕ���
                for (int i = 0; i < 9; i++)
                {
                    //�A���O���������_���ɕς��Ă΂炯������
                    transform.localEulerAngles = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(0f, 360f));
                    unUsedBulletList[0].transform.position = transform.position;
                    unUsedBulletList[0].transform.rotation = transform.rotation;

                    //�c�e�̃X�N���v�g��Shot�֐��Ăяo��
                    StartCoroutine(unUsedBulletSCList[0].Shot(bulletSpeed, fireRange));

                    //�����o���ꂽ�e�Ƃ��̃X�N���v�g���g�p�ς݃��X�g�ɒǉ�
                    usedBulletList.Add(unUsedBulletList[0]);
                    usedBulletSCList.Add(unUsedBulletSCList[0]);


                    //�c�e���X�g����폜
                    unUsedBulletList.RemoveAt(0);
                    unUsedBulletSCList.RemoveAt(0);

                    transform.localEulerAngles = currentAngle;//�p�x��߂�
                }
            }

        }
        else
        {
            //�����[�h���łȂ���΃����[�h
            if (!isReload && (energySC.UseEnergy(reloadEnergyReq)))
            {
                StartCoroutine(Reload());
                isReload = true;
            }
        }
        yield return new WaitForSeconds(1f / fireRate);//���ˊԊu���҂�

        isRunningFire = false;

    }

    //�����[�h�֐�
    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);

        while (unUsedBulletList.Count < bulletAmount && usedBulletList.Count > 0)
        {
            unUsedBulletList.Add(usedBulletList[0]);
            unUsedBulletSCList.Add(usedBulletSCList[0]);

            usedBulletList.RemoveAt(0);
            usedBulletSCList.RemoveAt(0);
        }

        isReload = false;
    }

}
