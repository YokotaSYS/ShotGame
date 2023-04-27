using System.Collections;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject targetPrefab; // ターゲットのプレハブ
    public Transform playerTransform; // プレイヤーのTransform
    public float spawnRadius = 20.0f; // ターゲット生成半径
    public int maxTargets = 15; // 最大ターゲット数
    public float spawnInterval = 1.0f; // ターゲットの生成間隔（秒）

    private bool isSpawning = false; // ターゲット生成中かどうかのフラグ
    private int currentTargets; // 現在のターゲット数

    // ターゲット生成を開始する
    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnTargets());
        }
    }

    // ターゲット生成を停止する
    public void StopSpawning()
    {
        isSpawning = false;
    }

    // ターゲットを生成するコルーチン
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
                } while (Vector3.Distance(playerTransform.position, randomDirection) < 2f); // 2m以内の場合、再度ランダムなポイントを生成する

                randomDirection.y = Random.Range(0.5f, 5.0f); // ターゲットの高さを0から5の範囲でランダムに設定

                GameObject newTarget = Instantiate(targetPrefab, randomDirection, Quaternion.identity);
                // 生成されたターゲットにTargetManagerとGameManagerへの参照を設定
                Target targetScript = newTarget.GetComponent<Target>();
                targetScript.targetManager = this;
                targetScript.gameManager = GameManager.Instance;

                currentTargets++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }



    // ターゲットをリセットする
    public void ResetTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targets)
        {
            Destroy(target);
        }
        currentTargets = 0;
    }

    // ターゲットが破壊されたことを通知する
    public void NotifyTargetDestroyed()
    {
        currentTargets--;
    }
}