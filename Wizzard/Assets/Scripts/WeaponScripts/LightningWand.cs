using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWand : Wand
{
    public float radius = 3f;
    public LayerMask enemyLayer;
    public GameObject chainLightningEffect;

    private GameObject target = null;
    
    protected override void Shoot()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target = GetClosestEnemy(position);
        if (target != null)
        {
            GameObject wandTip = Instantiate(chainLightningEffect, projectileSpawnPoint.transform.position, Quaternion.identity);
            ChainLightning wandTipScript = wandTip.GetComponent<ChainLightning>();
            wandTipScript.Initialize();
            wandTipScript.HitTarget(target);
            return;
            /*
            GameObject chainLightning = Instantiate(chainLightningEffect, position, Quaternion.identity);
            
            GameObject wandTip = Instantiate(chainLightningEffect, projectileSpawnPoint.transform.position, Quaternion.identity);
            ChainLightning wandTipScript = wandTip.GetComponent<ChainLightning>();
            wandTipScript.DisableSeeking();

            wandTipScript.PlayEffects(chainLightning.transform.position);*/
        }
        else
        {
            GameObject chainLightning = Instantiate(chainLightningEffect, position, Quaternion.identity);
            ChainLightning chain = chainLightning.GetComponent<ChainLightning>();
            chain.Initialize();
            chain.DisableSeeking();
            
            GameObject wandTip = Instantiate(chainLightningEffect, projectileSpawnPoint.transform.position, Quaternion.identity);
            ChainLightning wandTipScript = wandTip.GetComponent<ChainLightning>();
            wandTipScript.Initialize();
            wandTipScript.DisableSeeking();
            wandTipScript.PlayEffects(chainLightning.transform.position);
        }
        
    }
    
    GameObject GetClosestEnemy(Vector2 position)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, radius, enemyLayer);
        float closestDistance = radius;
        GameObject closestEnemy = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hitCollider.gameObject;
                }
            }
        }

        return closestEnemy;
    }
}
