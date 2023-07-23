using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
 
    private CircleCollider2D coll;
    
    public LayerMask enemyLayer;
    
    public float damage;
    public float stunDuration;
    public float pointsInLightning;
    public int amountToChain;

    public GameObject chainLightningEffect;

    public GameObject beenStruct;

    private Animator ani;

    public ParticleSystem parti;

    private int singleSpawns;
    
    // Start is called before the first frame update
    void Start()
    {
        if(amountToChain <= 0)
            Destroy(gameObject);
        Destroy(gameObject, 1f);
        
        Initialize();
    }

    public void Initialize()
    {
        coll = GetComponent<CircleCollider2D>();
        ani = GetComponent<Animator>();
        singleSpawns = 1;
    }

    public void PassParameters(float damage, float stunDuration, int amountToChain, float pointsInLightning)
    {
        this.damage = damage;
        this.stunDuration = stunDuration;
        this.amountToChain = amountToChain;
        this.pointsInLightning = pointsInLightning;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyLayer == (enemyLayer | (1 << collision.gameObject.layer)) &&
            !collision.GetComponentInChildren<EnemyStruck>())
        {
            if (singleSpawns != 0)
            {
                HitTarget(collision.gameObject);
            }
            
        }
    }

    public void HitTarget(GameObject target)
    {
        amountToChain -= 1;
        ani.enabled = true;
        Instantiate(chainLightningEffect, target.transform.position, Quaternion.identity);

        Instantiate(beenStruct, target.transform);

        target.SendMessage("ReceiveDamage", damage);
        target.SendMessage("Stun", stunDuration);
        
        
        ani.StopPlayback();

        coll.enabled = false;

        singleSpawns--;

        parti.Play();

        PlayEffects(target.transform.position);
             
        Destroy(gameObject, .1f);
    }

    public void PlayEffects(Vector3 target)
    {
        var emitParams = new ParticleSystem.EmitParams();

        Vector3 source = gameObject.transform.position;

        // TODO: Maybe implement a way to make lightning travel by many paths
        for (int i = 0; i < pointsInLightning + 1; i++)
        {
            // add random deviation to each point
            Vector3 randomDeviation = new Vector3(UnityEngine.Random.Range(-.025f, .025f),
                UnityEngine.Random.Range(-.025f, .025f), 0);
            if (i == 0 || i == pointsInLightning - 1)
                randomDeviation = Vector3.zero;
            emitParams.position = source + i / pointsInLightning * (target - source) + randomDeviation;
            parti.Emit(emitParams, 1);
        }
    }

    public void DisableSeeking()
    {
        ani.enabled = false;
        singleSpawns = 0;
    }
    
}
