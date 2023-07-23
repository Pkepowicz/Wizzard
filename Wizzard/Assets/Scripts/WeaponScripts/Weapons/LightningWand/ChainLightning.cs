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
    public float pointsInLightning = 10;

    public GameObject struckByLightningEffect;
    
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
        Destroy(gameObject, 1f);
        
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

                amountToChain -= 1;
                Instantiate(chainLightningEffect, collision.gameObject.transform.position, Quaternion.identity);

                Instantiate(beenStruct, collision.gameObject.transform);

                collision.SendMessage("ReceiveDamage", damage);
                collision.SendMessage("Stun", stunDuration);
            
                ani.StopPlayback();

                coll.enabled = false;

                singleSpawns--;
                
                parti.Play();

                var emitParams = new ParticleSystem.EmitParams();

                
                // TODO: Maybe implement a way to make lightning travel by many paths
                for(int i = 0; i < pointsInLightning + 1; i++)
                {
                    // add random deviation to each point
                    Vector3 randomDeviation = new Vector3(UnityEngine.Random.Range(-.025f, .025f), UnityEngine.Random.Range(-.025f, .025f), 0);
                    emitParams.position = startObject.transform.position + i/pointsInLightning * (endObject.transform.position - startObject.transform.position) + randomDeviation;
                    parti.Emit(emitParams, 1);
                }
                
                Destroy(gameObject, .1f);
            }
            
        }
        
        
    }

    public void PlayEffects(Vector2 target)
    {
        
    }
    
}
