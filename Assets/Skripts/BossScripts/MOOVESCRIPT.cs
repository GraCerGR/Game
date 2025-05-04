using System;
using UnityEngine;
using System.Collections;

public class MOOVESCRIPT : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 50f;
    public float yOffset = -5f;
    public float endDelay = 1f;
    

    private BossController bossController;
    
    
    void Awake()
    {
        bossController = GetComponentInChildren<BossController>();

        if (bossController == null)
        {
            Debug.LogError("No boss controller attached");
        }
    }
    void Update()
    {
        int isAttack = UnityEngine.Random.Range(0, 200);
        if (isAttack == 1)
        {
            StartCoroutine(ShiftTowardPlayer());    
        }
        
    }

    private IEnumerator ShiftTowardPlayer()
    {
        Vector3 initialPlayerPosition = playerTransform.position;
        
        if (playerTransform.position == initialPlayerPosition)
        {
            Debug.Log("Player is in the same position, moving towards it.");
            initialPlayerPosition.y += yOffset;
            while (Vector3.Distance(transform.position, initialPlayerPosition) > 5f)
            {
                Debug.Log("Moving towards player...");
                transform.position = Vector3.MoveTowards(transform.position, initialPlayerPosition, moveSpeed * Time.deltaTime);
                StartCoroutine(bossController.ShiftEvent());
                Debug.Log("CATCH YOU!");
                yield return null;
            }
        }
        else
        {
            Debug.Log("Player escaped the strike!");
        }
        
    }
}

