using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    public Button gameStartButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        gameStartButton.Select();
    }
}
