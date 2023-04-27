using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // シングルトンパターンを使用

    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
    }

}
