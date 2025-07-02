using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMoveScript : MonoBehaviour
{
    public CoreScript coreScript;

    [SerializeField, Header("�G�l���M�[�Ǘ��X�N���v�g")]
    EnergyScript energyScript;

    [SerializeField]
    GameObject sphere;//�e�X�g�p

    //���{�b�g��Rigidbody
    Rigidbody robotRB;
    
    [SerializeField, Header("�W�����v��")]
    float jumpForce = 1.0f;
    [SerializeField, Header("�ړ���")]
    float moveForce = 1.0f;
    [SerializeField, Header("��]��")]
    float rotateForce = 1.0f;
    [SerializeField, Header("�ō����x")]
    float maxSpeed = 1.0f;

    //��]����bool
    bool isRotate = false;

    void Start()
    {
        robotRB = GetComponent<Rigidbody>();

        SetMass();

        //�e�X�g�p
        StartCoroutine(MoveTarget(sphere));
    }

    void Update()
    {
        //�e�X�g�p
        if (Input.GetKeyDown(KeyCode.Space)) MoveUp();
    }

    //�@�̂̎��ʂ�ݒ�
    void SetMass()
    {
        //CoreScript�̏d�ʕϐ������{�b�g�̎��ʂƂ���
        robotRB.mass = coreScript.weight;
    }

    public void MoveUp()//�W�����v
    {
        if (energyScript.UseEnergy(1.0f))
        {
            robotRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    //�^�[�Q�b�g�ւ̈ړ�
    public IEnumerator MoveTarget(GameObject targetOBJ)
    {
        //�����͌�X�ύX
        while (Mathf.Abs(targetOBJ.transform.position.x - transform.position.x) > 1
            || Mathf.Abs(targetOBJ.transform.position.z - transform.position.z) > 1)
        {
            if (energyScript.UseEnergy(0.01f))
            {
                Vector3 direction = targetOBJ.transform.position - transform.position;

                //�������瑊��̕�����AddForce
                robotRB.AddForce(direction.normalized * moveForce, ForceMode.Force);

                //���x�`�F�b�N
                CheckVelocity();
                
                //��]���łȂ��Ƃ�
                if (!isRotate)
                {
                    isRotate = true;

                    StartCoroutine(RotateRobot(targetOBJ));
                }
            }  

            yield return null;
        }

        robotRB.velocity = Vector3.zero;
    }

    //�ړ����x�̒���
    void CheckVelocity()
    {
        //���{�b�g��xy���ʂ̑��x���v�Z
        float speed = Mathf.Sqrt(Mathf.Pow(robotRB.velocity.x, 2) + Mathf.Pow(robotRB.velocity.z, 2));
        
        //�ړ����x����`�F�b�N
        if (speed > maxSpeed)
        {
            robotRB.velocity = new Vector3(
                robotRB.velocity.x / (speed / maxSpeed),
                robotRB.velocity.y,
                robotRB.velocity.z / (speed / maxSpeed)
                );
        }
    }

    //���{�b�g�̉�]
    IEnumerator RotateRobot(GameObject targetOBJ)
    {
        while (Vector3.Angle(transform.forward, targetOBJ.transform.position - transform.position) > 10)
        {
            if (energyScript.UseEnergy(0.01f))
            {
                Vector3 direction = targetOBJ.transform.position - transform.position;

                float angle = Vector3.SignedAngle(transform.forward, direction, transform.up);

                //�p�x�����������ɉ�]
                robotRB.AddTorque(direction * -Mathf.Sign(angle) * rotateForce, ForceMode.Force);
            }

            yield return null;
        }

        robotRB.angularVelocity = Vector3.zero;

        isRotate = false;
    }
}
