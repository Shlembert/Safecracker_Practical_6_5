using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowsController : MonoBehaviour
{
    [SerializeField] private GameObject panelGameWin, panelGameOver;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private GameController gameController;

    public void GameWin()
    {
        SetPanelsState(true, false);
        SetButtonsInteractable(false);
    }

    public void GameOver()
    {
        SetPanelsState(false, true);
        SetButtonsInteractable(false);
    }

    public void GameStart()
    {
        SetPanelsState(false, false);
        SetButtonsInteractable(true);
        gameController.SetLockStartCombine();
    }

    private void SetPanelsState(bool gameWinActive, bool gameOverActive)
    {
        panelGameWin.SetActive(gameWinActive);
        panelGameOver.SetActive(gameOverActive);
    }

    private void SetButtonsInteractable(bool interactable)
    {
        foreach (var button in buttons) button.interactable = interactable;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
