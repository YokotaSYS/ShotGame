using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public int scoreValue = 1; // �^�[�Q�b�g�j�󎞂̃X�R�A
    public TargetManager targetManager; // �^�[�Q�b�g�}�l�[�W���[�ւ̎Q�Ƃ�ǉ�
    public GameManager gameManager; // �Q�[���}�l�[�W���[�ւ̎Q�Ƃ�ǉ�

    private void OnTriggerEnter(Collider other)
    {
        // ���̃I�u�W�F�N�g��Ball�^�O�������Ă��邩�`�F�b�N
        if (other.CompareTag("Ball"))
        {
            // �^�[�Q�b�g���j�󂳂ꂽ�Ƃ���GameManager�ɒʒm����
            gameManager.AddScore(scoreValue);

            // �^�[�Q�b�g���j�󂳂ꂽ�Ƃ���TargetManager�ɒʒm����
            targetManager.NotifyTargetDestroyed();

            Destroy(gameObject);
        }
    }
}
