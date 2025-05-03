using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform dropSpawnPoint;
    [SerializeField] bool isDropped;


    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
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
    }

    


}
