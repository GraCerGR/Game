using UnityEngine;

public class FallingDust : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //[SerializeField] float lifeTime = 60f;
    [SerializeField] GameObject player;
    Animator animator;
    private void Awake()
    {
        
        animator = player.GetComponent<Animator>();
    }
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("itemDust"))
        {
            animator.SetTrigger("dust");
            Destroy(other.gameObject);
        }
        

        // Можно добавить: Destroy при столкновении с чем-либо ещё
        
    }
}
