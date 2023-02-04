using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool GameIsOver;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject completeLevelUI;
    
    private void Start() => GameIsOver = false;
    
    private void Update()
    {
        if (GameIsOver)
            return;
        
        if (PlayerStats.Lives <= 0) 
            EndGame();
    }
    
    private void EndGame()
    {
        GameIsOver = true;
        Debug.Log("Game Over!");
        
        gameOverUI.SetActive(true);
    }
    
    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
