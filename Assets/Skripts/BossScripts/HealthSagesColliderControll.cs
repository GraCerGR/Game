using System.Collections;
using TMPro;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class HealthStagesColliderControll : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject dropPrefabHealth;
    [SerializeField] private GameObject dropPrefabAmmo;
    [SerializeField] private GameObject dropPrefabKey;
    [SerializeField] private Transform dropSpawnPoint;
    [SerializeField] bool isDropped;
    [SerializeField] private float damagePerHit;
    private Transform player;
    private BossController bossController;
    public bool changeOnce = true;
    private int currentHealth;

    private void Awake()
    {
        bool changeOnce = true;
        bossController = GetComponentInChildren<BossController>();
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
        DropSomething();
        

        if (currentHealth < maxHealth * 0.75 && changeOnce)
        {
            bossController.ChangeToSecondStage();
        }
        
        if (currentHealth < maxHealth * 0.5 && changeOnce)
        {
            bossController.ChangeToThirdStage();
        }

        if (currentHealth <= maxHealth * 25 && changeOnce)
        {
            bossController.ChangeToFourthStage();
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(DiePlease());
            // stop chasing and goes up a bit then disappears and drop key

        }
    }


    private void DropSomething()
    {
        int dropMe = UnityEngine.Random.Range(0, 10);
        if (dropMe == 1)
        {
            Instantiate(dropPrefabHealth, dropSpawnPoint.position + new Vector3(0, 10, 0), dropSpawnPoint.rotation);
        }
        if (dropMe == 2)
        {
            Instantiate(dropPrefabAmmo, dropSpawnPoint.position + new Vector3(0, 10, 0), dropSpawnPoint.rotation);
        }
        
    }

    public IEnumerator DiePlease()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        Instantiate(dropPrefabKey, new Vector3 (dropSpawnPoint.position.x, 37, dropSpawnPoint.position.z), dropSpawnPoint.rotation);
    } 
    
    
    private void Die()
    {
        // Destroy(gameObject);
        // if (isDropped)  Instantiate(dropPrefab, dropSpawnPoint.position, dropSpawnPoint.rotation);

    }
}