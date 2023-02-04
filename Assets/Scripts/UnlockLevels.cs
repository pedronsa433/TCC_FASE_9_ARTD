using UnityEngine;

public class UnlockLevels : MonoBehaviour
{
    [SerializeField] private int LevelCount = 3;
    public void UnlockAllLevels() => PlayerPrefs.SetInt("levelReached", LevelCount);
}
