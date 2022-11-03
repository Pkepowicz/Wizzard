using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Effect
{
    public float damageEachTick;
    
    protected override void Start()
    {
        // check effects of this fighter, if burning already exists, delete it and start new burn effect
        Transform[] ts = transform.parent.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if (t.name.Equals("Burn(Clone)")&& (t.gameObject != this.gameObject))
            {
                Destroy(t.gameObject);
                return;
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
