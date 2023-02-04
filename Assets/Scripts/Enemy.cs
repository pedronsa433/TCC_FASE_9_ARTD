using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private int killReward = 25;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Image healthBar;

    private bool isDead = false;

    [HideInInspector] public float health;
    [HideInInspector] public float Speed;
    [HideInInspector] public float startSpeed;

    
    private void Start()
    {
        health = maxHealth;
        startSpeed = Random.Range(minSpeed, maxSpeed);
        Speed = startSpeed;
    }
    
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / maxHealth;;
        
        if (health <= 0 && !isDead)
            Die();
    }
    
    public void Die()
    {
        isDead = true;
        
        PlayerStats.Money += killReward;

        GameObject effect = Instantiate(deathEffect, transform.position, quaternion.identity);
        Destroy(effect, 2f);

        Destroy(gameObject);

        WaveSpawner.enemiesAlive--;
    }

    
    public void Slow(float slowAmount) => Speed = startSpeed * (1f - slowAmount);
}