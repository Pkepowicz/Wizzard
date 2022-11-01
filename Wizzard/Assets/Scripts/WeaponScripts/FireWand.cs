using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWand : Wand
{
    public bool explodeAtDeath = false;
    public bool pullEnemies = false;
    public bool shotgun = false;
    
    protected override void Shoot()
    {
        
        base.Shoot();
        foreach (GameObject bullet in currentBullets)
        {
            bullet.GetComponent<Fireball>().PassParameters(explodeAtDeath, pullEnemies);
        }
        //currentBullet.GetComponent<Fireball>().PassParameters(explodeAtDeath, pullEnemies);
    }
    
    
    // if player has shotgun upgrade, projectiles will be given random angle, else execute normal funtion
    protected override Vector3 CalculateProjectileRotation(float shootArc, int i, float bulletAngleDifference)
    {
        if (shotgun)
            return new Vector3(0, 0, Random.Range(-(shootArc / 2), (shootArc / 2)));
        return base.CalculateProjectileRotation(shootArc, i, bulletAngleDifference);
    }
}
