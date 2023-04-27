using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonController : MonoBehaviour
{
    public GameManager gameManager; // GameManagerへの参照
    public float distanceFromPlayer = 15f; // プレイヤーからの距離

    private void Start()
    {
        // プレイヤーのカメラオブジェクトを取得
        GameObject playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // スタートボタンをプレイヤーの前に配置
        transform.position = playerCamera.transform.position + playerCamera.transform.forward * distanceFromPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // ゲームスタートを呼ぶ
            gameManager.StartGame();
            // StartButtonプレハブを消す
            Destroy(gameObject);


           
        }
    }
}
