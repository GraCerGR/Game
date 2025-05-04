using TMPro;
using UnityEngine;

public class ShootingRaicast1 : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootRange = 100f;
    [SerializeField] private LayerMask shootableLayers;
    [SerializeField] private LayerMask shildLayers;
    [SerializeField] private LayerMask wallsLayers;
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

    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private GameObject hiPrefab;



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
                    Debug.Log(hit.point);
                    if (enemy != null && !Physics.Raycast(ray, out RaycastHit hit1, shootRange, shildLayers))
                    {
                        enemy.TakeDamage(damage);
                        Debug.Log(hit1.point);
                        Debug.Log("hit.point");
                        Instantiate(bloodPrefab, hit.point+new Vector3(0.1f,-0.75f,0), hit.transform.rotation);
                    }
                    Debug.DrawRay(transform.position, hit.point, Color.green, 100.0f, false);
                } else if(Physics.Raycast(ray, out RaycastHit hit1, shootRange, wallsLayers))
                {
                    

                    // 1. Смещение вглубь от поверхности 
                    Vector3 inwardOffset = hit1.normal * 0.1f;

                    // 2. Смещение вниз по поверхности
                    Vector3 surfaceDownOffset = Vector3.zero;
                    Vector3 projectedDown = Vector3.ProjectOnPlane(Vector3.down, hit1.normal);

                    // Проверка, есть ли смысл применять смещение
                    if (projectedDown.sqrMagnitude > 0.001f)
                    {
                        surfaceDownOffset = projectedDown.normalized * 0.2f;
                    }

                    // 3. Общий вектор смещения
                    Vector3 totalOffset = inwardOffset + surfaceDownOffset;

                    // 4. Позиция спавна
                    Vector3 spawnPos = hit1.point + totalOffset;

                    // 5. Получаем позицию игрока
                    Vector3 toPlayer = Camera.main.transform.position - spawnPos;

                    // 6. Убираем вертикальную составляющую, чтобы спрайт не наклонялся вверх/вниз
                    toPlayer.y = 0;
                    toPlayer.Normalize();

                    // 7. Поворот спрайта: стоит вертикально и смотрит на игрока
                    Quaternion rotation = Quaternion.LookRotation(toPlayer);

                    // 8. Спавн
                    Instantiate(hiPrefab, spawnPos, rotation);


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
        ammoCount+= bulletsDropCount;
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
