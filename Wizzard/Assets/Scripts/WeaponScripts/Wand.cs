using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    // modifiers applied to bullet e.g track enemies
    protected List<string> modifiers = new List<string>();
    
    public GameObject bulletPrefab;

    protected GameObject currentBullet;
    
    protected int damage = 5;
    protected float reloadTime = 1;
    protected float lastShot = 0;
    
    protected int projectileCount;
    public Transform projectileSpawnPoint;
    
    protected virtual void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && (Time.time - lastShot) > reloadTime)
        {
            Debug.Log("Ready to shoot");
            Shoot();
            lastShot = Time.time;
        }
        
    }

    protected virtual void Shoot()
    {
        currentBullet = Instantiate(bulletPrefab, projectileSpawnPoint.position, Quaternion.identity);
        
    }
}
