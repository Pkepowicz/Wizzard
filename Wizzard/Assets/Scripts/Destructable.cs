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
    public float  HP;
    
    
    public void TakeDamage(float dmg){
        HP =- dmg;
        ChangeSprite();
        if(HP <= 0){
            DestroyThis();
        }
    }

    public void ChangeSprite(){
        switch(HP){
            case  1 when HP< 0.9 * MaxHP:
                spriteRenderer.sprite = statusList[0];
                break;
            case 2 when HP < 0.5 * MaxHP:
                spriteRenderer.sprite = statusList[1];
                break;
            case 3 when HP < 0.3 * MaxHP:
                spriteRenderer.sprite = statusList[2];
                break;
        }
    }

    private void DestroyThis(){
        
        Destroy(gameObject);
    }
    
    
    
}
