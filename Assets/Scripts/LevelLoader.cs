using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;

    
    private void Start()
    {
        if (instance != null)
            return;

        instance = this;
    }
    
    public void TransitionTo(string sceneName) => StartCoroutine(LoadLevel(sceneName));

    
    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
