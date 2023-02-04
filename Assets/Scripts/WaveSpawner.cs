using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private TMP_Text waveCountdownText;
    [SerializeField] private GameManager gameManager;
    
    
    public static int enemiesAlive = 0;
    public float countdown = 10.5f;
    
    private int waveIndex = 0;
    

    private void Update()
    {
        if (enemiesAlive > 0)
            return;
        
        if (waveIndex == waves.Length)
        {
            Debug.Log("LEVEL WON!");
            gameManager.WinLevel();
            this.enabled = false;
        }
        
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }
    
    private IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        enemiesAlive = wave.count;
        
        Debug.Log($"Wave #{waveIndex} Incoming!");
        
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);
        }
        
        waveIndex++;
    }
    
    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}