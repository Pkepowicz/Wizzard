using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Fighter
{
    private Transform target;

    public GameObject ghostExplosionPrefab;
    private GameObject currentExplosion;

    // if distance to player is lower than this start explosion
    public float triggerRadius = 5;

    private bool isExploding = false;

    public float explosionDamage = 2;
    public float explosionRadius = 1;
    public float explosionKnockback = 1;

    protected override void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        canMove = true;
        base.Start();
    }
    

    // Update is called once per frame
    void Update()
    {
        if((transform.position - target.position).magnitude <= triggerRadius && !isExploding)
        {
            StartExplosionAnimation();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 moveDir = target.position - transform.position;
            UpdateMotor(moveDir);
        }

    }

    private void StartExplosionAnimation(){
        canMove = false;
        isExploding = true;
        anim.SetTrigger("ExplodeAnim");
        // start animation
    }

    private void Explode()
    {
        Debug.Log("Ghost exploded");
        currentExplosion = Instantiate(ghostExplosionPrefab, transform.position, Quaternion.identity);
        currentExplosion.GetComponent<GhostExplosion>().PassParameters(explosionDamage, explosionRadius, explosionKnockback);
        Destroy();
    }
    
    
}
