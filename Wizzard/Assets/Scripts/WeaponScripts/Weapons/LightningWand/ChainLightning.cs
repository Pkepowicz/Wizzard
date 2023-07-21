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

    public GameObject chainLightningEffect;

    public GameObject beenStruct;

    public int amountToChain;

    private GameObject startObject;
    private GameObject endObject;

    private Animator ani;

    public ParticleSystem parti;

    private int singleSpawns;
    
    // Start is called before the first frame update
    void Start()
    {
        
        if(amountToChain <= 0)
            Destroy(gameObject);
        Destroy(gameObject, .4f);
        
        coll = GetComponent<CircleCollider2D>();
        
        ani = GetComponent<Animator>();
        
        parti = GetComponent<ParticleSystem>();

        startObject = gameObject;

        singleSpawns = 1;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyLayer == (enemyLayer | (1 << collision.gameObject.layer)) &&
            !collision.GetComponentInChildren<EnemyStruck>())
        {
            if (singleSpawns != 0)
            {
                endObject = collision.gameObject;
                Instantiate(chainLightningEffect, collision.gameObject.transform.position, Quaternion.identity);

                Instantiate(beenStruct, collision.gameObject.transform);

                collision.SendMessage("ReceiveDamage", damage);
            
                ani.StopPlayback();

                coll.enabled = false;

                singleSpawns--;
                
                parti.Play();

                var emitParams = new ParticleSystem.EmitParams();
                
                emitParams.position = startObject.transform.position;
                
                parti.Emit(emitParams, 1);
                
                emitParams.position = (startObject.transform.position + endObject.transform.position) / 2;
                
                parti.Emit(emitParams, 1);
                
                emitParams.position = endObject.transform.position;
                
                parti.Emit(emitParams, 1);
                
                
            
                Destroy(gameObject, .1f);
            }
            
        }
        
        
    }
}
