using UnityEngine;

public class UIMovingWeaponWalk : MonoBehaviour
{

    private bool isMoving = false;
    private Animator cameraAnimator;
    [SerializeField] private Transform playerCamera;
   

    private void Awake()
    {
        cameraAnimator = playerCamera.GetComponent<Animator>();
    }

    void Start()
    {


    }

    private void Update()
    {

        HandleCameraBob();
    }

    private void HandleCameraBob()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        bool moving = moveX != 0 || moveZ != 0;

        if (moving != isMoving)
        {
            isMoving = moving;
            cameraAnimator.SetBool("isWalking", isMoving);
        }
    }
}
