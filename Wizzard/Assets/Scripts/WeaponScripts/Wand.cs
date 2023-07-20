using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Wand : MonoBehaviour
{
    [Header("Shooting settings")] 
    public float damage = 1;
    public float reloadTime = 1;
    
    // what kind of projectile wand will shoot
    public GameObject bulletPrefab;
    
    // variables for shooting multiple projectiles
    public int projectileAmount = 1;
    public float shootArc = 30; // maximum angle at which additional projectile will be spawned, 
                                // 30 means 30deg shooting arc, so 15deg deviation on each side
                                
    private float lastShot = 0;
    
    // List of bullets, useful when firing many projectiles
    protected List<GameObject> currentBullets;
    
    // assign instanciated bullet to this variable to be used in derived classes, might be better way to do this
    protected GameObject currentBullet;

    protected virtual void Start()
    {
        currentBullets = new List<GameObject>();
        
    }
    
    public Transform projectileSpawnPoint;
    
    protected virtual void FixedUpdate()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Mouse0)) && (Time.time - lastShot) > reloadTime)
        {
            //Debug.Log("Ready to shoot");
            Shoot();
            lastShot = Time.time;
            SoundManager.PlaySound("PlayerAttack", projectileSpawnPoint.position);
        }
        
    }

    protected virtual void Shoot()
    {
        currentBullets.Clear();
        
        if (projectileAmount == 1)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, projectileSpawnPoint.position, transform.rotation);
            currentBullets.Add(currentBullet);
        }
            
        else
        {
            float bulletAngleDifference = shootArc / (projectileAmount - 1); // angle difference between each bullet shot
            
            for(int i = 0; i < projectileAmount; i++)
            {
                GameObject currentBullet = Instantiate(bulletPrefab, projectileSpawnPoint.position, transform.rotation);
                currentBullet.transform.Rotate(CalculateProjectileRotation(shootArc, i, bulletAngleDifference));
                currentBullets.Add(currentBullet);
            }
        }
        
    }

    // virtual funcion so it may be overwritten e.g random angles for shotgun
    protected virtual Vector3 CalculateProjectileRotation(float shootArc, int i, float bulletAngleDifference)
    {
        return new Vector3(0, 0, (-shootArc / 2) + i * bulletAngleDifference);
    }
}
