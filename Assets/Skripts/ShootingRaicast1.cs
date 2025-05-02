using UnityEngine;

public class ShootingRaicast1 : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootRange = 100f;
    [SerializeField] private LayerMask shootableLayers;
    [SerializeField] private int damage = 10;

    private float fireElapsedTime = 0;
    [SerializeField] float fireDelay = 0.2f;
    [SerializeField] int ammoCount = 10;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireElapsedTime += Time.deltaTime;
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && TryShootAmmo() && gameObject.activeInHierarchy) // Левая кнопка мыши
        {
            Ray ray = new Ray(shootPoint.position, shootPoint.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, shootRange, shootableLayers))
            {
                Debug.DrawLine(shootPoint.position, hit.point, Color.red, 1f);
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

                Debug.DrawRay(transform.position, hit.point, Color.green, 100.0f, false);
            }
            
        }
    }


   

    public bool TryShootAmmo()
    {
        if (ammoCount > 0 && fireElapsedTime >= fireDelay)
        {
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
