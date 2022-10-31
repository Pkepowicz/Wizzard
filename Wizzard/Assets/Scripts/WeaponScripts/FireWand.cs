using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWand : Wand
{
    public bool explodeAtDeath = false;
    public bool pullEnemies = false;
    
    protected override void Shoot()
    {
        base.Shoot();
        foreach (GameObject bullet in currentBullets)
        {
            bullet.GetComponent<Fireball>().PassParameters(explodeAtDeath, pullEnemies);
        }
        //currentBullet.GetComponent<Fireball>().PassParameters(explodeAtDeath, pullEnemies);
    }
}
