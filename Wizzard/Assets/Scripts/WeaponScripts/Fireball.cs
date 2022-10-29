using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    private bool explodeAtDeath = false;
    private bool pullEnemies = false;

    public void PassParameters(bool explode, bool pull)
    {
        explodeAtDeath = explode;
        pullEnemies = pull;
        Debug.Log(explodeAtDeath);
    }

    protected override void OnProjectileFinish()
    {
        if (explodeAtDeath is true)
        {
            Debug.Log("Explode");
        }
        base.OnProjectileFinish();
    }
}
