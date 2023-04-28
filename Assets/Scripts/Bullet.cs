using UnityEngine;


public class Bullet : MonoBehaviour
{
    public GameObject sparkPrefab;



    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {


            // �p�[�e�B�N���V�X�e�����Փˈʒu�ɃC���X�^���X��
            GameObject sparkInstance = Instantiate(sparkPrefab, transform.position, Quaternion.identity);

            // �Փ˂̖@���ɉ����ăp�[�e�B�N���V�X�e������]�i�g���K�[�C�x���g�ł͖@����񂪂Ȃ����߁A����ɒe�̑O���x�N�g�����g�p�j
            sparkInstance.transform.rotation = Quaternion.LookRotation(-other.transform.forward);

            // �p�[�e�B�N�����I������玩���I�ɍ폜����
            Destroy(sparkInstance, sparkInstance.GetComponent<ParticleSystem>().main.duration);
        
    }


}
