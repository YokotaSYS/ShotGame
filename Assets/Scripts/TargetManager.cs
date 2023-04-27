using System.Collections;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject targetPrefab; // �^�[�Q�b�g�̃v���n�u
    public Transform playerTransform; // �v���C���[��Transform
    public float spawnRadius = 20.0f; // �^�[�Q�b�g�������a
    public int maxTargets = 15; // �ő�^�[�Q�b�g��
    public float spawnInterval = 1.0f; // �^�[�Q�b�g�̐����Ԋu�i�b�j

    private bool isSpawning = false; // �^�[�Q�b�g���������ǂ����̃t���O
    private int currentTargets; // ���݂̃^�[�Q�b�g��

    // �^�[�Q�b�g�������J�n����
    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnTargets());
        }
    }

    // �^�[�Q�b�g�������~����
    public void StopSpawning()
    {
        isSpawning = false;
    }

    // �^�[�Q�b�g�𐶐�����R���[�`��
    private IEnumerator SpawnTargets()
    {
        while (isSpawning)
        {
            if (currentTargets < maxTargets)
            {
                Vector3 randomDirection;
                do
                {
                    randomDirection = Random.insideUnitSphere * spawnRadius;
                    randomDirection += playerTransform.position;
                } while (Vector3.Distance(playerTransform.position, randomDirection) < 2f); // 2m�ȓ��̏ꍇ�A�ēx�����_���ȃ|�C���g�𐶐�����

                randomDirection.y = Random.Range(0.5f, 5.0f); // �^�[�Q�b�g�̍�����0����5�͈̔͂Ń����_���ɐݒ�

                GameObject newTarget = Instantiate(targetPrefab, randomDirection, Quaternion.identity);
                // �������ꂽ�^�[�Q�b�g��TargetManager��GameManager�ւ̎Q�Ƃ�ݒ�
                Target targetScript = newTarget.GetComponent<Target>();
                targetScript.targetManager = this;
                targetScript.gameManager = GameManager.Instance;

                currentTargets++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }



    // �^�[�Q�b�g�����Z�b�g����
    public void ResetTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targets)
        {
            Destroy(target);
        }
        currentTargets = 0;
    }

    // �^�[�Q�b�g���j�󂳂ꂽ���Ƃ�ʒm����
    public void NotifyTargetDestroyed()
    {
        currentTargets--;
    }
}