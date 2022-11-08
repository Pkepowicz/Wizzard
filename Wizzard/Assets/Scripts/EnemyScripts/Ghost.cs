using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : EnemyAI
{
    public GameObject player;
    private Transform target;

    public GameObject ghostExplosionPrefab;
    
    private animation explosionAnim;

    // if distance to player is lower than this start explosion
    public float trigger radius = 5;

    void Start()
    {
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position - target.position).magnitude <= radius){
            StartExplosion()
        }
    }

    private void StartExplosion(){
        canMove = false;
        Destroy(gameObject, explosionAnim.length);
        // start animation
    }

    private 
}
