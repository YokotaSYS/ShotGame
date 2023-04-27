using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject prefab;
    private GameObject instance;

    void Start()
    {
        // �e�̃v���n�u���C���X�^���X��
        instance = Instantiate(prefab, transform.position, transform.rotation);

        // Y����90�x��]
        instance.transform.rotation *= Quaternion.Euler(0, 90, 12);

        // �A�^�b�`�����Q�[���I�u�W�F�N�g���q�ɐݒ�
        instance.transform.SetParent(transform);
    }
}
