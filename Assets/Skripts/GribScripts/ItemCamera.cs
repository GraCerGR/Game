using UnityEngine;

public class ItemCamera : MonoBehaviour
{
    private Transform player;
    

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }
    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        transform.forward = direction.normalized;
    }
}
