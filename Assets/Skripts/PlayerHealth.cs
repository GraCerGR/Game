using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    [SerializeField] public static float health = 100;
    [SerializeField] public static float dustcount = 0;
    [SerializeField] private TextMeshProUGUI Dust2;

    float ADust = 0;

    // ---- Звуки ---- 
    private PlayerSoundManager playerSounds;

    private void Awake()
    {
        playerSounds = transform.GetComponent<PlayerSoundManager>();
    }

    public void TakeDamage(float amount)
    {
        playerSounds.PlayHitPlayerSound();

        health -= amount;
        Debug.Log("Player HP: " + health);
        if (health <= 0)
        {
            // смерть
        }
    }
    public void TakeDustAngel(float a) 
    {
        ADust += PlayerHealth.dustcount + a;
        Dust2.text = $"{ADust:0}%";
    }
}
