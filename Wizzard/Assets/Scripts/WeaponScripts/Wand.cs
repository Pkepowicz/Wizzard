using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Wand : MonoBehaviour
{
    
    // variables for shooting multiple projectiles
    public int projectileAmount = 1;
    public float shootArc = 30; // maximum angle at which additional projectile will be spawned, 
                                // 30 means 30deg shooting arc, so 15def abberation on each side

    private float bulletAngleDifference; // angle difference between each bullet shot

    // what kind of projectile wand will shoot
    public GameObject bulletPrefab;
    
    // List of bullets, useful when firing many projectiles
    protected List<GameObject> currentBullets;
    
    // assign instanciated bullet to this variable to be used in derived classes, might be better way to do this
    protected GameObject currentBullet;

    protected virtual void Start()
    {
        currentBullets = new List<GameObject>();
        bulletAngleDifference = CalculateAngles(projectileAmount, shootArc);
    }

    // calculate angle difference between each next bullet, might be calculated many times during single game
    // e.g getting projectile count upgrade during game
    private float CalculateAngles(int projectileCount, float projectileArc)
    {
        if (projectileAmount != 1)
            return shootArc / (projectileAmount - 1);
        return 1;

    }
    
    protected float reloadTime = 1;
    protected float lastShot = 0;
    
    //protected int projectileCount;
    public Transform projectileSpawnPoint;
    
    protected virtual void Update()
    {
        
        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Mouse0)) && (Time.time - lastShot) > reloadTime)
        {
            //Debug.Log("Ready to shoot");
            Shoot();
            lastShot = Time.time;
        }
        
    }

    protected virtual void Shoot()
    {
        // add instanciated bullet to list of bullets to pass information to them in derived classes
        
        
        // niech ktoś to zrobi lepiej bo oczy bolą od samego patrzenia
        currentBullets.Clear();
        if (projectileAmount == 1)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, projectileSpawnPoint.position, transform.rotation);
            currentBullets.Add(currentBullet);
        }
            
        else
        {
            for(int i = 0; i < projectileAmount; i++)
            {
                GameObject currentBullet = Instantiate(bulletPrefab, projectileSpawnPoint.position, transform.rotation);
                currentBullet.transform.Rotate(0, 0, (-shootArc / 2) + i * bulletAngleDifference);
                currentBullets.Add(currentBullet);
            }
        }
        
    }
}
