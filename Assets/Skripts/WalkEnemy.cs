using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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

    private NavMeshAgent agent;

    private EnemyAwareness awareness;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        agent.acceleration = 1000f;
        agent.angularSpeed = 999f;

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found");
        }

        //playerObj.GetComponent<EnemyMusicManager>().RegisterEnemy(this);
        awareness = GetComponent<EnemyAwareness>();
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            agent.isStopped = true;
            animator?.SetBool("IsMoving", false);

            agent.SetDestination(transform.position);

            if (!isAttacking)
                StartCoroutine(AttackRoutine());
        }
        else if (distance <= detectionRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            agent.speed = speed;
            animator?.SetBool("IsMoving", true);
        }
        else
        {
            agent.isStopped = true;
            animator?.SetBool("IsMoving", false);
        }

        // ---- Музыка ----
        if (distance <= detectionRange + 5f)
        {
            if (awareness != null) awareness.isSeeYou = true;
        }
        else
        {
            if (awareness != null) awareness.isSeeYou = false;
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        while (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            animator.SetTrigger("Attack");

            // ���� ������
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damagePerHit);
                

            yield return new WaitForSeconds(attackInterval);
        }

        isAttacking = false;
    }
}
