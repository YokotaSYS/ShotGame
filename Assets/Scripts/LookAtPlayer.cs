using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // �v���C���[�I�u�W�F�N�g��Transform�R���|�[�l���g�ւ̎Q��

    void Start()
    {
        // �^�O��"Player"�ł���I�u�W�F�N�g���������āA����Transform�R���|�[�l���g���擾���܂��B
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        // �I�u�W�F�N�g���v���C���[�̕����������悤�ɉ�]
        transform.LookAt(player);
    }
}
