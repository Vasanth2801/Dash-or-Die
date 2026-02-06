using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] GameObject gameWinPanel;

    void OnEnable()
    {
        CountdownTimer.OnGameEnd += HandleGameEnd;
    }

    void OnDisable()
    {
        CountdownTimer.OnGameEnd -= HandleGameEnd;
    }

    void HandleGameEnd()
    {
        gameWinPanel.SetActive(true);
        Debug.Log("Game Won");
    }
}