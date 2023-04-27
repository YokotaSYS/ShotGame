using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float limitTime = 60f;
    public TextMeshProUGUI timeText;
    private float currentTime;
    private bool isRunning; // �ǉ�

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

    // �^�C�}�[���J�n���郁�\�b�h
    public void StartTimer()
    {
        isRunning = true;
    }

    // �^�C�}�[���~���郁�\�b�h
    public void StopTimer()
    {
        isRunning = false;
    }

    // �^�C�}�[�����Z�b�g���郁�\�b�h
    public void ResetTimer()
    {
        currentTime = limitTime;
    }
    //�^�C�}�[�̎c�莞�Ԃ��擾���郁�\�b�h
    public float GetTimeRemaining()
    {
        return currentTime;
    }


}
