using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform dropSpawnPoint;
    [SerializeField] bool isDropped;
    [SerializeField] private float damagePerHit;
    private Transform player;

    private int currentHealth;

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
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy took damage, current HP: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        if (isDropped)  Instantiate(dropPrefab, dropSpawnPoint.position, dropSpawnPoint.rotation);// Можно заменить на анимацию смерти
        
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.TakeDustAngel(damagePerHit);

    }

    


}
