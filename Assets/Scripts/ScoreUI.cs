using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // �X�R�A��\������e�L�X�gUI



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
        Debug.Log("�X�R�A" + newScore);
        scoreText.text = "Score: " + newScore.ToString();
    }



}
