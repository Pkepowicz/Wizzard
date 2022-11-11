using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public string effectName = "New effect";
    protected Fighter currentObject;

    public float duration; 
    public float startTime = 0; // delay before applying, default is 0
    public float repeatTime; // time between each tick

    protected void Awake()
    {
        currentObject = gameObject.GetComponentInParent<Fighter>();
    }

    protected virtual void Start()
    {
        // is effect repeated e.g heal over time or one time, e.g increased damage dealt during duration
        if (repeatTime > 0)
            InvokeRepeating("ApplyEffect", startTime, repeatTime);
        else 
            Invoke("ApplyEffect", startTime);
        
        Invoke("EndEffect", duration);
        
        
        Debug.Log("started effect " + effectName);

    }

    protected virtual void ApplyEffect()
    {
        
    }

    protected virtual void EndEffect()
    {
        CancelInvoke();
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        CancelInvoke();
    }
}
