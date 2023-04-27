using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public int scoreValue = 1; // ターゲット破壊時のスコア
    public TargetManager targetManager; // ターゲットマネージャーへの参照を追加
    public GameManager gameManager; // ゲームマネージャーへの参照を追加

    private void OnTriggerEnter(Collider other)
    {
        // 他のオブジェクトがBallタグを持っているかチェック
        if (other.CompareTag("Ball"))
        {
            // ターゲットが破壊されたときにGameManagerに通知する
            gameManager.AddScore(scoreValue);

            // ターゲットが破壊されたときにTargetManagerに通知する
            targetManager.NotifyTargetDestroyed();

            Destroy(gameObject);
        }
    }
}
