using UnityEngine;
using static Unity.VisualScripting.Member;

public class FlyingEnemy : MonoBehaviour
{
    private Transform player;
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float detectionRange = 20f;
    public float fireCooldown = 2f;
    public float fireballSpeed = 10f;
    Vector3 cameraPlace = new Vector3(0f, 1.4f, 0f);
    private bool isAttacking = false;

    private float lastFireTime;

    Animator animator;

    public AudioSource fireSource;
    public AudioClip fireSound;

    private EnemyAwareness awareness;


    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        animator = GetComponent<Animator>();

        awareness = GetComponent<EnemyAwareness>();
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

        // ---- Музыка ----
        if (distanceToPlayer <= detectionRange + 5f)
        {
            if (awareness != null) awareness.isSeeYou = true;
}
        else
        {
            if (awareness != null) awareness.isSeeYou = false;
        }
    }

    private void ShootFireball()
    {
        animator.SetTrigger("Attack");

        // Подождать 0.2–0.5 секунды можно через Animation Event, если нужно
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

        fireSource.PlayOneShot(fireSound, 2f);

        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (player.position + cameraPlace - firePoint.position).normalized;
            rb.linearVelocity = direction * fireballSpeed;
        }
    }

}
