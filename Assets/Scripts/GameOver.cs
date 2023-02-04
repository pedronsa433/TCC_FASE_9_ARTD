using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Retry() => LevelLoader.instance.TransitionTo(SceneManager.GetActiveScene().name);
    
    public void Menu() => LevelLoader.instance.TransitionTo("Menu");
}

