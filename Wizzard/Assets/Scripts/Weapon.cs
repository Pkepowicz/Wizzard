using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Transform LaunchOffset;

    public float attackSpeed = 1.0f;
    private float lastFire;

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (Time.time - lastFire > attackSpeed)
        {
            Instantiate(projectilePrefab, LaunchOffset.position, transform.rotation);
            lastFire = Time.time;
        }
    }
}
