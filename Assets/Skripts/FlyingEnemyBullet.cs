using UnityEngine;

public class FlyingEnemyBullet : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float damage = 20;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            
            Destroy(gameObject);
        }

        // Можно добавить: Destroy при столкновении с чем-либо ещё
        if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
