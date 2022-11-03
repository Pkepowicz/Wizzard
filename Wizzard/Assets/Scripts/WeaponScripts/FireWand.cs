using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWand : Wand
{
    [Header("Possible upgrades")] 
    public bool explodeAtDeath = false;
    public bool shotgun = false;
    public bool igniteEnemies = false;

    
    
    protected override void Shoot()
    {
        base.Shoot();
        
        // pass parameters like explosion, ignite to each bullet created
        foreach (GameObject bullet in currentBullets)
        {
            bullet.GetComponent<Fireball>().PassParameters(damage, explodeAtDeath, igniteEnemies);
        }
    }
    
    
    // if player has shotgun upgrade, projectiles will be given random angle, otherwise execute parent funtion
    protected override Vector3 CalculateProjectileRotation(float shootArc, int i, float bulletAngleDifference)
    {
        if (shotgun)
            return new Vector3(0, 0, Random.Range(-(shootArc / 2), (shootArc / 2)));
        return base.CalculateProjectileRotation(shootArc, i, bulletAngleDifference);
    }
}
