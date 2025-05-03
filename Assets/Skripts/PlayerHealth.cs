using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    [SerializeField] public static float health = 100;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Player HP: " + health);
        if (health <= 0)
        {
            // смерть
        }
    }
}
