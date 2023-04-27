using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;



public class GameManager : MonoBehaviour
{
    public GameTimer gameTimer;
    public TargetManager targetManager;
    public Text scoreText;
    public event Action<int> ScoreUpdated;
    public Canvas resultCanvas;
    public Text resultScoreText;
    private GameState gameState;
    public int score = 0;
    public bool disableModeSwitch = false;
    public GameObject vrPre; // �ǉ�: VRPre�I�u�W�F�N�g�ւ̎Q�Ƃ�ێ�����ϐ�
    public static GameManager Instance { get; private set; }
    private bool canReceiveInput = true; // �ǉ�: ���͂��󂯕t���邩�ǂ������Ǘ�����ϐ�
    public GameState CurrentGameState
    {
        get { return gameState; }
    }


    void Start()
    {
        gameState = GameState.StartMenu;
        vrPre = GameObject.Find("VRPre"); // �ǉ�: VRPre�I�u�W�F�N�g��������
    }


    void Update()
    {
        if (!canReceiveInput) return; // �ǉ�: ���͂��󂯕t�����Ȃ��ꍇ�AUpdate���\�b�h���I������

        switch (gameState)
        {
            case GameState.StartMenu:
                break;
            case GameState.Playing:
                if (gameTimer.GetTimeRemaining() <= 0f)
                {
                    EndGame();
                }
                break;
            case GameState.Result:
                // �����ł̃��g���C�{�^���̑I�������͍폜
                break;
        }
    }


    public void StartGame()
    {
        disableModeSwitch = true;

        gameState = GameState.Playing;
        gameTimer.ResetTimer();
        gameTimer.StartTimer();
        targetManager.StartSpawning();
        score = 0;
        ScoreUpdated?.Invoke(score);
        resultCanvas.enabled = false; // ���̍s��ǉ�
    }

    public void EndGame()
    {
        disableModeSwitch = false;

        gameState = GameState.Result;
        gameTimer.StopTimer();
        targetManager.ResetTargets();
        targetManager.StopSpawning();
        resultCanvas.gameObject.SetActive(true);
        resultScoreText.text = "Score: " + score;
        resultCanvas.enabled = true;
        StartCoroutine(WaitForInput()); // �ǉ�: ���͂���莞�Ԗ�������R���[�`�����J�n����
    }

    public void RestartGame()
    {
        resultCanvas.gameObject.SetActive(false);
        StartGame();
    }

    public void AddScore(int value)
    {
        score += value;
        ScoreUpdated?.Invoke(score);
    }

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

    // �ǉ�: ���͂���莞�Ԗ�������R���[�`��
    private IEnumerator WaitForInput()
    {
        canReceiveInput = false;
        yield return new WaitForSeconds(2f);
        canReceiveInput = true;
    }

    public void OnStartButtonClicked()
    {
        GameManager.Instance.StartGame(); // �Q�[�����J�n����
        gameObject.SetActive(false); // UI���\���ɂ���
    }

    public void OnScoreButtonClicked()
    {
        Debug.Log("Score Button Clicked"); // �f�o�b�O���O��ǉ�
        scoreText.text = "Test Score!";
    }


    public void ToggleARVR()
    {
        if (disableModeSwitch)
        {
            return;
        }

        if (Camera.main.clearFlags == CameraClearFlags.SolidColor)
        {
            Debug.Log("VR���[�h");
            Camera.main.clearFlags = CameraClearFlags.Skybox;

            if (vrPre != null)
            {
                vrPre.SetActive(true);
            }
        }
        else
        {
            Debug.Log("AR���[�h");
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            Camera.main.backgroundColor = new Color(0, 0, 0, 0);

            if (vrPre != null)
            {
                vrPre.SetActive(false);
            }
        }
    }






}

public enum GameState
{
    StartMenu,
    Playing,
    Result
}
