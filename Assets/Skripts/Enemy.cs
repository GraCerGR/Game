using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform dropSpawnPoint;
    [SerializeField] bool isDropped;
    [SerializeField] private float damagePerHit;
    private Transform player;

    private int currentHealth;

    // ---- Звуки ---- 
    private PlayerSoundManager playerSounds;
    public AudioClip dieSound;

    private void Awake()
    {
        currentHealth = maxHealth;
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found");
        }

        playerSounds = player.GetComponent<PlayerSoundManager>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth > 0)
        {
            playerSounds.PlayHitEnemySound();
        }

        Debug.Log("Enemy took damage, current HP: " + currentHealth);
        if (currentHealth <= 0)
        {
            playerSounds.PlayDieSound(dieSound);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        if (isDropped)  Instantiate(dropPrefab, dropSpawnPoint.position, dropSpawnPoint.rotation);
        
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.TakeDustAngel(damagePerHit);

    }

    


}
