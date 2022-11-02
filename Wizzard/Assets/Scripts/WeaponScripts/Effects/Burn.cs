using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Effect
{
    public float damageEachTick;
    
    protected override void Start()
    {
        // check if object is already burning (contains Burn child), if so, destroy this game object
        int burnCount = 0;
        Transform[] ts = transform.parent.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if (t.name.Equals("Burn(Clone)"))
            {
                burnCount += 1;
                if (burnCount >= 2)
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }
        base.Start();
    }

    public void CalculateBurnDamage(float impactDamage, float igniteEfficiency)
    {
        damageEachTick = (impactDamage * igniteEfficiency) / (duration / repeatTime);
    }

    protected override void ApplyEffect()
    {
        currentObject.SendMessage("TakeDamageFromDot", damageEachTick);
    }
}
