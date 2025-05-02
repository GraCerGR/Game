using UnityEngine;

public class ItemCamera : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        transform.forward = direction.normalized;
    }
}
