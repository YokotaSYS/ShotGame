using UnityEngine;


public class Bullet : MonoBehaviour
{
    public GameObject sparkPrefab;



    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {


            // パーティクルシステムを衝突位置にインスタンス化
            GameObject sparkInstance = Instantiate(sparkPrefab, transform.position, Quaternion.identity);

            // 衝突の法線に沿ってパーティクルシステムを回転（トリガーイベントでは法線情報がないため、代わりに弾の前方ベクトルを使用）
            sparkInstance.transform.rotation = Quaternion.LookRotation(-other.transform.forward);

            // パーティクルが終わったら自動的に削除する
            Destroy(sparkInstance, sparkInstance.GetComponent<ParticleSystem>().main.duration);
        
    }


}
