using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // スコアを表示するテキストUI



    private void OnEnable()
    {
        GameManager.Instance.ScoreUpdated += UpdateScoreText;
    }

    private void OnDisable()
    {
        GameManager.Instance.ScoreUpdated -= UpdateScoreText;
    }

    private void UpdateScoreText(int newScore)
    {
        Debug.Log("スコア" + newScore);
        scoreText.text = "Score: " + newScore.ToString();
    }



}
