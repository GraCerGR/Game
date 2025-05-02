using System;
using UnityEngine;

public class ShootingBullets1 : MonoBehaviour
{

    public event EventHandler OnShoot;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private float fireElapsedTime = 0;
    [SerializeField] float fireDelay = 0.2f;
    [SerializeField] private int ammoCount = 100;


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
        if (Input.GetMouseButtonDown(0) && TryShootAmmo() && gameObject.activeInHierarchy )
        {
            
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            OnShoot?.Invoke(this, EventArgs.Empty);
           
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
