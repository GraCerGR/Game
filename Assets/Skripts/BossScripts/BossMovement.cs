using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Camera playerCamera; 
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCamera != null)
        {
            transform.forward = playerCamera.transform.forward;
        }
    }
}
