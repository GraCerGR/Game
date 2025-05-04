using UnityEngine;
using System.Collections;

public class FairyController : MonoBehaviour
{
    private Transform player;

    public float innerRadius = 10f;
    public float outerRadius = 20f;
    public float moveSpeed = 2f;
    public float hoverTime = 0f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found");
        }

        StartCoroutine(MovementLoop());
    }

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (distToPlayer > outerRadius)
        {
            return;
        }

        if (distToPlayer < innerRadius && !isMoving)
        {
            Vector3 dirOutward = (transform.position - player.position).normalized;
            targetPosition = player.position + dirOutward * Random.Range(innerRadius + 1f, outerRadius - 1f);
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            Vector3 direction = targetPosition - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                StartCoroutine(MovementLoop());
            }
        }
    }

    IEnumerator MovementLoop()
    {
        yield return new WaitForSeconds(hoverTime);
        int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 circle = Random.insideUnitCircle.normalized * Random.Range(innerRadius + 0.5f, outerRadius - 0.5f);
            float heightOffset = Random.Range(1f, 3f);
            Vector3 candidate = player.position + new Vector3(circle.x, heightOffset, circle.y);


            if (!Physics.Linecast(transform.position, candidate))
            {
                targetPosition = candidate;
                isMoving = true;
                yield break;
            }
        }
    }

    IEnumerator FlyAway()
    {
        Vector3 flyDirection = (transform.position - player.position).normalized;
        Vector3 escapePoint = transform.position + flyDirection * 20f;

        while (Vector3.Distance(transform.position, escapePoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, escapePoint, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
