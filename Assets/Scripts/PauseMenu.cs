using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject ui;

    public void ToggleMenu()
    {
        ui.SetActive(!ui.activeSelf);

        Time.timeScale = ui.activeSelf ? 0f : 1f;
    }
    
    public void Retry()
    {
        LevelLoader.instance.TransitionTo(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Menu() => LevelLoader.instance.TransitionTo("Menu");

    public void Continue() => ToggleMenu();
}
