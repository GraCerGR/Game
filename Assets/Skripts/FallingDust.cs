using UnityEngine;

public class FallingDust : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //[SerializeField] float lifeTime = 60f;
    [SerializeField] GameObject player;
    Animator animator;
    private float fireElapsedTime = 0;
    private float fireDelay = 4.1f;
    private Transform player23;

    [SerializeField] float hp;

    private Transform pistol;


    private PlayerSoundManager playerSounds;

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

        playerSounds = player23.GetComponent<PlayerSoundManager>();
        Debug.LogError(playerSounds);


    }

    private void Start()
    {

    }
    private void FixedUpdate()
    {
        fireElapsedTime += Time.deltaTime;

        if (animator.GetBool("TakenDust")& fireElapsedTime < fireDelay)
        {
            animator.SetBool("TakenDust", false);
        }

    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("itemDust"))
        {
            animator.SetTrigger("dust");

            if (fireElapsedTime >= fireDelay)
            {
                animator.SetBool("TakenDust", true);


                fireElapsedTime = 0;
            }

            playerSounds.PlayDustSound();


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
            
            playerSounds.PlayBulletSound();




        }


        // Можно добавить: Destroy при столкновении с чем-либо ещё

    }
}
