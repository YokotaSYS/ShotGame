using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonController : MonoBehaviour
{
    public GameManager gameManager; // GameManager�ւ̎Q��
    public float distanceFromPlayer = 15f; // �v���C���[����̋���

    private void Start()
    {
        // �v���C���[�̃J�����I�u�W�F�N�g���擾
        GameObject playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // �X�^�[�g�{�^�����v���C���[�̑O�ɔz�u
        transform.position = playerCamera.transform.position + playerCamera.transform.forward * distanceFromPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // �Q�[���X�^�[�g���Ă�
            gameManager.StartGame();
            // StartButton�v���n�u������
            Destroy(gameObject);


           
        }
    }
}
