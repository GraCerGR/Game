using UnityEngine;

public class FlyingEnemyBullet : MonoBehaviour
{
    public float lifeTime = 5f;
    public int damage = 20;

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

        // ����� ��������: Destroy ��� ������������ � ���-���� ���
        if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
