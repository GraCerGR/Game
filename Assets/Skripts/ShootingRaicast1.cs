using TMPro;
using UnityEngine;

public class ShootingRaicast1 : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootRange = 100f;
    [SerializeField] private LayerMask shootableLayers;
    [SerializeField] private LayerMask shildLayers;
    [SerializeField] private int damage = 10;

    private float fireElapsedTime = 0;
    [SerializeField] float fireDelay = 0.2f;
    [SerializeField] public static int ammoCount = 100;
    private Animator animator;
    [SerializeField] GameObject pistolImage;
    [SerializeField] private TextMeshProUGUI Dust2;
    private int ammoCounter = ammoCount;

    [SerializeField] public float dustAnimationCounter=4f;
    [SerializeField] public float dustAnimationCounterDelay;

    [SerializeField] public int bulletsDropCount = 20;



    // ---- Звуки ---- 
    private PlayerSoundManager playerSounds;

    private void Awake()
    {
        animator = pistolImage.GetComponent<Animator>();
        Dust2.text = $"{ammoCount:0}";
        dustAnimationCounterDelay = 4f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSounds = GameObject.FindWithTag("Player").GetComponent<PlayerSoundManager>();
    }

    void Update()
    {
        fireElapsedTime += Time.deltaTime;
        dustAnimationCounterDelay += Time.deltaTime;
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.activeInHierarchy) // Выстрел
        {
            bool didShoot = TryShootAmmo();

            if (didShoot)
            {
                Ray ray = new Ray(shootPoint.position, shootPoint.forward);
                animator.SetTrigger("shoot");

                playerSounds.PlayGunSound();

                if (Physics.Raycast(ray, out RaycastHit hit, shootRange, shootableLayers))
                {
                    Debug.DrawLine(shootPoint.position, hit.point, Color.red, 1f);
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy != null && !Physics.Raycast(ray, out hit, shootRange, shildLayers))
                    {
                        enemy.TakeDamage(damage);
                    }

                    Debug.DrawRay(transform.position, hit.point, Color.green, 100.0f, false);
                }
            }
            else
            {
                if (fireElapsedTime >= fireDelay)
                    playerSounds.PlayEmptyGunSound();
            }

        }
    }

    public void TakeAmmo()
    {
        //Debug.Log("dd");
        ammoCounter += bulletsDropCount;
        Dust2.text = $"{ammoCounter:0}";
    }
   

    public bool TryShootAmmo()
    {
        if (ammoCount > 0 && fireElapsedTime >= fireDelay&& dustAnimationCounterDelay>= dustAnimationCounter)
        {
            ammoCounter --;
            Dust2.text = $"{ammoCounter:0}";



            ammoCount--;
            fireElapsedTime = 0;
            return true;
        }
        else
        {
            return false;
        }
    }


}
