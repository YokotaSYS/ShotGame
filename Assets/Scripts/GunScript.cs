using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject prefab;
    private GameObject instance;

    void Start()
    {
        // 銃のプレハブをインスタンス化
        instance = Instantiate(prefab, transform.position, transform.rotation);

        // Y軸に90度回転
        instance.transform.rotation *= Quaternion.Euler(0, 90, 12);

        // アタッチしたゲームオブジェクトを子に設定
        instance.transform.SetParent(transform);
    }
}
