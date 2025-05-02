using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    private Transform player;
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float detectionRange = 20f;
    public float fireCooldown = 2f;
    public float fireballSpeed = 10f;
    Vector3 cameraPlace = new Vector3(0f, 1.4f, 0f); 

    private float lastFireTime;


    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position+ cameraPlace);

        if (distanceToPlayer <= detectionRange)
        {
            transform.LookAt(player);

            if (Time.time - lastFireTime >= fireCooldown)
            {
                ShootFireball();
                lastFireTime = Time.time;
            }
        }
    }

    private void ShootFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (player.position + cameraPlace - firePoint.position).normalized;
            rb.linearVelocity = direction * fireballSpeed;

        }
    }
}
