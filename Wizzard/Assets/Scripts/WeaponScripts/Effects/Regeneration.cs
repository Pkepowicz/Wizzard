using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : Effect
{
    public int healthEachTick;

    protected override void ApplyEffect()
    {
        currentObject.Heal(healthEachTick);
    }
}
