using TMPro;
using UnityEngine;
using System.Runtime;
using UnityEngine.Audio;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform dropSpawnPoint;
     bool isDropped = false;
    [SerializeField] private float countOfDust;
    private Transform player;
    private float luck;
    [SerializeField] private float luckOfDrop;
    [SerializeField] private GameObject DiePrefab;


    private int currentHealth;

    // ---- ����� ---- 
    private PlayerSoundManager playerSounds;
    public AudioClip dieSound;

    private void Awake()
    {
        luck = Random.value;
        Debug.Log(luck);

        if (luck < luckOfDrop/100)
        {
                isDropped = true;
        }
        

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
        Instantiate(DiePrefab, gameObject.transform.position /*+ new Vector3(0.1f, -0.75f, 0)*/, gameObject.transform.rotation);


        Destroy(gameObject);
        if (isDropped)  Instantiate(dropPrefab, dropSpawnPoint.position, dropSpawnPoint.rotation);
        
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.TakeDustAngel(countOfDust);

    }

    


}
