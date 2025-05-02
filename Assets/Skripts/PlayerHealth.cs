using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player HP: " + health);
        if (health <= 0)
        {
            // смерть
        }
    }
}
