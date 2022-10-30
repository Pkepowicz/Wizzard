using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWand : Wand
{
    public bool explodeAtDeath = false;
    public bool pullEnemies = false;
    

    void Start()
    {

    }

    protected override void Shoot()
    {
        base.Shoot();
        currentBullet.GetComponent<Fireball>().PassParameters(explodeAtDeath, pullEnemies);
    }
}
