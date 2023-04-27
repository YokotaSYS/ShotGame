using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float limitTime = 60f;
    public TextMeshProUGUI timeText;
    private float currentTime;
    private bool isRunning; // 追加

    private void Start()
    {
        currentTime = limitTime;
        UpdateTimeText();
    }

    private void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
            }
            UpdateTimeText();
        }
    }

    private void UpdateTimeText()
    {
        timeText.text = "Time Limit: " + currentTime.ToString("0");
    }

    // タイマーを開始するメソッド
    public void StartTimer()
    {
        isRunning = true;
    }

    // タイマーを停止するメソッド
    public void StopTimer()
    {
        isRunning = false;
    }

    // タイマーをリセットするメソッド
    public void ResetTimer()
    {
        currentTime = limitTime;
    }
    //タイマーの残り時間を取得するメソッド
    public float GetTimeRemaining()
    {
        return currentTime;
    }


}
