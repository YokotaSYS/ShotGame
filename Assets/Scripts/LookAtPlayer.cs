using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // プレイヤーオブジェクトのTransformコンポーネントへの参照

    void Start()
    {
        // タグが"Player"であるオブジェクトを検索して、そのTransformコンポーネントを取得します。
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        // オブジェクトがプレイヤーの方向を向くように回転
        transform.LookAt(player);
    }
}
