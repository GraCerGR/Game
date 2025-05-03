using System.Collections;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    public float speed = 5f;
    public float detectionRange = 15f;
    public float attackRange = 1.5f;
    public float attackInterval = 1f;
    public int damagePerHit = 10;

    private Transform player;
    private bool isAttacking = false;
    private Animator animator;
    Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found. Assign the 'Player' tag.");
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            animator.SetBool("IsMoving", false);

            if (!isAttacking)
                StartCoroutine(AttackRoutine());
        }
        else if (distance <= detectionRange)
        {
            // Преследуем игрока
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            Vector3 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        while (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            animator.SetTrigger("Attack");

            // Наносим урон игроку
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damagePerHit);

            yield return new WaitForSeconds(attackInterval);
        }

        isAttacking = false;
    }
}
