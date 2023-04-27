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
    public GameObject vrPre; // 追加: VRPreオブジェクトへの参照を保持する変数
    public static GameManager Instance { get; private set; }
    private bool canReceiveInput = true; // 追加: 入力を受け付けるかどうかを管理する変数
    public GameState CurrentGameState
    {
        get { return gameState; }
    }


    void Start()
    {
        gameState = GameState.StartMenu;
        vrPre = GameObject.Find("VRPre"); // 追加: VRPreオブジェクトを見つける
    }


    void Update()
    {
        if (!canReceiveInput) return; // 追加: 入力が受け付けられない場合、Updateメソッドを終了する

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
                // ここでのリトライボタンの選択処理は削除
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
        resultCanvas.enabled = false; // この行を追加
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
        StartCoroutine(WaitForInput()); // 追加: 入力を一定時間無視するコルーチンを開始する
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

    // 追加: 入力を一定時間無視するコルーチン
    private IEnumerator WaitForInput()
    {
        canReceiveInput = false;
        yield return new WaitForSeconds(2f);
        canReceiveInput = true;
    }

    public void OnStartButtonClicked()
    {
        GameManager.Instance.StartGame(); // ゲームを開始する
        gameObject.SetActive(false); // UIを非表示にする
    }

    public void OnScoreButtonClicked()
    {
        Debug.Log("Score Button Clicked"); // デバッグログを追加
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
            Debug.Log("VRモード");
            Camera.main.clearFlags = CameraClearFlags.Skybox;

            if (vrPre != null)
            {
                vrPre.SetActive(true);
            }
        }
        else
        {
            Debug.Log("ARモード");
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
