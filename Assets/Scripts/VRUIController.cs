using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRUIController : MonoBehaviour
{
    public Button startButton;
    public Button scoreButton;
    public Text scoreText;
    public XRNode controllerNode = XRNode.RightHand;
    private InputDevice inputDevice;
    public Vector3 defaultButtonScale = new Vector3(5, 5, 5);
    public Vector3 selectedButtonScale = new Vector3(5.2f, 5.2f, 5.2f);
    public GameManager gameManager; // GameManagerへの参照を追加
    public Button retryButton;//リトライボタン
    public Canvas mainMenuCanvas;//メインメニュー
    private float inputDisableTimer = 0f; // 入力無効タイマー
    private bool inputDisabled = false; // 入力無効フラグ
    private bool wasTriggerPressed = false;
    [SerializeField]
    private Button toggleARVRButton;
    private bool isTriggerReleased = true;

    private EventSystem eventSystem;
    private Button selectedButton;//選択しているボタン



    void Start()
    {
        eventSystem = EventSystem.current;
        
    }


    void Update()
    {
        // 入力無効タイマーを処理
        if (inputDisabled)
        {
            inputDisableTimer -= Time.deltaTime;
            if (inputDisableTimer <= 0f)
            {
                inputDisabled = false;
            }
        }

        inputDevice = InputDevices.GetDeviceAtXRNode(controllerNode);
        Vector2 axisValue;
        inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out axisValue);
        float stickY = axisValue.y;

        if (stickY > 0.5f && selectedButton != startButton)
        {
            selectedButton = startButton;
            selectedButton.Select();
            startButton.transform.localScale = selectedButtonScale;
            scoreButton.transform.localScale = defaultButtonScale;
        }
        else if (stickY < -0.5f && selectedButton != scoreButton)
        {
            selectedButton = scoreButton;
            selectedButton.Select();
            scoreButton.transform.localScale = selectedButtonScale;
            startButton.transform.localScale = defaultButtonScale;
        }

        bool isPressed;
        inputDevice.IsPressed(InputHelpers.Button.PrimaryButton, out isPressed, 0.1f);
        bool isTriggerPressed = false;
        inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out isTriggerPressed);

        // トリガーがリリースされたときに isTriggerReleased を true に設定
        if (!isTriggerPressed)
        {
            isTriggerReleased = true;
        }

        if (isTriggerPressed && isTriggerReleased)
        {
            isTriggerReleased = false;

            // ここで AR/VR 切り替え処理を呼び出す
            gameManager.ToggleARVR();
        }


        // トリガーがリリースされたら、ボタン処理を実行する
        if (!inputDisabled && (gameManager.CurrentGameState == GameState.StartMenu || gameManager.CurrentGameState == GameState.Result) && selectedButton != null && isTriggerPressed && !wasTriggerPressed)
        {
            if (selectedButton == startButton)
            {
                OnStartButtonClicked();
            }
            else if (selectedButton == scoreButton)
            {
                OnScoreButtonClicked();
            }
            else if (selectedButton == retryButton)
            {
                OnRetryButtonClicked();
            }
        }

        // トリガーの状態を更新する
        wasTriggerPressed = isTriggerPressed;
    }



    public void OnStartButtonClicked()
    {
        GameManager.Instance.StartGame(); // ゲームを開始する
        mainMenuCanvas.gameObject.SetActive(false); // メインメニューのCanvasを非表示にする
    }

    public void OnScoreButtonClicked()
    {

        StartCoroutine(HideScoreText());

        //gameObject.SetActive(false);
    }

    public void OnRetryButtonClicked()
    {
        gameManager.RestartGame();
    }

    private void DisableInputForSeconds(float seconds)
    {
        inputDisableTimer = seconds;
        inputDisabled = true;
        DisableInputForSeconds(2f);

    }
    public void OnARVRButtonClicked()
    {
        gameManager.ToggleARVR();
    }


    private IEnumerator HideScoreText()
    {
        yield return new WaitForSeconds(3f);
        scoreText.text = "";
    }
}
