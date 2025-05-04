using UnityEngine;

public class FallingDust : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //[SerializeField] float lifeTime = 60f;
    [SerializeField] GameObject player;
    Animator animator;

    private Transform player23;

    [SerializeField] float hp;

    private Transform pistol;
    private void Awake()
    {
        
        animator = player.GetComponent<Animator>();
        GameObject playerObj = GameObject.FindWithTag("ShootingRaycast");
        if (playerObj != null)
        {
            pistol = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found");
        }

        GameObject playerObj23 = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player23 = playerObj23.transform;
        }
        else
        {
            Debug.LogError("Player not found");
        }




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

            ShootingRaicast1 shootingRaycast = pistol.GetComponent<ShootingRaicast1>();
            if (shootingRaycast != null)
                shootingRaycast.dustAnimationCounterDelay = 0;

            PlayerHealth playerHealth = player23.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeHP(hp);

            

        }
        if (other.CompareTag("ItemBullet"))
        {
            //animator.SetTrigger("dust");
            Destroy(other.gameObject);

            ShootingRaicast1 shootingRaycast = pistol.GetComponent<ShootingRaicast1>();
            if (shootingRaycast != null)
                shootingRaycast.TakeAmmo();

            

            

        }


        // Можно добавить: Destroy при столкновении с чем-либо ещё

    }
}
