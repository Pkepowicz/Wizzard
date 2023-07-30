using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Destructable : Collidable
{
    // Start is called before the first frame update
    public Sprite[] statusList;
    public SpriteRenderer spriteRenderer;
    public GameObject gameObject;
    public int MaxHP;
    public int HP;
    void Start()
    
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

       
    }

    // Update is called once per frame
    void Update()
    {
            ChangeSprite(HP,MaxHP);
            DestroyObject(HP);
    }

    public void ChangeSprite(int HP, int maxHP)
    {
        if (HP <= 0.9 * maxHP && HP > 0.7 * maxHP)
        {
            spriteRenderer.sprite = statusList[0];
        }
        else if(HP <= 0.7 * maxHP && HP > 0.4 * maxHP)
        {
            spriteRenderer.sprite = statusList[1];
        }   
        else if(HP <= 0.4 * maxHP )
        {
            spriteRenderer.sprite = statusList[2];
        }   
    }

    public void DestroyObject(int HP)
    {
        if(HP<=0)
            gameObject.SetActive(false);
    }
    
    
    
    
}
