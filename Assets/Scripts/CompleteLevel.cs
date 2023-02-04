using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    private int levelToUnlock;
    
    private void OnEnable() => levelToUnlock = SceneManager.GetActiveScene().buildIndex + 1;
    
    public void Continue()
    {
        UnlockLevel();
        LevelLoader.instance.TransitionTo($"Level {levelToUnlock}");
    }

    
    public void Menu()
    {
        UnlockLevel();
        LevelLoader.instance.TransitionTo("Menu");
    }
    
    private void UnlockLevel() => PlayerPrefs.SetInt("levelReached", levelToUnlock);
}
