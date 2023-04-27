using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;



public class GunShot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject mazzlePrefab;
    public float bulletSpeed = 10f;
    public Transform shotPoint; // 弾の発射位置を指定するTransform
    public int shotgunBulletCount = 20; // 同時に発射する弾の数
    public float spreadAngle = 30f; // 弾の広がり角度
    public float fireCooldown = 0.5f; // 射撃後のクールダウン時間
    public AudioClip shootSound; // 発射音
    public AudioSource audioSource; // AudioSourceコンポーネントへの参照


    private List<InputDevice> inputDevices;
    private InputDevice inputDevice;
    private float cooldownTimer = 0f;
    private XRNode controllerNode; // コントローラーの設定

    void Awake()
    {
        inputDevices = new List<InputDevice>();

        // 銃のタグに基づいてコントローラーを設定する
        if (gameObject.CompareTag("Right"))
        {
            controllerNode = XRNode.RightHand;
        }
        else if (gameObject.CompareTag("Left"))
        {
            controllerNode = XRNode.LeftHand;
        }
    }

    void Update()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, inputDevices);

        if (inputDevices.Count > 0)
        {
            inputDevice = inputDevices[0];
        }

        cooldownTimer -= Time.deltaTime;

        // トリガーボタンが押され、クールダウンが終了したら
        if (inputDevice.isValid && inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isTriggerPressed) && isTriggerPressed && cooldownTimer <= 0f)
        {
           
            for (int i = 0; i < shotgunBulletCount; i++)
            {
                // 弾をインスタンス化
                GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
                //マズルフラッシュ
                GameObject mazzle = Instantiate(mazzlePrefab, shotPoint.position, shotPoint.rotation);

                // マズルフラッシュのサイズを変更（例：スケールを0.5倍にする）
                mazzle.transform.localScale *= 0.5f;

                // マズルフラッシュを数秒後に破棄
                Destroy(mazzle, 3f);

                // Y軸に90度回転
                bullet.transform.rotation *= Quaternion.Euler(0, 90, 0);

                // ランダムな広がり角度を適用
                float randomHorizontal = Random.Range(-spreadAngle / 2, spreadAngle / 2);
                float randomVertical = Random.Range(-spreadAngle / 2, spreadAngle / 2);
                bullet.transform.rotation *= Quaternion.Euler(randomVertical, randomHorizontal, 0);

                // 弾に前方向への力を加える
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                if (bulletRigidbody != null)
                {
                    bulletRigidbody.velocity = bullet.transform.forward * bulletSpeed;
                }
                // 発射音を再生する
                if (audioSource != null && shootSound != null)
                {
                    audioSource.PlayOneShot(shootSound);
                }
                // 弾を数秒後に破棄
                Destroy(bullet, 2f);
            }

            // クールダウンタイマーをリセット
            cooldownTimer = fireCooldown;
        }
    }

  



}