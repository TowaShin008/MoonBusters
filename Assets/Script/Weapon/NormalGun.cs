using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    public float bulletSpeed = 30.0f;
    const int shotDelayMaxTime = 5;
    private int shotDelayTime = 0;
    [SerializeField]
    private GameObject firingPoint;

    //���ʉ�
    public AudioClip gunSound;
    AudioSource audioSource;

    void Start()
    {
        //���̃R���|�[�l���g�擾
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shotDelayTime > 0)
		{
            shotDelayTime--;
        }
    }
    /// <summary>
    /// �e�̔��ˏ���
    /// </summary>
    /// <param name="arg_firingPoint">�e�̃|�W�V����</param>
    /// <param name="arg_cameraRotation">�J�����̉�]��</param>
    public void Shot(Quaternion arg_cameraRotation)
	{
        if (shotDelayTime <= 0)
        {
            //�e�̉�
            audioSource.PlayOneShot(gunSound);
            //�e�̔��ˏ���
            // �e�𔭎˂���ꏊ���擾
            var bulletPosition = firingPoint.transform.position;
            // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
            GameObject newBall = Instantiate(bullet, bulletPosition, arg_cameraRotation);
            // �o���������{�[����forward(z������)
            var direction = newBall.transform.forward;
            // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
            newBall.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
            // �o���������{�[���̖��O��"bullet"�ɕύX
            newBall.name = bullet.name;
            // �o���������{�[����0.8�b��ɏ���
            Destroy(newBall, 1.5f);

            shotDelayTime = shotDelayMaxTime;
        }
    }
}