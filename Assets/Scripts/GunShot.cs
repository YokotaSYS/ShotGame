using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;



public class GunShot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject mazzlePrefab;
    public float bulletSpeed = 10f;
    public Transform shotPoint; // �e�̔��ˈʒu���w�肷��Transform
    public int shotgunBulletCount = 20; // �����ɔ��˂���e�̐�
    public float spreadAngle = 30f; // �e�̍L����p�x
    public float fireCooldown = 0.5f; // �ˌ���̃N�[���_�E������
    public AudioClip shootSound; // ���ˉ�
    public AudioSource audioSource; // AudioSource�R���|�[�l���g�ւ̎Q��


    private List<InputDevice> inputDevices;
    private InputDevice inputDevice;
    private float cooldownTimer = 0f;
    private XRNode controllerNode; // �R���g���[���[�̐ݒ�

    void Awake()
    {
        inputDevices = new List<InputDevice>();

        // �e�̃^�O�Ɋ�Â��ăR���g���[���[��ݒ肷��
        if (gameObject.CompareTag("Right"))
        {
            controllerNode = XRNode.RightHand;
        }
        else if (gameObject.CompareTag("Left"))
        {
            controllerNode = XRNode.LeftHand;
        }
    }

    void Update()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, inputDevices);

        if (inputDevices.Count > 0)
        {
            inputDevice = inputDevices[0];
        }

        cooldownTimer -= Time.deltaTime;

        // �g���K�[�{�^����������A�N�[���_�E�����I��������
        if (inputDevice.isValid && inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isTriggerPressed) && isTriggerPressed && cooldownTimer <= 0f)
        {
           
            for (int i = 0; i < shotgunBulletCount; i++)
            {
                // �e���C���X�^���X��
                GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
                //�}�Y���t���b�V��
                GameObject mazzle = Instantiate(mazzlePrefab, shotPoint.position, shotPoint.rotation);

                // �}�Y���t���b�V���̃T�C�Y��ύX�i��F�X�P�[����0.5�{�ɂ���j
                mazzle.transform.localScale *= 0.5f;

                // �}�Y���t���b�V���𐔕b��ɔj��
                Destroy(mazzle, 3f);

                // Y����90�x��]
                bullet.transform.rotation *= Quaternion.Euler(0, 90, 0);

                // �����_���ȍL����p�x��K�p
                float randomHorizontal = Random.Range(-spreadAngle / 2, spreadAngle / 2);
                float randomVertical = Random.Range(-spreadAngle / 2, spreadAngle / 2);
                bullet.transform.rotation *= Quaternion.Euler(randomVertical, randomHorizontal, 0);

                // �e�ɑO�����ւ̗͂�������
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                if (bulletRigidbody != null)
                {
                    bulletRigidbody.velocity = bullet.transform.forward * bulletSpeed;
                }
                // ���ˉ����Đ�����
                if (audioSource != null && shootSound != null)
                {
                    audioSource.PlayOneShot(shootSound);
                }
                // �e�𐔕b��ɔj��
                Destroy(bullet, 2f);
            }

            // �N�[���_�E���^�C�}�[�����Z�b�g
            cooldownTimer = fireCooldown;
        }
    }

  



}