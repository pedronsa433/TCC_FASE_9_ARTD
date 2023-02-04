using UnityEngine;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private string levelToLoad = "Test";
   public void Play() => LevelLoader.instance.TransitionTo(levelToLoad);

   public void Quit() => Application.Quit();
}
